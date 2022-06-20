using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using DG.Tweening;
public class GameManager : MonoBehaviour
{

    [SerializeField]
    private GameObject m_bossEnemy = null;

    [SerializeField]
    [Tooltip("プレイヤーのバーチャルカメラ")]
    private CinemachineVirtualCamera virtualCamera = default;

    [SerializeField]
    [Tooltip("メニュー")]
    private GameObject menu = default;

    [SerializeField]
    private GameObject m_win = null;

    [SerializeField]
    private GameObject m_gate = null;

    [SerializeField]
    private GameObject m_lose = null;

    [SerializeField]
    [Tooltip("照準のUI,バレットの参照用")]
    private RectTransform m_crosshairUi = null;

    [SerializeField]
    private PlayerManager m_player = default;

    [SerializeField]
    private TimerManager m_timerManager = default;

    public static GameManager Instance = null;

    public RectTransform CrosshairUI => m_crosshairUi;

    
    static public PlayerManager Player => Instance.m_player;

    void Awake()
    {
        Instance = this;
        PlayerPrefs.SetString("SceneName",SceneManager.GetActiveScene().name);
        PlayerPrefs.Save();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        SetGameState(GameState.START);
    }

    public void SetMenu()
    {
        myGameState = !menu.activeInHierarchy ? GameState.STOP : GameState.RESUME;
        SetGameConnditoin();
    }

    private void SetGameConnditoin()
    {
        switch (myGameState)
        {
            case GameState.START:
                m_timerManager.IsPlaying = true;
                StartCoroutine(m_timerManager.TimeUpdate());
                break;
            case GameState.PLAYING:
                break;
            case GameState.STOP:
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
                m_timerManager.IsPlaying = false;
                Time.timeScale = 0f;
                virtualCamera.enabled = false;
                menu.SetActive(true);
                break;
            case GameState.RESUME:
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.None;
                m_timerManager.IsPlaying = true;
                Time.timeScale = 1;
                virtualCamera.enabled = true;
                menu.SetActive(false);
                break;
            case GameState.PLAYERWIN:
                m_win.SetActive(true);
                m_gate?.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
                m_timerManager.SaveTime();
                break;
            case GameState.PLAYERLOSE:
                m_lose.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
                m_timerManager.SaveTime();
                break;
            default:
                break;
        }
    }

    public void SetGameState(GameState gameState)
    {
        myGameState = gameState;
        SetGameConnditoin();
    }

    public void SetGameState(int value) => SetGameState((GameState)value);

    private GameState myGameState;

    public GameState GameStatus => myGameState;

    public enum GameState
    {
        START,
        PLAYING,
        STOP,
        RESUME,
        PLAYERWIN,
        PLAYERLOSE,

    }

    private void OnDestroy()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
