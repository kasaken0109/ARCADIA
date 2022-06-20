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
        ///���ʂ�slider�̒l�ɔ��f������
        _se.value = SoundManager.Instance.GetSEVolume();
        _bgm.value = SoundManager.Instance.GetBGMVolume();
    }

    /// <summary>
    /// ���ʉ��̉��ʎw��֐����Ăяo��
    /// </summary>
    public void SetSE()
    {
        SoundManager.Instance.SetSEVolume(_se.value);
    }

    /// <summary>
    /// BGM�̉��ʎw��֐����Ăяo��
    /// </summary>
    public void SetBGM()
    {
        SoundManager.Instance.SetBGMVolume(_bgm.value);
    }
}
