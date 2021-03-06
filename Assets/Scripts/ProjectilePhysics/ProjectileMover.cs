using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMover : MonoBehaviour
{
    public KinematicCharacter player;

    //private SlimePicker slimePicked;
    //public GameObject child;
    public Rigidbody rb;

    //public Vector3 launchVelocity;//inital velocity
    //public Vector3 gravity;//
    public float timeItWillTake = 1;// time interval
    //public float mass;

    //raycast
    public Camera cam;
    public float rayLength;
    public LayerMask layerMask;

    Ray ray;
    RaycastHit hit;
    private Transform slime;

    // Update is called once per frame
    void Update()
    {
        if (player.playerCam == false)
        {
            if (Input.GetMouseButtonDown(2))//middle mouse
            {
                ray = cam.ScreenPointToRay(Input.mousePosition);

                Callraycast();


            }
        }

    }

    public void Callraycast()
    {
        if (player.playerCam == false)
        {
            if (Physics.Raycast(ray, out hit, 600))
            {
                if (hit.collider.tag == "Slime")
                {

                    //Debug.Log(hit.collider.tag);
                    Debug.DrawLine(ray.origin, hit.point);
                    Debug.Log("This is a slime");
                    slime = hit.transform.gameObject.transform;//saves the current slime position(grabbing the rigidbody will take the whole object that is moving)
                    rb = slime.GetComponent<Rigidbody>();//grabs selected slime rigidbody


                }
                if (hit.collider.tag == "MegaSlime")
                {

                    //Debug.Log(hit.collider.tag);
                    Debug.DrawLine(ray.origin, hit.point);
                    Debug.Log("This is a slime");
                    slime = hit.rigidbody.transform;//saves the current slime position(grabbing the rigidbody will take the whole object that is moving)
                    rb = slime.GetComponent<Rigidbody>();//grabs selected slime rigidbody

                }
                else if (hit.collider.tag == "ground")
                {

                    if (slime != null)
                    {
                        Debug.Log("ground hit");

                        // horizontal distance = target - current / time
                        float disX = (hit.point.x - slime.position.x) / timeItWillTake;
                        float disZ = (hit.point.z - slime.position.z) / timeItWillTake;

                        float verticalImpulse = (hit.point.y + 1.2f * -Physics.gravity.y * timeItWillTake * timeItWillTake - slime.position.y) / timeItWillTake;

                        //rb.AddForce(new Vector3(disX, verticalImpulse, disZ), ForceMode.Impulse);
                        rb.velocity = (new Vector3(disX, verticalImpulse, disZ));

                        Debug.DrawRay(slime.position, Vector3.up, Color.green, 2.0f);
                        Debug.DrawRay(hit.point, Vector3.up, Color.red, 2.0f);
                        Debug.DrawRay(slime.position, new Vector3(disX, verticalImpulse, disZ), Color.magenta, 5.0f);

                        slime = null;//forces player to select a slime again if wanted to be placed somewhere else
                    }


                }

            }
        }

        
    }
}
