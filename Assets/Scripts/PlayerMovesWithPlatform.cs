using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovesWithPlatform : MonoBehaviour
{
    private GameObject target = null;
    private Vector3 offset;
    public GameObject platform;
    void Start()
    {
        target = null;
    }
    void OnTriggerStay(Collider col)
    {
        if(col.tag == "Player")
        {
            target = col.gameObject;
            offset = target.GetComponent<Rigidbody>().position - platform.GetComponent<Rigidbody>().position;
            //offset = target.transform.position - platform.transform.position;

            col.transform.parent = transform;
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            target = null;
            col.transform.parent = null;

        }
    }
    void LateUpdate()
    {
        Debug.Log("Target: " + target);
        if (target != null)
        {
            //target.GetComponent<Rigidbody>().position = target.GetComponent<Rigidbody>().position + offset;
            target.transform.position = target.GetComponent<Rigidbody>().position + offset;
            target.GetComponent<Rigidbody>().position = target.GetComponent<Rigidbody>().position + offset;

        }
    }
}
