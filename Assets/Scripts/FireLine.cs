using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class FireLine : MonoBehaviour
{
    #region SerializeGameObject
    [SerializeField]
    GameObject m_muzzle = null;
    [SerializeField]
    GameObject m_effect = null;
    [SerializeField]
    GameObject m_frostEffect = null;
    [SerializeField]
    GameObject m_bullet = null;
    [SerializeField]
    GameObject m_shield = null;
    #endregion
    [SerializeField]
    [Tooltip("LineRenderer 兼 Line の出発点")]
    private LineRenderer m_line = null;

    [SerializeField]
    [Tooltip("リロード時間")]
    private float m_seconds = 2f;

    [SerializeField]
    [Tooltip("射程距離")]
    private float m_shootRange = 15f;
    /// <summary>当たるレイヤー</summary>
    [SerializeField]
    private LayerMask m_layerMask = 0;
    /// <summary>発射した時の音</summary>
    [SerializeField]
    private AudioClip m_shootSound = null;
    /// <summary>命中した時の音</summary>
    [SerializeField] 
    private AudioClip m_hitSound = null;
    [SerializeField]
    private AudioClip m_reloadSound = null;
    [SerializeField]
    private AudioClip m_airshoot = null;
    
    [SerializeField]
    private Text m_text;
    [SerializeField]
    private float m_chargeTime = 3f;
    [SerializeField]
    private int m_attackpower = 10;
    /// <summary>マガジン内の弾数</summary>
    public int m_bulletNum;
    
    public int m_bulletMaxNum = 4;
    GameObject m_reload = null;
    GameObject m_particleMuzzle = null;
    GameObject m_textBox = null;
    GameObject m_fireLine = null;
    PlayerControll m_pControll;
    public Volume m_Volume;


    #region Bool
    bool IsSounded = false;
    bool IsHitSound = false;
    bool IsEndHit = false;
    bool CanShoot = true;
    bool IsCreate = false;
    bool IsReload = false;
    #endregion
    /// <summary>照準</summary>
    RectTransform m_crosshairUi = null;
    Transform m_shieldSpawn = null;
    ShieldDisplayController m_shieldDisplay = null;
    Vector3 hitPosition;
    ParticleSystem particleSystem;
    AudioSource audio;
    Animator m_anim;
    MotionBlur motionBlur;
    

    private void Awake()
    {
        m_bulletNum = PlayerPrefs.GetInt("Bullet1");
        m_fireLine = GameObject.Find("FireLine");
        StopCoroutine(nameof(WaitSeconds));
    }
    void Start()
    {
        GameObject.Find("RifleImage").GetComponent<Image>().fillAmount = 1;
        m_bulletNum = PlayerPrefs.GetInt("Bullet1");

        m_text = m_textBox.GetComponent<Text>();
        m_text.text = m_bulletNum + "/" + m_bulletMaxNum;

        m_muzzle = GameObject.FindWithTag("Muzzle");
        m_line = m_muzzle.GetComponent<LineRenderer>();
        m_reload?.SetActive(false);
        audio = GetComponent<AudioSource>();
        m_crosshairUi = GameObject.Find("Targetaim").GetComponent<RectTransform>();

        m_pControll = GetComponentInParent<PlayerControll>();
        m_anim = GetComponentInParent<Animator>();

        VolumeProfile profile = m_Volume.sharedProfile;
        foreach (var item in profile.components)
        {
            if (item as MotionBlur)
            {
                motionBlur = item as MotionBlur;
            }
        }
    }

    private void OnEnable()
    {
        m_bulletNum = PlayerPrefs.GetInt("Bullet1");
        m_fireLine = GameObject.Find("FireLine");
        m_muzzle = GameObject.FindWithTag("Muzzle");
        m_particleMuzzle = GameObject.Find("BulletSpawn");
        m_line = m_muzzle.GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (Time.timeScale == 0) return;
        if(!IsReload)m_text.text = m_bulletNum + "/" + m_bulletMaxNum;

        Ray ray = Camera.main.ScreenPointToRay(m_crosshairUi.position);
        transform.LookAt(new Vector3(Camera.main.transform.position.x,Camera.main.transform.position.y,int.MaxValue));
        Vector3 pos = Camera.main.ScreenToWorldPoint(m_crosshairUi.position);
        RaycastHit hit;

        GameObject hitObject = null;    // Ray が当たったオブジェクト

        if (Input.GetButtonDown("Fire2"))
        {
            if (m_bulletNum >= 1) StartCoroutine(nameof(Fireline));
            else if(!IsReload)
            {
                //音の処理
                m_reload?.SetActive(true);
                Reload();
            }
            //if (m_shieldDisplay.ShieldValue < 1 && m_bulletNum == 4) StartCoroutine(nameof(Fireline));
        }
        else if (Input.GetButtonUp("Fire2"))
        {
            SoundManager.Instance.StopSE();
            StopCoroutine(nameof(Fireline));
            if (IsCreate)
            {
                m_bulletNum--;
                IsCreate = false;
            }
            else
            {
                if (m_bulletNum >= 1)
                {
                    SoundManager.Instance.PlayShoot();
                    Instantiate(m_bullet,m_particleMuzzle.transform.position,Camera.main.transform.rotation);
                    m_bulletNum -= 1;
                    bool IsHit = Physics.Raycast(ray, out hit, m_shootRange, m_layerMask);

                    if (IsHit)
                    {
                        
                        hitPosition = hit.point;    // Ray が当たった場所
                        hitObject = hit.collider.gameObject;    // Ray が洗ったオブジェクト

                        if (hitObject)
                        {
                            if (hitObject.tag == "Enemy" || hitObject.tag == "Item")
                            {
                                IsSounded = !IsSounded ? true : false;
                                hitObject.GetComponentInParent<IDamage>().AddDamage(m_attackpower);
                                Instantiate(m_effect, hitPosition, Quaternion.identity);
                                Instantiate(m_frostEffect, hitPosition, Quaternion.identity,hitObject.transform);
                            }
                            if (!IsHitSound)
                            {
                                PlayHitSound(hitPosition);  // レーザーが当たった場所でヒット音を鳴らす
                                SoundManager.Instance.PlayFrost();
                                IsHitSound = true;
                            }
                        }
                    }
                }
            }
            
        }

        if (Input.GetButtonDown("Reload") && !IsReload) Reload();

    }
    IEnumerator Fireline()
    {
        float time = 0;
        IsCreate = false;
        yield return new WaitForSeconds(1f);
        SoundManager.Instance.PlayCharge();
        yield return new WaitForSeconds(0.5f);
        IsCreate = true;
        m_pControll.StepForward(-10);
        m_anim.Play("Step");
        
    }

    void OnDestroy()
    {
        PlayerPrefs.SetInt("Bullet1", m_bulletNum);
        PlayerPrefs.Save();
        StopCoroutine(nameof(WaitSeconds));
        m_text.text = m_bulletNum + "/" + m_bulletMaxNum;
    }

    /// <summary>
    /// 射撃音を鳴らす
    /// </summary>
    /// <param name="position">音を鳴らす場所</param>
    void PlayShootSound(AudioClip audioClip)
    {
        audio?.PlayOneShot(m_shootSound);
    }

    /// <summary>
    /// ヒット音を鳴らす
    /// </summary>
    /// <param name="position">音を鳴らす場所</param>
    void PlayHitSound(Vector3 position)
    {
        if (m_hitSound) AudioSource.PlayClipAtPoint(m_hitSound, position, 0.1f);
    }

    /// <summary>
    /// Line Renderer を使ってレーザーを描く
    /// </summary>
    /// <param name="destination">レーザーの終点</param>
    void DrawLaser(Vector3 destination)
    {
        Vector3[] positions = { m_line.transform.position, destination };   // レーザーの始点は常に Muzzle にする
        m_line.positionCount = positions.Length;   // Line を終点と始点のみに制限する
        if (m_bulletNum >= 1) m_line.SetPositions(positions);
    }
    void Reload()
    {
        AudioSource.PlayClipAtPoint(m_reloadSound, this.transform.position);
        m_text.text ="リロード中";
        StopCoroutine(nameof(WaitSeconds));
        StartCoroutine(nameof(WaitSeconds));
    }

    IEnumerator WaitSeconds()
    {
        m_reload?.SetActive(false);
        var i = GameObject.Find("RifleImage").GetComponent<Image>();
        i.fillAmount = 0;
        IsReload = true;
        while(i.fillAmount <= 0.99f)
        {
            i.fillAmount += 0.01f;
            yield return new WaitForSeconds(m_seconds / 100);
        }
        m_bulletNum = m_bulletMaxNum;
        IsReload = false;
    }
}
