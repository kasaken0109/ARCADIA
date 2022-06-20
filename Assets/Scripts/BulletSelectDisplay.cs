using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[System.Serializable]
public struct BulletInformation
{
    public Text _NameDisplay;

    public Image _skill1Display;

    public Image _skill2Display;

}

public class BulletSelectDisplay : MonoBehaviour
{
    [SerializeField]
    private BulletInformation[] _bulletInformations = default;

    [SerializeField]
    private RectTransform _framePosition = default;

    [SerializeField]
    private RectTransform[] _movePosition = default;

    [SerializeField]
    private float _moveDuraration = 0.5f;

    public void BulletInformationInit(Bullet[] bullets)
    {
        for (int i = 0; i < bullets.Length; i++)
        {
            _bulletInformations[i]._NameDisplay.text = bullets[i].Name;
            _bulletInformations[i]._skill1Display.sprite = bullets[i].passiveSkill_1?.ImageBullet;
            _bulletInformations[i]._skill2Display.sprite = bullets[i].passiveSkill_2?.ImageBullet;
        }
    }

    public void MoveSelectFrame(int index)
    {
        _framePosition.DOMove(_movePosition[index].position, _moveDuraration);
    }
}
