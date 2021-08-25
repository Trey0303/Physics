using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetLineGizmo : MonoBehaviour
{
    public Transform target;

    void OnDrawGizmosSelected()
    {
        if (target != null)
        {
            // Draws a blue line from this transform to the target
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, target.position);
        }
    }
}
