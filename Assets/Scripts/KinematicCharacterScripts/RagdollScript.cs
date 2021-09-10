using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollScript : MonoBehaviour
{
    public KinematicCharacter playerController;
    Rigidbody rb;

    public GameObject[] children;
    public Rigidbody[] rbChildren;
    
    public bool ragdoll;
    public Camera cam;
    public Transform target;
    public Transform player;

    public Vector3[] lastposition;
    public Quaternion[] lastRotation;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ragdoll = false;
        lastposition = new Vector3[children.Length];
        lastRotation = new Quaternion[children.Length];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //player falls over
        if (playerController.playerCam == true)
        {
            if (ragdoll)
            {
                FallOver();
            }
            CamFollowPlayer();
            
        }
        else if (playerController.playerCam == false)//stand player back up
        {
            if (!ragdoll)
            {
                StandUp();
                
            }
        }
    }

    private void CamFollowPlayer()
    {
        cam.transform.position = new Vector3(target.position.x, target.position.y, target.position.z);
        cam.transform.LookAt(player);
    }

    private void StandUp()
    {
        //Debug.Log("kinematic off");

        foreach (Rigidbody rbChild in rbChildren)
        {
            //Debug.Log("test");
            rbChild.isKinematic = false;
            //Debug.Log(rbChild.isKinematic);
        }
        for (int i = 0; i < children.Length; i++)
        {
            //Debug.Log("test");
            lastposition[i] = children[i].transform.position;
            lastRotation[i] = children[i].transform.rotation;
        }
        ragdoll = true;
    }

    private void FallOver()
    {
        //Debug.Log("kinematic on");

        foreach (Rigidbody rbChild in rbChildren)
        {
            //Debug.Log("test");
            rbChild.isKinematic = true;
            //Debug.Log(rbChild.isKinematic);
        }
        for (int i = 0; i < children.Length; i++)
        {
            //Debug.Log("test");
            children[i].transform.position = lastposition[i];
            children[i].transform.rotation = lastRotation[i];
        }
        ragdoll = false;
    }
}
