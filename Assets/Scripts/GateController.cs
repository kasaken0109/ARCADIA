using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    [SerializeField]
    private GameObject m_camera = null;
    void Start()
    {
        StartCoroutine(nameof(ZoomMe));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneLoader.Instance.SceneLoad();
            SoundManager.Instance.PlayMove();
        }
    }

    IEnumerator ZoomMe()
    {
        m_camera?.SetActive(true);
        yield return new WaitForSeconds(1f);
        m_camera?.SetActive(false);
    }
}
