using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlipDamageController : MonoBehaviour
{
    [SerializeField] int m_maxSlipDamage = 10;
    BoxCollider collider;
    GameObject hitObj;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hitObj = other.gameObject;
            if (!IsStarted)
            {
                StartCoroutine(nameof(SlipDamage));
                IsStarted = true;
            }
        }
    }

    bool IsStarted;
    IEnumerator SlipDamage()
    {
        while (true)
        {
            float distance = Vector2.Distance(new Vector2(hitObj.transform.position.x, hitObj.transform.position.z), new Vector2(collider.center.x, collider.center.z));
            float size = Vector2.Distance(new Vector2(collider.center.x, collider.center.z), new Vector2(collider.bounds.max.x, collider.bounds.max.z));
            int damage = (int)((m_maxSlipDamage * distance) / size);
            //if(distance / size < 0.5f)
            hitObj.GetComponentInParent<PlayerManager>().AddDamage(damage);
            yield return new WaitForSeconds(1.5f);
        }
    }
}
