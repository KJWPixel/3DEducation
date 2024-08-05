using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootAtPlayer : MonoBehaviour
{
    [SerializeField] Transform trsLookAt;

     void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(trsLookAt == null)
        {
            return;
        }

        transform.LookAt(trsLookAt);
    }
}
