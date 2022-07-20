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

    /// <summary>剣のコライダーコントローラー</summary>
    AttackcolliderController[] _controller;

    Coroutine[] attackColliderCoroutine;
    
    /// <summary>プレイヤーのrb</summary>
    Rigidbody rb;

    /// <summary>通常攻撃の当たり判定発生時間</summary>
    const float normalActiveTime = 0.5f;
    /// <summary>スペシャル攻撃の当たり判定発生時間</summary>
    const float specialActiveTime = 1f;
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
        attackColliderCoroutine = new Coroutine[_activeCollider.Length];
        ColliderSetUp();
    }

    void ColliderSetUp()
    {
        _controller = new AttackcolliderController[_activeCollider.Length];
        for (int i = 0; i < _activeCollider.Length; i++)
        {
            _activeCollider[i].TryGetComponent(out _controller[i]);
        }
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
    }

    /// <summary>
    /// 剣の当たり判定を有効にする
    /// </summary>
    /// <param name="colliderIndex">有効にするコライダーID</param>
    /// <param name="activeDuraration">有効時間</param>
    /// <param name="power">攻撃力</param>
    public void ActiveAttackCollider(int colliderIndex, float activeDuraration,int power)
    {
        //ID範囲外時の処理
        if (colliderIndex < 0 || colliderIndex >= _activeCollider.Length) colliderIndex = 0;
        _controller[colliderIndex].AttackPower = power;
        attackColliderCoroutine[colliderIndex] = null;
        attackColliderCoroutine[colliderIndex] = StartCoroutine(ColliderGenerater.GenerateCollider(_activeCollider[colliderIndex], activeDuraration));
    }

    /// <summary>
    /// 剣の軌跡を消す
    /// </summary>
    public void StopEmitting()
    {
        GetComponentInChildren<TrailRenderer>().emitting = false;
    }

    /// <summary>
    /// 剣の軌跡を表示する
    /// </summary>
    public void StartEmitting()
    {
        GetComponentInChildren<TrailRenderer>().emitting = true;
    }

    /// <summary>
    /// 浮かび上がる
    /// </summary>
    public void FloatUp()
    {
        rb.constraints = RigidbodyConstraints.FreezeAll;
        rb.DOMoveY(GameManager.Player.transform.position.y + floatUpDistance, floatUpDuraration)
            .OnComplete(() => rb.constraints = RigidbodyConstraints.FreezeRotation);
    }
}
