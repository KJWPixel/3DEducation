using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class CEnemyBT : MonoBehaviour
{
    NavMeshAgent mNavMeshAgent = null;

    GameObject mTarget = null;  //주인공 캐릭터

    [SerializeField]
    bool mIsGrenade = false;        //수류탄 소지 여부

    [SerializeField]
    Vector3 mSpawnAnyPostion = Vector3.zero;        //원래 지점

    [SerializeField]
    float mDetectedRadius = 0f;     //탐지 반경


    Sequence mRootNode = null;



    void BuildBT()
    {
        //행동트리 구축 Build BT
        //행동트리로 작성하면 코드 변경의 경계가 행동트리 구축 부분으로 국지화된다.

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
        //탐지 범위 안에 대상이 있다면 성공
        if (Vector3.Distance(this.transform.position, mTarget.transform.position) <= mDetectedRadius)
        {
            Debug.Log("<color='yellow'>Is Arrived, TRUE!!!!!!!</color>");

            return NodeStates.SUCCESS;
        }
        else//그렇지 않다면 실패
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

        //수류탄은 1발이라고 가정
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
        //하이어라키를 검색하여 목적대상을 찾음.
        mTarget = FindObjectOfType<CPChar_step_1>().gameObject;
        //원래 지점도 기억시켜 둔다.
        mSpawnAnyPostion = this.transform.position;


        //행동트리 구축
        BuildBT();

    }

    // Update is called once per frame
    void Update()
    {
        mRootNode.Evaluate();
    }

    private void OnDrawGizmos()
    {
        //목적지점 확인용 기즈모
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireCube(mTarget.transform.position, Vector3.one * 0.5f);


        //탐지범위 확인용 기즈모
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(this.transform.position, mDetectedRadius);
    }
}
