using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchWeaponTips : MonoBehaviour
{
    PlayerTutorialControll controll;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            controll = other.gameObject.GetComponent<PlayerTutorialControll>();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            controll.Tutorial(5);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            controll.Tutorial(6);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            controll.Tutorial(7);
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            controll.Tutorial(5);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            controll.Tutorial(6);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            controll.Tutorial(7);
        }
    }
}
