using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

/// <summary>
/// メニューを制御するクラス
/// </summary>
public class MenuController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("メニュー")]
    private GameObject menu = default;

    [SerializeField]
    [Tooltip("プレイヤーのバーチャルカメラ")]
    private CinemachineVirtualCamera virtualCamera = default;

    private bool menuFlag = true;
    
    // Start is called before the first frame update
    private void Start()
    {
        //Time.timeScale = 0f;//メニューを最初に表示しない場合は要コメントアウト
    }
    // Update is called once per frame
    void Update()
    {
        //　ゲームの結果が出た場合は処理をスキップ
        if (GameManager.Instance.GameStatus == GameManager.GameState.PLAYERWIN || GameManager.Instance.GameStatus == GameManager.GameState.PLAYERLOSE) return;
        if (Input.GetButtonDown("Menu"))
        {
            SetMenu();
        }

        if (!menuFlag)
        {
            Cursor.visible = false;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }

    }

    public void SetMenu()
    {
        Time.timeScale = menuFlag ? 1 : 0;
        menu.SetActive(!menuFlag);
        SetCamera(!menuFlag);
        //GameManager.Instance.GameStatus = menuFlag ? GameManager.GameState.PLAYING : GameManager.GameState.STOP;
        menuFlag = menuFlag ? false : true;
    }

    public void SetCamera(bool value)
    {
        virtualCamera.enabled = value;
    }

    private void OnDestroy()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
}
