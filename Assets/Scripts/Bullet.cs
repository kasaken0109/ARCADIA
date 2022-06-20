using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Bullets/Create Bullet")]
public class Bullet : ScriptableObject
{
    [SerializeField]
    private GameObject m_bullet;

    [SerializeField]
    private int m_damage = 1;

    [SerializeField]
    private float m_delay = 0.2f;

    [SerializeField]
    [Tooltip("消費するスタンス値")]
    [Range(-1, 1)]
    private float m_consumeStanceValue;

    [Tooltip("パッシブSkill1")]
    public PassiveSkill passiveSkill_1 = default;

    [Tooltip("パッシブSkill2")]
    public PassiveSkill passiveSkill_2 = default;

    [SerializeField]
    private BulletType m_bulletType = default;

    [SerializeField]
    private int m_bulletID = 0;//後々IDをバレットセット画面で設定するようにする

    [SerializeField]
    private string m_name = default;

    [SerializeField]
    private string m_explainText = default;

    [SerializeField]
    private Sprite m_image = default;



    public GameObject MyBullet => m_bullet;

    public int Damage => m_damage;

    public float ConsumeStanceValue => m_consumeStanceValue;

    public PassiveSkill PassiveSkill1 { get =>passiveSkill_1;  set { passiveSkill_1 = value; } }

    public PassiveSkill PassiveSkill2 { get => passiveSkill_2; set { passiveSkill_2 = value; } }


    public BulletType BulletType => m_bulletType;

    public int BulletID => m_bulletID;

    public float Delay => m_delay;

    public string Name => m_name;

    public Sprite Image => m_image;

    public string ExplainText => m_explainText;
}
