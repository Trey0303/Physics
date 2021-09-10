using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slimeGate : MonoBehaviour
{
    public KinematicCharacter player;

    public Camera cam;

    Ray ray;
    RaycastHit hit;

    public HingeJoint rightHinge;
    public HingeJoint leftHinge;
    JointMotor rightMotor;
    JointMotor leftMotor;
    JointLimits rightLimits;
    JointLimits leftLimits;

    //public Component[] hingeJoints;
    //JointMotor motor;

    // Start is called before the first frame update
    void Start()
    {
        //rightHinge = GetComponentInChildren<HingeJoint>();
        //leftHinge = GetComponentInChildren<HingeJoint>();

        rightMotor = rightHinge.motor;
        leftMotor = rightHinge.motor;

        rightHinge.useMotor = true;
        leftHinge.useMotor = true;

        rightMotor.freeSpin = false;
        leftMotor.freeSpin = false;

        rightLimits = rightHinge.limits;
        leftLimits = leftHinge.limits;

    }

    // Update is called once per frame
    void Update()
    {
        if(player.playerCam == false)
        {
            if (Input.GetMouseButtonDown(0))//left click
            {
                OpenCloseGates();
            }
        }

        
    }

    private void OpenCloseGates()
    {
        ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 600))
        {
            if (hit.collider.tag == "RightGate" || hit.collider.tag == "LeftGate")
            {
                Debug.Log("Door clicked");
                if (rightMotor.targetVelocity == rightLimits.min || leftMotor.targetVelocity == leftLimits.max)
                {
                    Debug.Log("Door opened");
                    //right door
                    rightMotor.force = 100;
                    rightMotor.targetVelocity = rightLimits.max;

                    rightHinge.motor = rightMotor;

                    //left door
                    leftMotor.force = 100;
                    leftMotor.targetVelocity = leftLimits.min;

                    leftHinge.motor = leftMotor;
                }
                else if (rightMotor.targetVelocity == rightLimits.max || leftMotor.targetVelocity == leftLimits.min)
                {
                    Debug.Log("Door closed");
                    //right door
                    rightMotor.force = 100;
                    rightMotor.targetVelocity = -rightLimits.max;

                    rightHinge.motor = rightMotor;

                    rightMotor.targetVelocity = rightLimits.min;//set velocity back to min so the doors can open again

                    //left door
                    leftMotor.force = 100;
                    leftMotor.targetVelocity = -leftLimits.min;

                    leftHinge.motor = leftMotor;

                    leftMotor.targetVelocity = leftLimits.max;//set velocity back to min so the doors can open again


                }
            }
        }
    }
}
