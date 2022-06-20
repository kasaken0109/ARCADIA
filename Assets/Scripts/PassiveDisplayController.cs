using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassiveDisplayController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("エフェクトの発生ポイント")]
    Transform m_effectBirthPoint = default;
    [SerializeField]
    private Image[] effectDisplays;
    [SerializeField]
    private Sprite m_defaultImage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDisplay(ref PassiveSkill passiveSkill)
    {
        var obj = Instantiate(passiveSkill.Effect, m_effectBirthPoint.position, m_effectBirthPoint.rotation, m_effectBirthPoint);
        var particle = obj.GetComponent<ParticleSystem>().main;
        particle.duration = passiveSkill.EffectableTime;
    }
}
