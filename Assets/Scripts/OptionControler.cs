using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionControler : MonoBehaviour
{
    [SerializeField]
    private Slider _se;

    [SerializeField]
    private Slider _bgm;
    private void Awake()
    {
        ///音量をsliderの値に反映させる
        _se.value = SoundManager.Instance.GetSEVolume();
        _bgm.value = SoundManager.Instance.GetBGMVolume();
    }

    /// <summary>
    /// 効果音の音量指定関数を呼び出す
    /// </summary>
    public void SetSE()
    {
        SoundManager.Instance.SetSEVolume(_se.value);
    }

    /// <summary>
    /// BGMの音量指定関数を呼び出す
    /// </summary>
    public void SetBGM()
    {
        SoundManager.Instance.SetBGMVolume(_bgm.value);
    }
}
