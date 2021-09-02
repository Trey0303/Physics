using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMover : MonoBehaviour
{
    //private SlimePicker slimePicked;
    //public GameObject child;
    public Rigidbody rb;

    //public Vector3 launchVelocity;//inital velocity
    //public Vector3 gravity;//
    public float timeItWillTake = 1;// time interval
    public float mass;

    //raycast
    public Camera cam;
    public float rayLength;
    public LayerMask layerMask;

    Ray ray;
    RaycastHit hit;
    private Transform slime;

    // Start is called before the first frame update
    void Start()
    {
        //timeItWillTake = Time.time;
        mass = rb.mass;

        //overwrites local position of child gameObject with launchVelocity, Gravity, timeSinceLaunch
        //x = (speed/initial horizontal launch force * time interval)
        //y = (initial horizontal launch force * time interval) + (Constant Acceleration * time interval)

        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(2))//middle mouse
        {
            ray = cam.ScreenPointToRay(Input.mousePosition);

            Callraycast();

            
        }

        

    }

    public void Callraycast()
    {
        if (Physics.Raycast(ray, out hit, 600))
        {
            if (hit.collider.tag == "Slime")
            {
               
                //Debug.Log(hit.collider.tag);
                Debug.DrawLine(ray.origin, hit.point);
                Debug.Log("This is a slime");
                slime = hit.rigidbody.transform;//saves the current slime position(grabbing the rigidbody will take how whole object that is moving)

            }
            if (hit.collider.tag == "MegaSlime")
            {
               
                //Debug.Log(hit.collider.tag);
                Debug.DrawLine(ray.origin, hit.point);
                Debug.Log("This is a slime");
                slime = hit.rigidbody.transform;//saves the current slime position(grabbing the rigidbody will take how whole object that is moving)


            }
            else if (hit.collider.tag == "ground")
            {

                if (slime != null)
                {
                    Debug.Log("ground hit");

                    // horizontal distance = target - current / time
                    float disX = (hit.point.x - slime.position.x) / timeItWillTake;
                    float disZ = (hit.point.z - slime.position.z) / timeItWillTake;

                    float verticalImpulse = (hit.point.y + 0.5f * -Physics.gravity.y * timeItWillTake * timeItWillTake - slime.position.y) / timeItWillTake;

                    //rb.AddForce(new Vector3(disX, verticalImpulse, disZ), ForceMode.Impulse);
                    rb.velocity = (new Vector3(disX, verticalImpulse, disZ));

                    Debug.DrawRay(slime.position, Vector3.up, Color.green, 2.0f);
                    Debug.DrawRay(hit.point, Vector3.up, Color.red, 2.0f);
                    Debug.DrawRay(slime.position, new Vector3(disX, verticalImpulse, disZ), Color.magenta, 5.0f);

                    //slime.position = new Vector3(hit.point.x, hit.point.y + 1f, hit.point.z);//replaces current slimes position with selected ground position to move to
                    //set slime reference to null so that it doesnt keep the previous slime selected
                    //while (timeSinceLaunch <= 5)
                    //{
                    //Update the position by adding the object’s displacement to its position
                    //position = position + velocity * deltaTime
                    //transform.localPosition = transform.localPosition + launchVelocity;

                    //Update the velocity by integrating forces
                    //velocity = velocity + (force / mass) * dt;
                    //launchVelocity = launchVelocity + (gravity / mass) ;  

                    //As time since launch increases, the child game object should be moved along its launch trajectory.



                    //}
                    slime = null;//forces player to select a slime again if wanted to be placed somewhere else
                }


            }

        }
    }
}
