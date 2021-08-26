using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEditor;

//Queries
//This script will allow you to pickup a slime using left mouse click
public class SlimePicker : MonoBehaviour
{
    public SlimeMotor slimeMotor;

    public Rigidbody rb;

    public Camera cam;
    public float rayLength;
    public LayerMask layerMask;

    Ray ray;
    RaycastHit hit;

    private Transform slime;//create a reference for slime to save in memory for when we want to drop the slime somewhere else

    public float explosionRadius = 2;
    public float explosionStrength = 5;
    public float upwardsModifier = 7f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))//left click
        {
            ray = cam.ScreenPointToRay(Input.mousePosition);



            if (Physics.Raycast(ray, out hit, 600))
            {
                if (hit.collider.tag == "Slime")
                {
                    //Debug.Log(hit.collider.tag);
                    Debug.DrawLine(ray.origin, hit.point);
                    //Debug.Log("This is a slime");
                    slime = hit.rigidbody.transform;//saves the current slime position(grabbing the rigidbody will take how whole object that is moving)
                    

                }
                if (hit.collider.tag == "MegaSlime")
                {
                    //Debug.Log(hit.collider.tag);
                    Debug.DrawLine(ray.origin, hit.point);
                    //Debug.Log("This is a slime");
                    slime = hit.rigidbody.transform;//saves the current slime position(grabbing the rigidbody will take how whole object that is moving)


                }
                else if (hit.collider.tag == "ground")
                {

                    if (slime != null)
                    {
                        //Debug.Log("ground hit");
                        slime.position = hit.point;//replaces current slimes position with selected ground position to move to
                        //set slime reference to null so that it doesnt keep the previous slime selected
                        slime = null;//forces player to select a slime again if wanted to be placed somewhere else
                    }


                }

            }
        }
        else if (Input.GetMouseButtonDown(1))//right click
        {
            ray = cam.ScreenPointToRay(Input.mousePosition);



            if (Physics.Raycast(ray, out hit, 100))
            {
                ray = cam.ScreenPointToRay(Input.mousePosition);

                ExplosionDamage(hit.point, explosionRadius);
            }

        }
    }

    void ExplosionDamage(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        
        if(hitColliders.Length > 0)//not null
        {
            foreach (Collider hitCollider in hitColliders)
            {

                if (hitCollider.gameObject.tag == "Slime")
                {
                    //slime gets hit by explosion
                    Debug.Log("Slime Explosion");
                    rb.AddExplosionForce(explosionStrength, center, radius, upwardsModifier, ForceMode.Impulse);
                    
                    

                }


            }
        }
        
        
    }

    void OnDrawGizmos()
    {
        if(hit.point != null)
        {
            //changes the color to red
            Gizmos.color = Color.red;
            //draws a line from camera to mouse input position
            Gizmos.DrawLine(ray.origin, hit.point);

            //draws a sphere at mouse click position with a float radius
            //Gizmos.DrawSphere(hit.point, .5f);

            //Gizmos.DrawSphere(hit.point, explosionRadius);

        }
    }
}
