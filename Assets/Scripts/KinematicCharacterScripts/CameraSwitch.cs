using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public KinematicCharacter player;
    public Camera cam1;

    public Camera cam2;

    public bool camSwap = false;

    // Start is called before the first frame update
    void Start()
    {
        if (camSwap)//player cam
        {
            cam1.enabled = true;
            cam2.enabled = false;
            player.playerCam = true;

        }
        else if (!camSwap)//overview cam
        {
            cam1.enabled = false;
            cam2.enabled = true;
            player.playerCam = false;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (camSwap)
            {
                cam1.enabled = false;
                cam2.enabled = true;
                camSwap = false;
                player.playerCam = false;

            }
            else if (!camSwap)
            {
                cam1.enabled = true;
                cam2.enabled = false;
                camSwap = true;
                player.playerCam = true;

            }
        }
    }
}
