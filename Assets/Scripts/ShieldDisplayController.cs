using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ShieldDisplayController : MonoBehaviour
{
    [SerializeField] Image[] m_gauge;
    Image m_shieldImage = null;
    private float shieldValue;
    private bool canCharge = true;

    public bool CanCharge { get=> canCharge; }


    public float ShieldValue
    {
        get { return shieldValue; }
        set { shieldValue = value; }
    }



    // Start is called before the first frame update
    void Start()
    {
        m_shieldImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_shieldImage.fillAmount > 0) canCharge = false;
        shieldValue = m_shieldImage.fillAmount;
    } 
    public void ChangeValues(float value)
    {
        if (value == 0) StartCoroutine(nameof(WaitCoolDown));
        
        DOTween.To(
                () => m_shieldImage.fillAmount, // getter
                x => m_shieldImage.fillAmount = x, // setter
                (float)(float)value, // ターゲットとなる値
                1f  // 時間（秒）
                ).SetEase(Ease.OutCubic);
        //DOTween.To(
        //        () => m_gauge[0].fillAmount, // getter
        //        x => m_gauge[0].fillAmount = x, // setter
        //        (float)(float)value, // ターゲットとなる値
        //        1f  // 時間（秒）
        //        ).SetEase(Ease.OutCubic);
        //DOTween.To(
        //       () => m_gauge[1].fillAmount, // getter
        //       x => m_gauge[1].fillAmount = x, // setter
        //       (float)(float)value, // ターゲットとなる値
        //       1f  // 時間（秒）
        //       ).SetEase(Ease.OutCubic);
    }
    public void ChangeValues(float value, float time)
    {
        DOTween.To(
                () => m_shieldImage.fillAmount, // getter
                x => m_shieldImage.fillAmount = x, // setter
                (float)(float)value, // ターゲットとなる値
                time  // 時間（秒）
                ).SetEase(Ease.OutCubic);
        //DOTween.To(
        //        () => m_gauge[0].fillAmount, // getter
        //        x => m_gauge[0].fillAmount = x, // setter
        //        (float)(float)value, // ターゲットとなる値
        //        time  // 時間（秒）
        //        ).SetEase(Ease.OutCubic);
        //DOTween.To(
        //       () => m_gauge[1].fillAmount, // getter
        //       x => m_gauge[1].fillAmount = x, // setter
        //       (float)(float)value, // ターゲットとなる値
        //       time  // 時間（秒）
        //       ).SetEase(Ease.OutCubic);
    }

    IEnumerator WaitCoolDown()
    {
        canCharge = false;
        yield return new WaitForSeconds(2);
        canCharge = true;
    }

    public void FullCharge(float time)
    {
        DOTween.To(
                () => m_shieldImage.fillAmount, // getter
                x => m_shieldImage.fillAmount = x, // setter
                1, // ターゲットとなる値
                time  // 時間（秒）
                ).SetEase(Ease.OutCubic);
    }
}

    
