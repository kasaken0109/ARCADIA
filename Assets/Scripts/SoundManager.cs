using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Linq;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance {
        get
        {
            var target = FindObjectOfType<SoundManager>();
            if (target)
            {
                instance = target;
            }
            else
            {
                var gm = GameObject.Find("GM");
                if (!gm) gm = new GameObject("GM");
                instance = gm.AddComponent<SoundManager>();
                DontDestroyOnLoad(instance);
            }
            return instance;
        }
    }

    private static SoundManager instance = null;

    [SerializeField]
    private AudioMixer _audioMixer = default;

    [SerializeField] AudioClip[] m_click;
    [SerializeField] AudioClip m_frostWall;
    [SerializeField] AudioClip m_playerHit;
    [SerializeField] AudioClip m_roar;
    [SerializeField] AudioClip m_shoot;
    [SerializeField] AudioClip m_heal;
    [SerializeField] AudioClip m_fireB;
    [SerializeField] AudioClip m_move;
    [SerializeField] AudioClip m_dodge;
    [SerializeField] AudioClip m_kick;
    [SerializeField] AudioClip m_blizzard;
    [SerializeField] AudioClip m_god;
    [SerializeField] AudioClip m_charge;
    [SerializeField, Range(-80, 20)] float m_seVolume = -60f;
    [SerializeField, Range(-80, 20)] float m_bgmVolume = -60f;
    AudioSource source;

    bool IsInint = true;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        if (IsInint)
        {
            DontDestroyOnLoad(gameObject);
            SetSEVolume(m_seVolume);
            SetBGMVolume(m_bgmVolume);
            IsInint = false;
        }
        var button = FindObjectsOfType<Button>();
        button.ToList().ForEach(x => x.onClick.AddListener(() => PlayClick()) );
    }

    public void PlayClick()
    {
        source.PlayOneShot(m_click[0]);
    }

    public void PlayClick(int index)
    {
        if (index > m_click.Length) index = 0;
        source.PlayOneShot(m_click[index]);
    }

    public void PlayRoar()
    {
        source.PlayOneShot(m_roar);
    }
    public void PlayKick()
    {
        source.PlayOneShot(m_kick);
    }

    public void PlayMove()
    {
        source.PlayOneShot(m_move);
    }

    public void PlayHeal()
    {
        source.PlayOneShot(m_heal);
    }

    public void PlayFrostWall()
    {
        source.PlayOneShot(m_frostWall);
    }
    public void PlayFireB()
    {
        source.PlayOneShot(m_fireB);
    }

    public void PlayDodge()
    {
        source.PlayOneShot(m_dodge);
    }

    public void PlayFrost()
    {
        source.PlayOneShot(m_god);
    }
    public void PlayShoot()
    {
        source.PlayOneShot(m_shoot);
    }

    public void PlayCharge()
    {
        source.PlayOneShot(m_charge);
    }

    public void StopSE()
    {
        source.Pause();
    }

    public void PlayHit(AudioClip hit)
    {
        source.PlayOneShot(hit);
    }
    public void PlayPlayerHit()
    {
        source.PlayOneShot(m_playerHit);
    }

    public void PlayHit(AudioClip hit, Vector3 pos)
    {
        AudioSource.PlayClipAtPoint(hit, pos, ConvertDbToVolume()/10);
    }

    public float ConvertDbToVolume()
    {
        return Mathf.Abs(GetSEVolume()-80) / 100;
    }

    /// <summary>
    /// SEの音量を調整する
    /// </summary>
    /// <param name="volume">音量</param>
    public void SetSEVolume(float volume)
    {
        _audioMixer.SetFloat("SEVolume", volume);
        Debug.Log("SetSE");
    }

    /// <summary>
    /// BGMの音量を調整する
    /// </summary>
    /// <param name="volume">音量</param>
    public void SetBGMVolume(float volume)
    {
        _audioMixer.SetFloat("BGMVolume", volume);
        Debug.Log("SetBGM");
    }

    public float GetSEVolume()
    {
        float volume;
        _audioMixer.GetFloat("SEVolume", out volume);
        return volume;
    }

    public float GetBGMVolume()
    {
        float volume;
        _audioMixer.GetFloat("BGMVolume", out volume);
        return volume;
    }
}
