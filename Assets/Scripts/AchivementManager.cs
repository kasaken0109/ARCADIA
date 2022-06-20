using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アクション成功回数を管理する
/// </summary>
public class AchivementManager:MonoBehaviour
{
    public static AchivementManager Instance { get; private set; }

    [Tooltip("回避に成功した回数")]
    private static int m_dodgeCount = 0;

    /// <summary>
    /// 回避に成功した回数
    /// </summary>
    public int DodgeCount { get => m_dodgeCount; set { m_dodgeCount = value; }}
    private void Awake()
    {
        Instance = this;
    }

}
