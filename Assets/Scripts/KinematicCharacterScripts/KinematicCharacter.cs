using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicCharacter : MonoBehaviour
{
    public GameObject player;
    public Rigidbody rb;
    public float speed = 5f;
    public float skinWidth = .001f;
    Vector3 velocity;

    public bool gravity = false;
    public bool isGrounded = false;

    //public int jumpStrength = 10;
    public float jumpHeight = 5f;
    public float maxGroundAngle = 60f;
    private bool jump = false;
    //public float jumpSpeed = 5;

    Vector3 playerSize;

    public bool playerCam;

    LayerMask mask;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        velocity = rb.velocity;
        playerSize = player.transform.localScale;
        mask = LayerMask.GetMask("Default");
    }

    private void Update()
    {
        if (playerCam)
        {
            if (isGrounded)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    //Debug.Log("Jump");
                    jump = true;

                    isGrounded = false;
                }

            }
        }
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerCam)
        { 
            //update Velocity
            //player.transform.position += velocity * Time.deltaTime;

            //Input
            Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            if (jump)
            {
                velocity += new Vector3(0, jumpHeight, 0);
                jump = false;
            }


            //gravity
            if (gravity)
            {
                velocity += Physics.gravity * Time.deltaTime;
            }

            //projected position
            Vector3 projectedPosition = player.transform.position + (velocity + input) * Time.deltaTime * speed;

            var thisCollider = GetComponent<Collider>();//player collider

            if (thisCollider != null)
            {

                //collide/overlap Detection                                              Skin Width: Add this skin width when performing your collision tests. For example, if my KinematicCharacter has a BoxCollider that has a size of 1,1,1, then I should be testing with a box whose full-extents are 1.001, 1.001, 1.001 when calling Physics.OverlapBox.
                Collider[] hitColliders = Physics.OverlapBox(projectedPosition, new Vector3(transform.localScale.x + skinWidth, transform.localScale.y + skinWidth, transform.localScale.z + skinWidth), Quaternion.identity, mask);//Remember that Physics.OverlapBox asks for the half-extents so you'll need to divide the box's size by two before passing it along.

                //Check when there is a new collider coming into contact with the box
                for (int i = 0; i < hitColliders.Length; i++)
                {
                    if (hitColliders[i] == thisCollider)
                    {
                        continue;
                    }

                    //Output all of the collider names
                    //Debug.Log("Hit : " + hitColliders[i].name + i);

                    Vector3 otherPosition = hitColliders[i].transform.position;//gets objects position
                    Quaternion otherRotation = hitColliders[i].transform.rotation;//gets objects rotation

                    float distance;
                    Vector3 direction;

                    //ComputePenetration works as a bool to know when the player is or isnt inside an object. Also give you the distance and direction between player and object
                    //                                           object A collider, object A position, object A rotation, object b collider, object b position, object b rotation, MTV(minimum translation vector)
                    bool overlapped = Physics.ComputePenetration(thisCollider, projectedPosition, transform.rotation, hitColliders[i], otherPosition, otherRotation, out direction, out distance);

                    //if player is gonna overlap with object
                    if (overlapped)
                    {
                        //Debug.DrawRay(player.transform.position, direction * distance);
                        //position = position + direction * distance
                        projectedPosition += direction * distance;//pushes back player by direction times distance and adding to players current position

                        //                               (vector, planeNormal)
                        velocity = Vector3.ProjectOnPlane(velocity, direction);

                        float angle = Vector3.Angle(direction, Vector3.up);
                        //Debug.Log("angle: " + angle);

                        if (angle < maxGroundAngle)
                        {
                            projectedPosition.y += direction.y * distance;
                            velocity = new Vector3(0f, velocity.y, 0f);

                            //Debug.Log("walkable slope");
                            isGrounded = true;
                        }
                        else//if slope too steep
                        {
                            //Debug.Log("too steep a slope");
                            isGrounded = false;
                        }

                    }


                }


            }
            //Movement
            rb.MovePosition(projectedPosition);
        }
            
        

    }
}
