using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLaserController : MonoBehaviour
{
    [Tooltip("照準のUI")]
    private RectTransform _crosshairUi = null;

    [SerializeField]
    [Tooltip("射程距離")]
    private float _shootRange = 50f;

    [SerializeField]
    private float _speed = 3f;

    [SerializeField]
    [Tooltip("当たるレイヤー")]
    private LayerMask _layerMask = 0;

    [SerializeField]
    [Tooltip("命中した時の音")]
    private AudioClip _hitSound = null;

    [SerializeField]
    [Tooltip("着弾時に発生するエフェクト")]
    private GameObject _effect = null;

    [SerializeField]
    [Tooltip("")]
    private GameObject _frostEffect = null;

    [SerializeField]
    [Tooltip("")]
    private BulletSetting _bulletSetting = default;

    private int damage;

    public int Damage { set { damage = value; } }

    private bool IsSounded = false;
    private bool IsHitSound = false;

    private RaycastHit hit;
    private Vector3 hitPosition;
    private bool EndHit = false;
    private ParticleSystem _particle;
    private Rigidbody _rb;
    GameObject hitObject = null;    // Ray が当たったオブジェクト
    Ray ray;
    Vector3 bulletOrigin;
    // Start is called before the first frame update
    void Start()
    {
        bulletOrigin = transform.position;
        _particle = GetComponentInChildren<ParticleSystem>();
        _rb = GetComponent<Rigidbody>();
        _rb.velocity = new Vector3(0, 0, _speed);
        _crosshairUi = GameManager.Instance.CrosshairUI;
        EndHit = false;
    }

    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(_crosshairUi.position);
        if(!EndHit)hit = RayHit(ray, ref hitObject);
    }

    private RaycastHit RayHit(Ray ray, ref GameObject hitObject)
    {
        EndHit = true;
        bool IsHit = Physics.Raycast(ray, out hit, _shootRange, _layerMask);

        if (IsHit)
        {
            hitPosition = hit.point;    // Ray が当たった場所
            hitObject = hit.collider.gameObject;    // Ray が洗ったオブジェクト

            if (!hitObject) hit = default;
            if (_bulletSetting.HasCriticalDistance) damage = Mathf.CeilToInt((1 - Mathf.Abs(Vector3.Distance(bulletOrigin,hitPosition) - _bulletSetting.CriticalDistance) / _bulletSetting.CriticalDistance * _bulletSetting.ReduceDamagePerDistance) * damage);
            
            if (hitObject.CompareTag("Enemy") || hitObject.CompareTag("Item"))
            {
                IsSounded = !IsSounded;
                hitObject.GetComponentInParent<IDamage>().AddDamage(damage);
                Instantiate(_effect, hitPosition, Quaternion.identity);
                if(_frostEffect)Instantiate(_frostEffect, hitPosition, Quaternion.identity, hitObject.transform);
                Destroy(this.gameObject,0.5f);
            }
            if (!IsHitSound)
            {
                PlayHitSound(hitPosition);  // レーザーが当たった場所でヒット音を鳴らす
                SoundManager.Instance.PlayFrost();
                IsHitSound = true;
            }
        }
        return hit;
    }

    /// <summary>
    /// ヒット音を鳴らす
    /// </summary>
    /// <param name="position">音を鳴らす場所</param>
    private void PlayHitSound(Vector3 position)
    {
        if (_hitSound) AudioSource.PlayClipAtPoint(_hitSound, position, 0.1f);
    }
}
