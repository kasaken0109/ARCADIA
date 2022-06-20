using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの音の処理を管理する
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] m_sounds;

    [SerializeField]
    private Animator m_anim = default;

    [SerializeField]
    private float pitchRange = 0.1f;

    private AudioSource source;
    
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponents<AudioSource>()[0];
    }

    public void PlayFootstepSE()
    {
        if (m_anim.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
        {
            source.PlayOneShot(m_sounds[0]);
        }
        else if (m_anim.GetCurrentAnimatorStateInfo(0).IsName("Run"))
        {
            source.PlayOneShot(m_sounds[1]);
        }
    }

    public void PlayJumpSE()
    {
        source.PlayOneShot(m_sounds[2]);
    }

    public void PlaySlashSE()
    {
        source.PlayOneShot(m_sounds[3]);
    }

    public void PlaySpecialSlashSE()
    {
        source.PlayOneShot(m_sounds[4]);
    }
}
