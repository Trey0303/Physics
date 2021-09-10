using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomSpringJoint : MonoBehaviour
{
    //hooke's law
    //formula for spring like behaviour
    //f = -kx
    //F is the force applied to the particle to return it to its rest position
    //k is a constant describing the tightness of the spring
    //x is the displacement of the end of the spring from its resting position

    //damper
    //-bv
    //b is the coefficient of damping(higher values brings it to rest faster)
    //v is the relative velocity of the two connected points


    public Rigidbody rb;
    public Rigidbody connectedRigidbody;
    private Vector3 restPosition;

    //hookes law
    public Vector3 spring;//The spring force used to keep the two objects together.
    public float tightness;//The maximum allowed error between the current spring length and the length defined by minDistance and maxDistance.
    public Vector3 displacement;

    //damper
    public float damper;//The damper force used to dampen the spring force.
    public float maxDistance;//The maximum distance between the bodies relative to their initial distance.
    public float minDistance;//The minimum distance between the bodies relative to their initial distance.

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        tightness = minDistance + maxDistance;

        if (connectedRigidbody == null)//if there is no object connected
        {
            //Debug.Log("No rigidbody connected");
            restPosition = transform.position;
        }
        else//if there is a connect object
        {
            //Debug.Log("there is a rigidbody connected");
            restPosition = connectedRigidbody.transform.position;
        }
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (connectedRigidbody != null)
        {
            restPosition = connectedRigidbody.transform.position;
        }

        //                      collider 1, collider 2, bool ignore
        Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), connectedRigidbody.GetComponent<Collider>(), true);

        //
        displacement = transform.position - restPosition;

        //f = -kx-bv
        spring = -(tightness * displacement) - damper * rb.velocity;

        rb.AddForce(spring);
    }
}
