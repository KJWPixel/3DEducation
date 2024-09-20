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
        //������ǥ�� �������� 1�����Ӹ��� �ش� �ӵ��� ȸ��
        this.transform.Rotate(Vector3.up, mAngle * Time.deltaTime, Space.World );
    }
}
