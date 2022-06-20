using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveActiveController : MonoBehaviour
{
    [SerializeField] int activeNum;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerTutorialControll>()?.Tutorial(activeNum);
        }
    }
}
