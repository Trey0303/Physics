using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchController : MonoBehaviour
{
    //array
    public GameObject[] gameObjects;

    public Vector3 changeVelocity;

    // Start is called before the first frame update
    void Start()
    {
        gameObjects[0].GetComponent<SimpleRigidbody>().velocity = changeVelocity;
        gameObjects[1].GetComponent<SimpleRigidbody>().velocity = changeVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
