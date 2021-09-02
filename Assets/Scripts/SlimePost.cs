using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimePost : MonoBehaviour
{
    //public SlimeMotor slimeMotor;

    public Transform targetPost;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Slime")
        {
            //switch slimes target to be slimepost
            //Debug.Log("slime is in post range");
            //slimeMotor.target = targetPost;
            var curSlime = other.GetComponent<SlimeMotor>();//works with small slime because they dont have child colliders to depend on
            if(curSlime != null)//if slime has slimemotor script
            {
                curSlime.target = targetPost;//change slimes target

                curSlime.focusedOnPost = true;
            }
        }
        else if (other.gameObject.tag == "MegaSlime")
        {
            //switch slimes target to be slimepost
            //Debug.Log("MegaSlime is in post range");
            //slimeMotor.target = targetPost;
            var curSlime = other.transform.parent.GetComponent<SlimeMotor>();//works with finding the parent of child objects
            if (curSlime != null)//if slime has slimemotor script
            {
                curSlime.target = targetPost;//change slimes target

                curSlime.focusedOnPost = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Slime")
        {
            //switch target back to slimes original target
            //Debug.Log("MegaSlime is out of post range");
            //slimeMotor.target = slimeMotor.tempTarget;

            var curSlime = other.GetComponent<SlimeMotor>();

            if (curSlime != null)
            {
                curSlime.target = curSlime.tempTarget;
                curSlime.focusedOnPost = false;
                //focusedOnPost = false;
            }
        }
        else if (other.gameObject.tag == "MegaSlime")
        {
            //switch target back to slimes original target
            //Debug.Log("slime is out of post range");
            //slimeMotor.target = slimeMotor.tempTarget;

            var curSlime = other.transform.parent.GetComponent<SlimeMotor>();

            if (curSlime != null)
            {
                curSlime.target = curSlime.tempTarget;
                curSlime.focusedOnPost = false;
                //focusedOnPost = false;
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
