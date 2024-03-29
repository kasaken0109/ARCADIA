﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// 障害物にキックが当たった時の挙動を制御する
/// </summary>
public class JumpUpController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("上昇量")]
    float _upAmount = 5f;


    Rigidbody _rb;
    AttackcolliderController attackcolliderController;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponentInParent<Rigidbody>();
        TryGetComponent(out attackcolliderController);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(attackcolliderController.AttackPower != 0)_rb.DOMove(transform.position + transform.up * _upAmount, 0.5f);
    }
}
