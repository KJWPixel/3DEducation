using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class CEnemyBT : MonoBehaviour
{
    NavMeshAgent mNavMeshAgent = null;

    GameObject mTarget = null;  //���ΰ� ĳ����

    [SerializeField]
    bool mIsGrenade = false;        //����ź ���� ����

    [SerializeField]
    Vector3 mSpawnAnyPostion = Vector3.zero;        //���� ����

    [SerializeField]
    float mDetectedRadius = 0f;     //Ž�� �ݰ�


    Sequence mRootNode = null;



    void BuildBT()
    {
        //�ൿƮ�� ���� Build BT
        //�ൿƮ���� �ۼ��ϸ� �ڵ� ������ ��谡 �ൿƮ�� ���� �κ����� ����ȭ�ȴ�.

        //level 4
        ActionNode tANAvoid = new ActionNode(DoAvoid);
        ActionNode tANFollow = new ActionNode(DoFollow);

        //level 3
        //level 3_0
        ActionNode tANIsGrenade = new ActionNode(DoIsGrenade);
        Inverter tNotAvoid = new Inverter(tANAvoid);
        List<Node> tLevel_3_0 = new List<Node>();
        tLevel_3_0.Add(tANIsGrenade);
        tLevel_3_0.Add(tNotAvoid);

        //level 3_1
        ActionNode tANIsArrived = new ActionNode(DoIsArrived);
        Inverter tNotFollow = new Inverter(tANFollow);
        List<Node> tLevel_3_1 = new List<Node>();
        tLevel_3_1.Add(tANIsArrived);
        tLevel_3_1.Add(tNotFollow);

        //level 2
        Selector tSelectIsGrenade = new Selector(tLevel_3_0);
        Selector tSelectIsArrived = new Selector(tLevel_3_1);
        ActionNode tANThrowGrenade = new ActionNode(DoThrowGrenade);
        List<Node> tLevel_2 = new List<Node>();
        tLevel_2.Add(tSelectIsGrenade);
        tLevel_2.Add(tSelectIsArrived);
        tLevel_2.Add(tANThrowGrenade);

        //level 1
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
            Debug.Log("<color='yellow'>Is Arrived, TRUE!!!!!!!</color>");

            return NodeStates.SUCCESS;
        }
        else//�׷��� �ʴٸ� ����
        {
            return NodeStates.FAILURE;
        }
    }
    NodeStates DoAvoid()
    {
        Debug.Log("DoAvoid");
        mNavMeshAgent.SetDestination(mSpawnAnyPostion);

        return NodeStates.SUCCESS;
    }
    NodeStates DoFollow()
    {
        Debug.Log("DoFollow");
        mNavMeshAgent.SetDestination(mTarget.transform.position);

        return NodeStates.SUCCESS;
    }
    NodeStates DoThrowGrenade()
    {
        Debug.Log("<size='16'><color='red'>Do Throw Grenade</color></size>");

        //����ź�� 1���̶�� ����
        mIsGrenade = false;

        return NodeStates.SUCCESS;
    }

    private void Awake()
    {
        mNavMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //���̾��Ű�� �˻��Ͽ� ��������� ã��.
        mTarget = FindObjectOfType<CPChar_step_1>().gameObject;
        //���� ������ ������ �д�.
        mSpawnAnyPostion = this.transform.position;


        //�ൿƮ�� ����
        BuildBT();

    }

    // Update is called once per frame
    void Update()
    {
        mRootNode.Evaluate();
    }

    private void OnDrawGizmos()
    {
        //�������� Ȯ�ο� �����
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireCube(mTarget.transform.position, Vector3.one * 0.5f);


        //Ž������ Ȯ�ο� �����
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(this.transform.position, mDetectedRadius);
    }
}
