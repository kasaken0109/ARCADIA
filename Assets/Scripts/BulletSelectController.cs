using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class BulletSelectController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("強調表示するUIに付属するクラス")]
    private UISelecter[] _selecters;

    [SerializeField]
    [Tooltip("射撃を管理するクラス")]
    private BulletFire m_bulletFire = default;

    [SerializeField]
    [Tooltip("弾を設定するリスト")]
    private List<Bullet> m_IDs;

    [SerializeField]
    [Tooltip("初期化時の弾のリスト")]
    private BulletList _init;

    [SerializeField]
    private GameObject[] UI;

    [SerializeField]
    private Animator _anim;

    private BulletSelectDisplay _bulletDisplay;

    private int equipID = 0;

    private bool IsPreScroll = false;

    private bool IsPush = false;


    public List<Bullet> MyBullet { set { m_IDs = value; } }



    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent(out _bulletDisplay);
        EquipmentInit();

        _bulletDisplay.BulletInformationInit(EquipmentManager.Instance.Equipments);
        EquipBullets();
        SelectBullet(equipID);//弾の選択状態の初期化
        _bulletDisplay.MoveSelectFrame(equipID);
        IsPreScroll = false;
    }

    private void EquipmentInit()
    {
        var equip = EquipmentManager.Instance.Equipments;
        for (int i = 0; i < equip.Length; i++)
        {
            equip[i] = equip[i] == null ? _init.Bullets[i] : equip[i];
        }
    }

    public void OpenBulletMenu()
    {
        IsPush = !IsPush;
        _anim.SetBool("IsPush", IsPush);
        Vector3 scale = IsPush ? Vector3.zero : Vector3.one;
        foreach (var item in UI) item.transform.localScale = scale;
    }

    public void SelectBullet(float scrollValue)
    {
        equipID = scrollValue > 0 ? (equipID == 0 ? 2 : equipID - 1) : (equipID == 2 ? 0 : equipID + 1);
        SelectBullet(equipID);
        _bulletDisplay.MoveSelectFrame(equipID);
        if(!m_IDs[equipID]) SelectBullet(scrollValue);
    }

    public void SelectBullet(int bullet)
    {
        m_bulletFire.EquipBullet(m_IDs[bullet]);
    }

    /// <summary>
    /// EquipmentManegerに設定されている装備を選択
    /// </summary>
    private void EquipBullets()
    {
        var equipM = EquipmentManager.Instance;
        for (int i = 0;i < m_IDs.Count;i++) m_IDs[i] = equipM.Equipments[i] ? equipM.Equipments[i] : _init.Bullets[i];
    }

    private void OnDestroy()
    {
        equipID = 0;
    }
}
