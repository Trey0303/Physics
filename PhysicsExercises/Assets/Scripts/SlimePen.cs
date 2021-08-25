using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlimePen : MonoBehaviour
{
    public Text text;
    public int count;

    // Start is called before the first frame update
    void Start()
    {
        text.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "" + count;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Slime")
        {
            count++;
            //Debug.Log("A slime has entered the pen");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Slime")
        {
            count--;
            //Debug.Log("A slime has left the pen");
        }

    }
}
