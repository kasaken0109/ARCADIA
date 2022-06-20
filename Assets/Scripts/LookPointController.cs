using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// キャラクターの姿勢変更時に見るポイントを変更する
/// </summary>
public class LookPointController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("ズームする距離")]
    float m_zoomDistance = 0.5f;

    Animator m_anim;

    private void Start()
    {
        m_anim = GetComponentInParent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(0, 0, m_anim.GetFloat("Speed") > 12 ? m_zoomDistance : 0);
    }
}
