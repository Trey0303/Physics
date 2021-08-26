using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicCharacter : MonoBehaviour
{
    public GameObject player;
    public Rigidbody rb;
    public float speed = 5;
    public float skinWidth = .05f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //player.transform.position += velocity * Time.deltaTime;
        
        //player movement
        //Input
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));


        var thisCollider = GetComponent<Collider>();//player collider

        if (thisCollider != null)
        {
            //collide/overlap Detection                                              Skin Width: Add this skin width when performing your collision tests. For example, if my KinematicCharacter has a BoxCollider that has a size of 1,1,1, then I should be testing with a box whose full-extents are 1.001, 1.001, 1.001 when calling Physics.OverlapBox.
            Collider[] hitColliders = Physics.OverlapBox(player.transform.position, new Vector3(transform.localScale.x + skinWidth, transform.localScale.y + skinWidth, transform.localScale.z + skinWidth), Quaternion.identity);//Remember that Physics.OverlapBox asks for the half-extents so you'll need to divide the box's size by two before passing it along.

            //Check when there is a new collider coming into contact with the box
            for (int i = 0; i < hitColliders.Length; i++)
            {
                if (hitColliders[i] == thisCollider)
                {
                    continue;
                }

                //Output all of the collider names
                Debug.Log("Hit : " + hitColliders[i].name + i);

                Vector3 otherPosition = hitColliders[i].transform.position;//gets objects position
                Quaternion otherRotation = hitColliders[i].transform.rotation;//gets objects rotation

                float distance;
                Vector3 direction;

                //ComputePenetration works as a bool to know when the player is or isnt inside an object. Also give you the distance and direction between player and object
                //                                           object A collider, object A position, object A rotation, object b collider, object b position, object b rotation, MTV(minimum translation vector)
                bool overlapped = Physics.ComputePenetration(thisCollider, transform.position, transform.rotation, hitColliders[i], otherPosition, otherRotation, out direction, out distance);

                //if player is gonna overlap with object
                if (overlapped)
                {
                    Debug.DrawRay(player.transform.position, direction * distance);
                    //position = position + direction * distance
                    player.transform.position += direction * distance;//pushes back player by direction times distance and adding to players current position

                    //                                        (vector, planeNormal)
                    Vector3 clippingVelocity = Vector3.ProjectOnPlane(player.transform.position, direction);
                }
            }


        }
        //Movement
        rb.MovePosition(player.transform.position + input * Time.deltaTime * speed);

    }
}
