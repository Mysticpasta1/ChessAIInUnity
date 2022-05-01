using System.Collections;
using System.Collections.Generic;
using Net;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    public GameObject MultiplayerMenu;
    public GameObject MainMenu;
    public GameObject WaitingMenu;
    public GameObject OnlineMenu;
    public GameObject BetaWarning;
    public GameObject OptionsMenu;
    public Server server;
    public Client client;
    [SerializeField] private TMP_InputField addressInput;

    public void HandleStartGame()
    {
        DontDestroyOnLoad(this);
        StartCoroutine(LoadStartGame());
    }

    public IEnumerator LoadStartGame()
    {
        yield return SceneManager.LoadSceneAsync("GameScreen");
        GameManager gm = GameManager.current;
        FenDontDestrory fdd = FenDontDestrory.instance;
        fdd.DontDestroryFenScript();
        gm.Setup();
    }
    
    public void HandleAIButton()
    {
        DontDestroyOnLoad(this);
        StartCoroutine(LoadAIMenu());
    }

    public IEnumerator LoadAIMenu()
    {
        yield return SceneManager.LoadSceneAsync("GameScreen");
        GameManager gm = GameManager.current;
        gm.Setup();
        gm.AIvsAI();
    }

    public void HandlePlayerWhiteVsAIButton()
    {
        DontDestroyOnLoad(this);
        StartCoroutine(LoadPlayerWhiteVsAI());
    }

    public IEnumerator LoadPlayerWhiteVsAI()
    {
        yield return SceneManager.LoadSceneAsync("GameScreen");
        GameManager gm = GameManager.current;
        gm.Setup();
        gm.PlayerPlaysWhite();
    }

    public void HandlePlayerBlackVsAIButton()
    {
        DontDestroyOnLoad(this);
        StartCoroutine(LoadPlayerBlackVsAI());
    }

    public IEnumerator LoadPlayerBlackVsAI()
    {
        yield return SceneManager.LoadSceneAsync("GameScreen");
        GameManager gm = GameManager.current;
        gm.Setup();
        gm.PlayerPlaysBlack();
    }

    public void HandleHomeGameButton()
    {
        DontDestroyOnLoad(this);
        StartCoroutine(LoadHomeGame());
    }

    public IEnumerator LoadHomeGame()
    {
        yield return SceneManager.LoadSceneAsync("GameScreen");
        GameManager gm = GameManager.current;
        gm.Setup();
        gm.OnHomeGame();
    }

    public void HandleLocalGame()
    {
        DontDestroyOnLoad(this);
        StartCoroutine(LoadLocalGame());
    }

    public IEnumerator LoadLocalGame()
    {
        yield return SceneManager.LoadSceneAsync("GameScreen");
        GameManager gm = GameManager.current;
        gm.Setup();
        server.Init(8009);
        client.Init("127.0.0.1", 8009);
    }

    public void HandlePlayerVsPlayerButton()
    {
        MainMenu.SetActive(false);
        MultiplayerMenu.SetActive(true);
    }

    public void HandMultiplayerBackButton()
    {
        MultiplayerMenu.SetActive(false);
        MainMenu.SetActive(true);
    }
    
    public void HandleOnlineGameButton()
    {
        MultiplayerMenu.SetActive(false);
        OnlineMenu.SetActive(true);
    }

    public void OnOnlineConnectButton()
    {
        OnlineMenu.SetActive(false);
        WaitingMenu.SetActive(true);
        client.Init(addressInput.text, 8009);
    }

    public void OnOnlineBackButton()
    {
        OnlineMenu.SetActive(false);
        MultiplayerMenu.SetActive(true);
    }

    public void OnWaitingBackButton()
    {
        OnlineMenu.SetActive(true);
        WaitingMenu.SetActive(false);
        server.Shutdown();
        client.Shutdown();
    }

    public void OnOptionsFenMenuButton()
    {
        MainMenu.SetActive(false);
        BetaWarning.SetActive(true);
    }

    public void OnBetaWarningButton()
    {
        BetaWarning.SetActive(false);
        OptionsMenu.SetActive(true);
    }

    public void OnBackOptionsButton()
    {
        OptionsMenu.SetActive(false);
        MainMenu.SetActive(true);
    }
    
    public void OnHostButton()
    {
        WaitingMenu.SetActive(true);
        OnlineMenu.SetActive(false);
        server.Init(8009);
        client.Init("127.0.0.1", 8009);
    }
}
