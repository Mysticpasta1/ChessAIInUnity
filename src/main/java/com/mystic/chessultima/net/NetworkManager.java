package com.mystic.chessultima.net;

import com.mystic.chessultima.model.Move;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.net.InetSocketAddress;
import java.net.ServerSocket;
import java.net.Socket;
import java.nio.charset.StandardCharsets;

/**
 * Minimal TCP peer-to-peer transport for online play. One side hosts (listens),
 * the other joins. The host dictates the starting FEN and which colour each side
 * plays. All callbacks are delivered on a background thread; the UI wraps them.
 */
public final class NetworkManager {

    public interface Listener {
        void onConnected(boolean localPlaysWhite, String fen);
        void onMove(Move move);
        void onDisconnected(String reason);
        void onStatus(String status);
    }

    private ServerSocket serverSocket;
    private Socket socket;
    private PrintWriter out;
    private volatile boolean running;
    private Listener listener;

    public boolean isConnected() {
        return running && socket != null && socket.isConnected() && !socket.isClosed();
    }

    /** Host a game. {@code hostPlaysWhite} decides colours for both peers. */
    public void host(int port, boolean hostPlaysWhite, String fen, Listener l) {
        this.listener = l;
        Thread t = new Thread(() -> {
            try {
                serverSocket = new ServerSocket();
                serverSocket.setReuseAddress(true);
                serverSocket.bind(new InetSocketAddress(port));
                l.onStatus("Waiting for opponent on port " + port + " ...");
                socket = serverSocket.accept();
                setupStreams();
                // Tell the client its colour and the position.
                out.println("HELLO " + (hostPlaysWhite ? 0 : 1) + " " + fen);
                l.onConnected(hostPlaysWhite, fen);
                l.onStatus("Opponent connected.");
                readLoop();
            } catch (IOException e) {
                fail("Host error: " + e.getMessage());
            }
        }, "net-host");
        t.setDaemon(true);
        t.start();
    }

    /** Join a hosted game. */
    public void join(String hostAddress, int port, Listener l) {
        this.listener = l;
        Thread t = new Thread(() -> {
            try {
                l.onStatus("Connecting to " + hostAddress + ":" + port + " ...");
                socket = new Socket();
                socket.connect(new InetSocketAddress(hostAddress, port), 15000);
                setupStreams();
                readLoop();
            } catch (IOException e) {
                fail("Join error: " + e.getMessage());
            }
        }, "net-join");
        t.setDaemon(true);
        t.start();
    }

    private void setupStreams() throws IOException {
        running = true;
        out = new PrintWriter(new java.io.OutputStreamWriter(socket.getOutputStream(),
                StandardCharsets.UTF_8), true);
    }

    private void readLoop() {
        try (BufferedReader in = new BufferedReader(new InputStreamReader(
                socket.getInputStream(), StandardCharsets.UTF_8))) {
            String line;
            while (running && (line = in.readLine()) != null) {
                handle(line.trim());
            }
        } catch (IOException e) {
            if (running) fail("Connection lost: " + e.getMessage());
        }
        if (running) fail("Opponent disconnected.");
    }

    private void handle(String line) {
        if (line.isEmpty()) return;
        if (line.startsWith("HELLO ")) {
            String[] parts = line.split(" ", 3);
            boolean localPlaysWhite = "1".equals(parts[1]); // client's colour flag
            String fen = parts.length > 2 ? parts[2] : "";
            listener.onConnected(localPlaysWhite, fen);
            listener.onStatus("Connected.");
        } else if (line.startsWith("MOVE ")) {
            try {
                listener.onMove(Move.decode(line.substring(5)));
            } catch (RuntimeException ex) {
                listener.onStatus("Bad move received.");
            }
        } else if (line.startsWith("MSG ")) {
            listener.onStatus(line.substring(4));
        }
    }

    public void sendMove(Move m) {
        if (out != null) out.println("MOVE " + m.encode());
    }

    private void fail(String reason) {
        boolean wasRunning = running;
        close();
        if (wasRunning && listener != null) listener.onDisconnected(reason);
    }

    public void close() {
        running = false;
        try { if (socket != null) socket.close(); } catch (IOException ignored) {}
        try { if (serverSocket != null) serverSocket.close(); } catch (IOException ignored) {}
    }
}
