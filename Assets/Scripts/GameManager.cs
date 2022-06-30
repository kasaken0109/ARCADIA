using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;
using Cinemachine;
using DG.Tweening;

public enum GameState
{
    START,
    PLAYING,
    STOP,
    RESUME,
    PLAYERWIN,
    PLAYERLOSE,
}
public class GameManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("プレイヤーのバーチャルカメラ")]
    private CinemachineVirtualCamera virtualCamera = default;

    [SerializeField]
    [Tooltip("メニューオブジェクト")]
    private GameObject menu = default;

    [SerializeField]
    [Tooltip("勝利オブジェクト")]
    private GameObject m_win = null;

    [SerializeField]
    [Tooltip("勝利時のゲートオブジェクト")]
    private GameObject m_gate = null;

    [SerializeField]
    [Tooltip("敗北オブジェクト")]
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

    /// <summary>
    /// メニュー開閉をする
    /// </summary>
    public void SetMenu()
    {
        myGameState = !menu.activeInHierarchy ? GameState.STOP : GameState.RESUME;
        SetGameConnditoin();
    }

    /// <summary>
    /// ゲームの状態に応じた処理を実行する
    /// </summary>
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
                m_lose.GetComponentInChildren<Button>().onClick.Invoke();
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
                m_timerManager.SaveTime();
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// ゲームの状態を設定する
    /// </summary>
    /// <param name="gameState">ゲームの状態</param>
    public void SetGameState(GameState gameState)
    {
        myGameState = gameState;
        SetGameConnditoin();
    }

    /// <summary>
    /// ゲームの状態を設定する
    /// </summary>
    /// <param name="value">ゲームの状態</param>
    public void SetGameState(int value) => SetGameState((GameState)value);

    private GameState myGameState;

    public GameState GameStatus => myGameState;

    private void OnDestroy()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
