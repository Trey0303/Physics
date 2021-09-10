using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCam : MonoBehaviour
{
    public KinematicCharacter playerController;
    public float speed;

    //public Camera cam;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!playerController.playerCam)
        {
            Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            transform.position += input * Time.deltaTime * speed;
        }
        
    }
}
