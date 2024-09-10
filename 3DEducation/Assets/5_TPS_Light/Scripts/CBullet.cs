using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBullet : MonoBehaviour
{
    
    void Start()
    {
        //일정 시간 후에 자동으로 소멸
        Destroy(gameObject, 5f);
    }

    
    void Update()
    {
        
    }
}
