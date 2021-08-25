using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UpdateFixedUpdate : MonoBehaviour
{
    public int framerate = 0;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = framerate;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Update" + Time.deltaTime);
    }

    void FixedUpdate()
    {
        Debug.Log("FixedUpdate" + Time.deltaTime);
    }
}
