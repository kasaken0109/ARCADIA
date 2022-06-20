using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// 氷エフェクトの出現、消滅をコントロールする
/// </summary>
public class ActiveController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("エフェクトの表示時間")]
    private float m_activeTime = 1;
    // Start is called before the first frame update
    private void OnEnable()
    {
        FrostActive();   
    }

    /// <summary>
    /// 鉛直方向に上昇後、一定時間静止し、下降する
    /// </summary>
    public void FrostActive()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(transform.DOMoveY(transform.position.y + 0.5f, 0.2f))
                .AppendInterval(1f)
                .Append(transform.DOMoveY(transform.position.y - 2f, 3f))
                .AppendCallback(() =>
                {
                    Destroy(this.gameObject);
                });
        sequence.Play();
    }
}
