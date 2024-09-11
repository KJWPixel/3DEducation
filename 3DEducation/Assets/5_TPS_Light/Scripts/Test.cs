using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Test : MonoBehaviour
{
    NavMeshAgent mNavMeshAgent = null;

    GameObject mTarget = null; //���ΰ� ĳ����

    [SerializeField] bool mIsGrenade = false;    //����ź ���� ����

    [SerializeField] Vector3 mSpawnAnyPosition = Vector3.zero; //���� ����

    [SerializeField] float mDetectedRadius = 0f; //Ž�� �ݰ�

    Sequence mRootNode = null;



    void BuildBT()
    {
        //�ൿƮ�� ���� Build BT
        //�ൿƮ���� �ۼ��ϸ� �ڵ� ������ ��谡 �ൿƮ�� ���� �κ����� ����ȭ �ȴ�.

        //Level4
        ActionNode tANAvoid = new ActionNode(DoIsAvoid);
        ActionNode tANFollow = new ActionNode(DoIsFollow);

        //Level3
        //Level 3_0
        ActionNode tANIsGrenade = new ActionNode(DoIsGrenade);
        Inverter tNotAvoid = new Inverter(tANAvoid);
        List<Node> tLevel_3_0 = new List<Node>();
        tLevel_3_0.Add(tANIsGrenade);
        tLevel_3_0.Add(tNotAvoid);

        //Level 3_1
        ActionNode tANIsArrived = new ActionNode(DoIsArrived);
        Inverter tNotFollow = new Inverter(tANFollow);
        List<Node> tLevel_3_1 = new List<Node>();
        tLevel_3_1.Add(tANIsArrived);
        tLevel_3_1.Add(tNotFollow);

        //Level2
        Selector tSelectIsGrenade = new Selector(tLevel_3_0);
        Selector tSelectIsArrived = new Selector(tLevel_3_1);
        ActionNode tANThrowGrenade = new ActionNode(DoThrowGrenade);
        List<Node> tLevel_2 = new List<Node>();
        tLevel_2.Add(tSelectIsGrenade);
        tLevel_2.Add(tSelectIsArrived);
        tLevel_2.Add(tANThrowGrenade);

        //Level1
        mRootNode = new Sequence(tLevel_2);
    }

    NodeStates DoIsGrenade()
    {
        if (mIsGrenade)
        {
            Debug.Log("Is Grenade? TRUE");
            return NodeStates.SUCCESS;
        }
        else
        {
            return NodeStates.FAILURE;
        }
    }

    NodeStates DoIsArrived()
    {
        //Ž�� ���� �ȿ� ����� �ִٸ� ����
        if (Vector3.Distance(this.transform.position, mTarget.transform.position) <= mDetectedRadius)
        {
            Debug.Log("<color='yellow'>Is Arrived, TRUE!!!</color>");

            return NodeStates.SUCCESS;
        }
        else//�׷��� �ʴٸ� ����
        {
            return NodeStates.FAILURE;
        }
    }

    NodeStates DoIsAvoid()
    {
        Debug.Log("DoAvoid");
        mNavMeshAgent.SetDestination(mSpawnAnyPosition);

        return NodeStates.SUCCESS;
    }

    NodeStates DoIsFollow()
    {
        Debug.Log("DoFollow");
        mNavMeshAgent.SetDestination(mTarget.transform.position);

        return NodeStates.SUCCESS;
    }
    NodeStates DoThrowGrenade()
    {
        Debug.Log("<size=16><color='red'>DoThrowGrenade</color></size>");

        //����ź�� 1���̶�� ����
        mIsGrenade = false;

        return NodeStates.SUCCESS;
    }

    private void Awake()
    {
        mNavMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        //���̾��Ű�� �˻��Ͽ� ��������� ã��.
        mTarget = FindObjectOfType<CPChar_step_1>().gameObject;
        //���� ������ ������ �д�.
        mSpawnAnyPosition = this.transform.position;


        //�ൿƮ�� ����
        BuildBT();
    }

    void Update()
    {
        mRootNode.Evaluate();
    }

    private void OnDrawGizmos()
    {
        //�������� Ȯ�ο� �����
        //Gizmos.color = Color.yellow;
        //Gizmos.DrawWireCube(mTarget.transform.position, Vector3.one * 0.5f);

        //Ž������ Ȯ�ο� �����
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(this.transform.position, mDetectedRadius);
    }
}
