using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class TreeController : MonoBehaviour
{
    [SerializeField] GameObject[] m_trees = null;
    [SerializeField] float m_waitTime = 10f;
    // Start is called before the first frame update
    bool IsFirst = true;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && IsFirst)
        {
            foreach (var item in m_trees)
            {
                item.transform.DOLocalRotate(item.transform.rotation.eulerAngles + new Vector3(0, 0, -80f), 4f) ;
            }
            IsFirst = false;
            StartCoroutine(nameof(CountTime));
        }
        //if (!IsFirst && time >= 3) SceneLoader.Instance.SceneLoad(SceneManager.GetActiveScene().name);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!IsFirst && time >= m_waitTime && other.gameObject.CompareTag("Player"))
        {
            SceneLoader.Instance.SceneLoad(SceneManager.GetActiveScene().name);
        }
    }

    float time = 0;
    IEnumerator CountTime()
    {
        time = 0;
        yield return new WaitForSeconds(m_waitTime);
        time = m_waitTime;
    }
}
