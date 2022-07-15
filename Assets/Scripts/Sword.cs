using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// 剣が扱う処理を行う
/// </summary>
public class Sword : MonoBehaviour,IWeapon
{
    [SerializeField]
    [Tooltip("有効にする剣のコライダー")]
    GameObject[] _activeCollider;

    [SerializeField]
    [Tooltip("剣のコライダーコントローラー")]
    AttackcolliderController _controller;
    
    /// <summary>プレイヤーのrb</summary>
    Rigidbody rb;

    /// <summary>通常攻撃の当たり判定発生時間</summary>
    const float normalActiveTime = 0.5f;
    /// <summary>スペシャル攻撃の当たり判定発生時間</summary>
    const float specialActiveTime = 1f;
    /// <summary>ジャンプ距離</summary>
    const float jumpUpDistance = 0.5f;
    /// <summary>浮く距離</summary>
    const float floatUpDistance = 2f;
    /// <summary>浮く時間</summary>
    const float floatUpDuraration = 0.5f;
    /// <summary>ダメージ計算値</summary>
    const int damageRate = 8;
    /// <summary>ダメージ最大値</summary>
    const int maxDamage = 100;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Player.TryGetComponent(out rb);
    }
    /// <summary>
    /// 通常攻撃の当たり判定を有効にする
    /// </summary>
    public void BasicAttack()
    {
        ColliderGenerater.Instance.StartActiveCollider(_activeCollider[0], normalActiveTime);
    }

    /// <summary>
    /// スペシャル攻撃の当たり判定を有効にする
    /// </summary>
    public void SpecialAttack()
    {
        ColliderGenerater.Instance.StartActiveCollider(_activeCollider[1], specialActiveTime);
        rb.DOMoveY(0,jumpUpDistance);
    }

    /// <summary>
    /// 剣の軌跡を消す
    /// </summary>
    public void StopEmitting()
    {
        GetComponentInChildren<TrailRenderer>().emitting = false;
    }

    /// <summary>
    /// 剣の軌跡を表示する、ダメージを計算する
    /// </summary>
    public void StartEmitting()
    {
        GetComponentInChildren<TrailRenderer>().emitting = true;
        
        int dmg = (int)Mathf.Abs(transform.position.y - GameObject.FindGameObjectWithTag("Floor").transform.position.y) * damageRate;
        int correctDmg = dmg >= maxDamage ? maxDamage : dmg;
        _controller.AddDamageCount(correctDmg);
    }

    /// <summary>
    /// 浮かび上がる
    /// </summary>
    public void FloatUp()
    {
        rb.DOMoveY(GameManager.Player.transform.position.y + floatUpDistance, floatUpDuraration);
    }
}
