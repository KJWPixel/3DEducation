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
        //���̾��Ű�� �˻��Ͽ� ��������� ã��.
        mTarget = FindObjectOfType<CPChar_step_1>().gameObject;

        //���������� �����Ѵ�.
        //mNavMeshAgent.SetDestination(mTarget.transform.position);
    }

    void Update()
    {
        if(null != mNavMeshAgent)
        {
            if(mNavMeshAgent.enabled)
            {
                //���������� �����Ѵ�.
                mNavMeshAgent.SetDestination(mTarget.transform.position);
                //<--coroutine�� Ȱ���ϸ� ���� ȿ������ ���̴�.
            }
        }
    }
}
