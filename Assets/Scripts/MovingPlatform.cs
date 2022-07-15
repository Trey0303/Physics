using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform[] waypoints;

    public int curWaypoint;

    public float speed = 3;

    //public Rigidbody platformRB;

    // Start is called before the first frame update
    void Start()
    {
        curWaypoint = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        

        if (Vector3.Distance(waypoints[curWaypoint].transform.position, transform.position) <= 0)
        {
            Debug.Log("NEXT WAYPOINT");
            curWaypoint++;

            if (curWaypoint >= waypoints.Length)//if on the last waypoint
            {
                curWaypoint = 0;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoints[curWaypoint].transform.position,(speed * Time.deltaTime));
        }
        
        //for (int i = 0; i < waypoints.Length; i++)
        //{

        //}

    }

    

    void OnDrawGizmosSelected()
    {
        if (waypoints[curWaypoint] != null)
        {
            // Draws a blue line from this transform to the target
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, waypoints[curWaypoint].position);
        }
    }
}
