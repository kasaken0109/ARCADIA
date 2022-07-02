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

    static SoundManager instance = null;

    AudioSource seSource;

    AudioSource bgmSource;

    SoundAssets _soundAssets = null;

    bool IsInint = true;

    void Awake()
    {
        
        if (IsInint)
        {
            GameObject ins = (GameObject)Resources.Load("SoundAssets");
            _soundAssets = ins.GetComponent<SoundAssetInformation>().SoundAssets;
            var bgm = new GameObject("BGM");
            bgm.transform.SetParent(gameObject.transform);
            bgmSource = bgm.AddComponent<AudioSource>();
            bgmSource.volume = 0.1f;
            bgmSource.loop = true;
            bgmSource.outputAudioMixerGroup= _soundAssets._audioMixerBGM;
            seSource = GetComponent<AudioSource>();
            seSource.outputAudioMixerGroup = _soundAssets._audioMixerSE;
            DontDestroyOnLoad(gameObject);
            SetSEVolume(_soundAssets.m_seVolume);
            SetBGMVolume(_soundAssets.m_bgmVolume);
            IsInint = false;
        }
    }

    public void PlayClick(int index = 0)
    {
        if (index > _soundAssets.m_click.Length) index = 0;
        seSource.PlayOneShot(_soundAssets.m_click[index]);
    }

    public void PlayClick(AudioClip clip)
    {
        seSource.PlayOneShot(clip);
    }

    public void PlayRoar()
    {
        seSource.PlayOneShot(_soundAssets.m_roar);
    }
    public void PlayKick()
    {
        seSource.PlayOneShot(_soundAssets.m_kick);
    }

    public void PlayMove()
    {
        seSource.PlayOneShot(_soundAssets.m_move);
    }

    public void PlayHeal()
    {
        seSource.PlayOneShot(_soundAssets.m_heal);
    }

    public void PlayFrostWall()
    {
        seSource.PlayOneShot(_soundAssets.m_frostWall);
    }
    public void PlayFireB()
    {
        seSource.PlayOneShot(_soundAssets.m_fireB);
    }

    public void PlayDodge()
    {
        seSource.PlayOneShot(_soundAssets.m_dodge);
    }

    public void PlayFrost()
    {
        seSource.PlayOneShot(_soundAssets.m_god);
    }
    public void PlayShoot()
    {
        seSource.PlayOneShot(_soundAssets.m_shoot);
    }

    public void PlayCharge()
    {
        seSource.PlayOneShot(_soundAssets.m_charge);
    }

    public void PlayEmptyBullet()
    {
        seSource.PlayOneShot(_soundAssets.m_emptyBullet);
    }

    public void StopSE()
    {
        seSource.Pause();
    }

    public void PlayHit(AudioClip hit)
    {
        seSource.PlayOneShot(hit);
    }
    public void PlayPlayerHit()
    {
        seSource.PlayOneShot(_soundAssets.m_playerHit);
    }

    public void PlayHit(AudioClip hit, Vector3 pos)
    {
        AudioSource.PlayClipAtPoint(hit, pos, ConvertDbToVolume()/10);
    }

    public void PlayBGM(AudioClip bgm)
    {
        if (bgmSource.clip) bgmSource.Stop();
        bgmSource.clip = bgm;
        bgmSource.Play();
    }

    public void StopBGM()
    {
        bgmSource.Pause();
    }

    public void ResumeBGM()
    {
        bgmSource.UnPause();
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
        _soundAssets._audioMixerSE.audioMixer.SetFloat("MasterVolume", volume);
    }

    /// <summary>
    /// BGMの音量を調整する
    /// </summary>
    /// <param name="volume">音量</param>
    public void SetBGMVolume(float volume)
    {
        _soundAssets._audioMixerBGM.audioMixer.SetFloat("MasterVolume", volume);
    }

    public float GetSEVolume()
    {
        float volume;
        _soundAssets._audioMixerSE.audioMixer.GetFloat("MasterVolume", out volume);
        return volume;
    }

    public float GetBGMVolume()
    {
        float volume;
        _soundAssets._audioMixerBGM.audioMixer.GetFloat("MasterVolume", out volume);
        return volume;
    }
}
