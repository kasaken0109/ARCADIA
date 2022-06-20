
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// タイマーの表示と制御を行う
/// </summary>
public class TimerManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("クエストの制限時間")]
    private int timeLimit = 180;

    [SerializeField]
    [Tooltip("時間切れの警告時間")]
    private int timeWarning = 20;

    [SerializeField]
    [Tooltip("残り時間の表示テキスト")]
    private Text m_timeText = null;

    [SerializeField]
    [Tooltip("ゲームオーバーＵＩ")]
    private GameObject m_gameOver = null;
    [SerializeField]
    [Tooltip("ゲームクリアＵＩ")]
    private GameObject m_gameClear = null;
    
    Animation m_anim = null;
    int maxTimeLimit;
    bool isPlaying = false;
    public bool IsPlaying { set => isPlaying = value; }
    // Start is called before the first frame update
    void Start()
    {
        TimerSetUp();
        m_anim = GetComponent<Animation>();
    }

    /// <summary>
    /// タイマーの初期設定を行う
    /// </summary>
    private void TimerSetUp()
    {
        //クリアランク判定用にクエスト制限時間を保存
        PlayerPrefs.SetInt("MaxTime", timeLimit);
        PlayerPrefs.Save();
        ///

        m_gameClear.SetActive(false);
        m_gameOver.SetActive(false);
        maxTimeLimit = timeLimit;
    }

    public IEnumerator TimeUpdate()
    {
        m_timeText.text = "制限時間：" + timeLimit;
        while (timeLimit > 0)
        {
            if (isPlaying)
            {
                timeLimit--;
                m_timeText.text = "制限時間：" + timeLimit;
                yield return new WaitForSeconds(1);
            }
            if (timeLimit <= timeWarning)
            {
                //アニメーションとフォントの色を変え、警告
                m_timeText.color = Color.red;
                m_anim.Play();
            }
            yield return null;
        }
        GameManager.Instance.SetGameState(GameManager.GameState.PLAYERLOSE);
    }

    /// <summary>
    /// クエストの結果を保存
    /// </summary>
    public void SaveTime()
    {
        if (GameManager.Instance.GameStatus == GameManager.GameState.PLAYERWIN)
        {
            //経過時間を計算
            PlayerPrefs.SetInt("TimeScore", maxTimeLimit - timeLimit);
            PlayerPrefs.Save();
        }
        else if (GameManager.Instance.GameStatus == GameManager.GameState.PLAYERLOSE)
        {
            PlayerPrefs.SetInt("TimeScore", maxTimeLimit);
            PlayerPrefs.Save();
        }
    }
}
