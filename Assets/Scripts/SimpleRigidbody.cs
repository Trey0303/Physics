using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRigidbody : MonoBehaviour
{
    public Vector3 velocity;

    public bool useGravity;

    public enum UpdateMode
    {
        None,
        Update,
        FixedUpdate
    }

    public UpdateMode updateMode;

    // Update is called once per frame
    void Update()
    {
        if(updateMode != UpdateMode.Update) { return; }

        UpdateVelocity();
        UpdatePosition();
    }
    void FixedUpdate()
    {
        if(updateMode != UpdateMode.FixedUpdate) { return; }

        UpdateVelocity();
        UpdatePosition();

    }

    private void UpdateVelocity()
    {
        if (useGravity)
        {
            velocity += Physics.gravity * Time.deltaTime;
        }
    }

    private void UpdatePosition()
    {
        transform.position += velocity * Time.deltaTime;
    }
}
