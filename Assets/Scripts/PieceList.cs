using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceList : MonoBehaviour
{

    public int[] occupiedSquares; // Indices of squares occupied by given piece type (only elements up to Count are valid, the rest are unused/garbage)
    int[] map; // Map to go from index of a square, to the index in the occupiedSquares array where that square is stored
    int numPieces;

    public PieceList(int maxPieceCount = 144)
    {
        occupiedSquares = new int[maxPieceCount];
        map = new int[576];
        int[] randomDebug = occupiedSquares;
        numPieces = 0;
    }

    public int Count
    {
        get
        {
            return numPieces;
        }
    }

    public void AddPieceAtSquare(int square)
    {
        occupiedSquares[numPieces] = square;
        map[square] = numPieces;
        numPieces++;
    }

    public int this[int index] => occupiedSquares[index];
}
