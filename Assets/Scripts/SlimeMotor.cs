using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Jobs;

public class SlimeMotor : MonoBehaviour
{
    //public SlimePicker slimePicker;

    public Rigidbody rb;
    //public GameObject slime;
    public Transform target;
    public Transform tempTarget;

    //Quanternion myQuanternion;
    public float speed = 5f;

    //how strong the jump is vertically
    public float jumpVertically;
    //how strong the jump is horizontally
    public float jumpHorizontally;
    //how often to jump in seconds
    public float jumpSeconds;
    public float tempJumpSec;

    public bool atTarget = false;


    public float min = 1f;
    public float max = 2f;

    public int happiness = 0;
    public float pointTimer = 1;
    public float setPointTimer = 1;
    public bool focusedOnPost = false;
    public int requiredPoints = 10;
    public int points = 0;


    //public bool grabbed = false;

    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<Rigidbody>().position = Vector3.zero;
        tempJumpSec = 0;
        tempTarget = target;
        //Quanternion = new Quanternion();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //slime growth
        if (focusedOnPost)
        {
            pointTimer = pointTimer - Time.deltaTime;
            if (pointTimer <= 0)
            {
                happiness++;
                points++;
                pointTimer = setPointTimer;
                if (points > requiredPoints)
                {
                    points = 0;
                    gameObject.transform.localScale = new Vector3(happiness / 4, happiness / 4, happiness / 4);
                }
            }

        }

        float dist = Vector3.Distance(target.position, transform.position);
        if (dist > .7f)
        {
            atTarget = false;

            


            //get the offset for distance between target and slime. normalize it to simplify it to 1, then multiply it by a horizontal force.
            Vector3 offset = (target.position - transform.position).normalized * jumpHorizontally;
            //set the y axis to a vertical force.
            offset.y = jumpVertically;
            //transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, Time.deltaTime * speed);
            //rb.rotation(offset);

            //public static bool Linecast(Vector3 start, Vector3 end, out RaycastHit hitInfo, int layerMask
            //if (Physics.Linecast(transform.position, target.position))
            //{
            //    //Debug.Log("blocked");
            //    //hop randomly

            //    float randX = Random.Range(min, max);
            //    float randZ = Random.Range(min, max);
            //    Vector3 randTarget = new Vector3(randX, 0, randZ);

            //    Vector3 randOffset = (randTarget - transform.position);

            //}

            //slime jumps a certain amount of seconds
            tempJumpSec = tempJumpSec + Time.deltaTime;
            if (tempJumpSec >= jumpSeconds)
            {
                //use offset to add force to rigidbody of slime
                rb.AddForce(offset, ForceMode.Impulse);
                tempJumpSec = 0;
            }

        }
        else if(dist <= .5f)
        {
            
            if (atTarget == false)
            {
               //Debug.Log("target reached");
                atTarget = true;
                //rb.MovePosition(transform.position);
            }

        }

        //if (grabbed)
        //{
        //    transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //}



    }

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
