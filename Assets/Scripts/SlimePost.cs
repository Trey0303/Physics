using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimePost : MonoBehaviour
{
    //public SlimeMotor slimeMotor;

    public Transform targetPost;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Slime")
        {
            //switch slimes target to be slimepost
            Debug.Log("slime is in post range");
            //slimeMotor.target = targetPost;
            var curSlime = other.GetComponent<SlimeMotor>();
            if(curSlime != null)
            {
                curSlime.target = targetPost;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Slime")
        {
            //switch target back to slimes original target
            Debug.Log("slime is out of post range");
            //slimeMotor.target = slimeMotor.tempTarget;

            var curSlime = other.GetComponent<SlimeMotor>();

            if (curSlime != null)
            {
                curSlime.target = curSlime.tempTarget;
            }
        }
    }

    //void OnDrawGizmos()
    //{
    //    //changes the color to red
    //    Gizmos.color = Color.yellow;
    //    //draws a line from camera to mouse input position
    //    //Gizmos.DrawLine(ray.origin, hit.point);

    //    //draws a sphere at mouse click position with a float radius
    //    //Gizmos.DrawSphere(rigidbody.transform, .5f);

    //    //Gizmos.DrawSphere(hit.point, explosionRadius);



    //}
}
