using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForRotate : MonoBehaviour
{
    [SerializeField] float mAngle = 0f;
   
    void Start()
    {
        
    }

    
    void Update()
    {
        //월드좌표계 기준으로 1프레임마다 해당 속도로 회전
        this.transform.Rotate(Vector3.up, mAngle * Time.deltaTime, Space.World );
    }
}
