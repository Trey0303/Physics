using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public KinematicCharacter player;
    public Camera cam1;

    public Camera cam2;

    public bool camSwap = false;

    public GameObject playerUI;
    public GameObject overviewUI;
    public bool hidden;

    // Start is called before the first frame update
    void Start()
    {
        StartingCam();
        playerUI.SetActive(true);
        overviewUI.SetActive(false);
        hidden = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            hideText();

        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwapCam();
        }
    }

    private void hideText()
    {
        if (hidden)//text is hidden
        {
            hidden = false;
            //show correct text
            if (camSwap)//if on player cam show player info
            {
                playerUI.SetActive(true);
                overviewUI.SetActive(false);

            }
            else if (!camSwap)//if on overview cam show overview info
            {
                playerUI.SetActive(false);
                overviewUI.SetActive(true);

            }

        }
        else//hide text
        {
            hidden = true;
            playerUI.SetActive(false);
            overviewUI.SetActive(false);

        }

    }

    void SwapCam(){

        if (camSwap)//overview
        {
            cam1.enabled = false;
            cam2.enabled = true;
            camSwap = false;
            player.playerCam = false;
            if (!hidden)
            {
                playerUI.SetActive(false);
                overviewUI.SetActive(true);

            }

        }
        else if (!camSwap)//player
        {
            cam1.enabled = true;
            cam2.enabled = false;
            camSwap = true;
            player.playerCam = true;

            if (!hidden)
            {
                playerUI.SetActive(true);
                overviewUI.SetActive(false);

            }

        }
    }

    void StartingCam()
    {
        if (camSwap)//player cam
        {
            cam1.enabled = true;
            cam2.enabled = false;
            player.playerCam = true;
            playerUI.SetActive(true);
            overviewUI.SetActive(false);

        }
        else if (!camSwap)//overview cam
        {
            cam1.enabled = false;
            cam2.enabled = true;
            player.playerCam = false;
            playerUI.SetActive(false);
            overviewUI.SetActive(true);

        }
    }
}
