using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentPresenter : MonoBehaviour
{
    [SerializeField]
    private Image _bulletImage = default;

    [SerializeField]
    private Text _bulletName = default;

    [SerializeField]
    private Image _skill1Image = default;

    [SerializeField]
    private Text _skill1Name = default;

    [SerializeField]
    private Image _skill2Image = default;

    [SerializeField]
    private Text _skill2Name = default;

    [Header("初期化データ")]
    [Header("―――――")]
    [SerializeField]
    Sprite _defaultImage = default;

    [SerializeField]
    private string _defaultText = "未設定";

    // Start is called before the first frame update
    void Start()
    {
        SetInformation(EquipmentManager.Instance.GetEquipID - 1);
    }
    public void SetInformation(int id)
    {
        var instance = EquipmentManager.Instance.Equipments[id];
        _bulletImage.sprite = instance ? instance.Image : _defaultImage;
        _bulletName.text = instance ? instance.Name : _defaultText;
        _skill1Name.text = instance ? instance.passiveSkill_1 ? instance.PassiveSkill1.SkillName : _defaultText : _defaultText;
        _skill1Image.sprite = instance ? instance.passiveSkill_1 ? instance.PassiveSkill1.ImageBullet : _defaultImage : _defaultImage;
        _skill2Name.text = instance ? instance.PassiveSkill2 ? instance.PassiveSkill2.SkillName : _defaultText : _defaultText;
        _skill2Image.sprite = instance ? instance.PassiveSkill2 ? instance.PassiveSkill2.ImageBullet :_defaultImage : _defaultImage;
    }
}
