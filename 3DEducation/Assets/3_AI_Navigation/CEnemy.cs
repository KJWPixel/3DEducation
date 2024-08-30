using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CEnemy : MonoBehaviour
{
    NavMeshAgent mNavMeshAgent = null;

    GameObject mTarget = null;

    private void Awake()
    {
        mNavMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        //하이어라키를 검색하여 목적대상을 찾음.
        mTarget = FindObjectOfType<CPChar_step_1>().gameObject;

        //목적지점을 설정한다.
        //mNavMeshAgent.SetDestination(mTarget.transform.position);
    }

    void Update()
    {
        if(null != mNavMeshAgent)
        {
            if(mNavMeshAgent.enabled)
            {
                //목적지점을 설정한다.
                mNavMeshAgent.SetDestination(mTarget.transform.position);
                //<--coroutine을 활용하면 좀더 효율적일 것이다.
            }
        }
    }
}
