using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMover : MonoBehaviour
{
    public GameObject child;

    public Vector3 launchVelocity;//inital velocity
    public float gravity;//
    public float timeSinceLaunch;// time interval

    // Start is called before the first frame update
    void Start()
    {
        timeSinceLaunch = Time.time;

        //overwrites local position of child gameObject with launchVelocity, Gravity, timeSinceLaunch
        child.transform.localPosition = new Vector3(launchVelocity.x * timeSinceLaunch,gravity,launchVelocity.z);
    }

    // Update is called once per frame
    void Update()
    {
        

        //As time since launch increases, the child game object should be moved along its launch trajectory.

        timeSinceLaunch++;

    }
}
