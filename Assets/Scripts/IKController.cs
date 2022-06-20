using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class IKController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("左ひざのHint用Transform")]
    private Transform leftKneeTransform;

    [SerializeField]
    [Tooltip("右ひざのHint用Transform")]
    private Transform rightKneeTransform;

    [SerializeField]
    [Tooltip("左手のHint用Transform")]
    private Transform leftHandTransform;

    [SerializeField]
    [Tooltip("IKのウェイトが変化するスピード")]
    private float m_controllSpeed = 0.03f;

    private Animator m_anim = default;

    private float IKValue;

    private float IKFloatValue;

    // Start is called before the first frame update
    void Start()
    {
        IKValue = 1;
        m_anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void OnAnimatorIK(int layerIndex)
    {
        m_anim.SetIKHintPositionWeight(AvatarIKHint.RightKnee, IKValue);
        m_anim.SetIKHintPositionWeight(AvatarIKHint.LeftKnee, IKValue);
        m_anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, IKFloatValue);
        m_anim.SetIKHintPosition(AvatarIKHint.RightKnee, rightKneeTransform.position);
        m_anim.SetIKHintPosition(AvatarIKHint.LeftKnee, leftKneeTransform.position);
        m_anim.SetIKPosition(AvatarIKGoal.LeftHand, leftHandTransform.position);
    }

    public void SetIKValue(float weight)
    {
        if (weight < 0 || weight > 1) IKValue = 0;
        IKValue = weight;
    }

    public void SetHandIK(bool IsActive)
    {
        if (IsActive) StartCoroutine(nameof(SetHand));
        else IKFloatValue = 0;
    }

    IEnumerator SetHand()
    {
        float value = 0;
        while (value < 0.99)
        {
            value += m_controllSpeed;
            IKFloatValue = value;
            yield return null;
        }
    }
}
