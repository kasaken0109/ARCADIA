using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 敵の生成を行う
/// </summary>
public class SpawnManeger : MonoBehaviour
{
    [SerializeField]
    private Transform [] m_spawnPoint = null;
    [SerializeField]
    private GameObject enemy = null;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < m_spawnPoint.Length; i++)
        {
            Instantiate(enemy, m_spawnPoint[i].position, m_spawnPoint[i].rotation);
        }
    }
}
