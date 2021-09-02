using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildSlime : MonoBehaviour
{
    public bool shouldJump = false;
    // Start is called before the first frame update
    void Start()
    {
        bool grounded = (Physics.Raycast(this.transform.position, Vector3.down, 1f));
        if (grounded)
        {
            shouldJump = true;
        }
        else
        {
            shouldJump = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //check for ground collision
        bool grounded = (Physics.Raycast(this.transform.position, Vector3.down, 1f)); 

        if (grounded)
        {
            shouldJump = true;
        }
        else
        {
            shouldJump = false;
        }
    }
}
