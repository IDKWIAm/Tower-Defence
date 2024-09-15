using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class RotatePlayer : MonoBehaviour
{
    public int rotate_;
    void Start()
    {

    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (rotate_ != 0)
            {
                rotate_ = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (rotate_ != 90)
            {
                rotate_ = 90;
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (rotate_ != 180)
            {
                rotate_ = 180;
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (rotate_ != -90)
            {
                rotate_ = -90;
            }
        }
        transform.localEulerAngles = new Vector3(0, 0, rotate_);
    }

}
