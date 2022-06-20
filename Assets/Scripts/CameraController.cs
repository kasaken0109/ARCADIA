using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Linq;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("ロックオンカメラ")]
    CinemachineVirtualCamera m_lockOnCamera = default;

    [SerializeField]
    [Tooltip("プレイヤーのカメラ")]
    CinemachineVirtualCamera m_playerCamera = default;

    private GameObject[] m_lockOnTargets;

    private GameObject m_player;

    private static int cameraPriority = 15;

    private int lockOnId = 0;

    private CinemachinePOV _cinemachinePOV;

    // Start is called before the first frame update
    void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
        m_lockOnTargets = SetSearchTarget("Enemy");
        _cinemachinePOV = m_playerCamera.GetCinemachineComponent<CinemachinePOV>();
        lockOnId = 0;
    }

    private void OnEnable()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
        m_lockOnTargets = SetSearchTarget("Enemy");
        _cinemachinePOV = m_playerCamera.GetCinemachineComponent<CinemachinePOV>();
        lockOnId = 0;
        ResetCam();
    }

    private void ResetCam()
    {
        _cinemachinePOV.m_VerticalAxis.Value = 0;
        _cinemachinePOV.m_HorizontalAxis.Value = -180;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            LockON();
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            m_playerCamera.transform.LookAt(m_lockOnTargets[lockOnId].transform);
            m_playerCamera.GetCinemachineComponent<CinemachinePOV>().GetRecenterTarget();
            ResetCam();
            m_lockOnCamera.Priority = 0;
            lockOnId = 0;
            m_lockOnTargets = SetSearchTarget("Enemy");
        }
    }

    private void LockON()
    {
        lockOnId = lockOnId == m_lockOnTargets.Length - 1 ? 0 : lockOnId + 1;
        m_lockOnCamera.LookAt = m_lockOnTargets[lockOnId].transform;
        m_lockOnCamera.Priority = cameraPriority;
    }

    ///// <summary>
    ///// 特定のコンポーネントを持つオブジェクを検索する、重いのであまり多用しない
    ///// </summary>
    ///// <typeparam name="T"></typeparam>
    ///// <returns></returns>
    //public GameObject[] SetSearchTarget<T>() where T : Component
    //{
    //    return FindObjectsOfType<GameObject>();
    //}

    public GameObject[] SetSearchTarget(string tag)
    {
        var target = GameObject.FindGameObjectsWithTag(tag);
        return target.Where(ta => ta.GetComponent<Rigidbody>()) .OrderBy(t => Vector3.Distance(m_player.transform.position,t.transform.position)).ToArray();
    }
}
