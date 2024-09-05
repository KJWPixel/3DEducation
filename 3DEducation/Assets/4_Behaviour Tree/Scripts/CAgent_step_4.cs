using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAgent_step_4 : MonoBehaviour
{

    [SerializeField] Vector3 mTargetPosition = new Vector3(0, 0, 0);

    float mSpeed = 5f;

    //최상단 루트 노드, Sequence노드로 작성
    Sequence mRootNode = null;

    void Start()
    {
        //행동트리 구축 Build BT
        //행동트리로 작성하면 코드 변경의 경계가 행동트리 구축 부분으로 국지화 된다.

        //Level4
        ActionNode tANMove = new ActionNode(DoMove);
        ActionNode tANDetect = new ActionNode(DoDetect);

        //Level3
        //Level 3_0
        ActionNode tANIsArrived = new ActionNode(DoisArrived);
        Inverter tNot = new Inverter(tANMove);
        List<Node> tLevel_3_0 = new List<Node>();
        tLevel_3_0.Add(tANIsArrived);
        tLevel_3_0.Add(tNot);

        //Level 3_1
        ActionNode tANIsDetect = new ActionNode(DoisDetect);
        Inverter tNotDetect = new Inverter(tANDetect);
        List<Node> tLevel_3_1 = new List<Node>();
        tLevel_3_1.Add(tANIsDetect);
        tLevel_3_1.Add(tNotDetect);

        //Level2
        Selector tSelectArrived = new Selector(tLevel_3_0);
        Selector tSelectDetect = new Selector(tLevel_3_1);
        ActionNode tANAttack = new ActionNode(DoAttack);
        List<Node> tLevel_2 = new List<Node>();
        tLevel_2.Add(tSelectArrived);
        tLevel_2.Add(tANDetect);
        tLevel_2.Add(tANAttack);

        //Level1
        mRootNode = new Sequence(tLevel_2);
    }

    
    void Update()
    {
        mRootNode.Evaluate();
    }


    NodeStates DoisArrived()
    {
        //목적지에 도달했다면 성공
        if (Vector3.Distance(this.transform.position, mTargetPosition) <= 0.01f)
        {
            Debug.Log("<color='red'>Move Complete</color>");

            return NodeStates.SUCCESS;
        }
        else//그렇지 않다면 실패
        {
            return NodeStates.FAILURE;
        }
    }

    NodeStates DoMove()
    {
        //이동
        Debug.Log("DoMove");

        //MoveTowards선형보간을 기반으로 이동을 수행하는 함수
        this.transform.position = Vector3.MoveTowards(this.transform.position, mTargetPosition, mSpeed * Time.deltaTime);

        return NodeStates.SUCCESS;

    }

    NodeStates DoisDetect()
    {
        //대상
        GameObject tOpposite = GameObject.FindGameObjectWithTag("tagOpposite");

        if (tOpposite == null)
        {
            return NodeStates.FAILURE;
        }

        //탐지 범위 안에 대상이 있다면 성공
        if (Vector3.Distance(this.transform.position, tOpposite.transform.position) <= 2f)
        {
            Debug.Log("<color='yellow'>Detect Success!!!</color>");

            return NodeStates.SUCCESS;
        }
        else//그렇지 않다면 실패
        {
            return NodeStates.FAILURE;
        }
    }

    NodeStates DoDetect()
    {
        //탐지
        Debug.Log("DoDetect");

        return NodeStates.SUCCESS;

    }

    NodeStates DoAttack()
    {
        Debug.Log("<color='blue'>Do Attack</color>");

        return NodeStates.SUCCESS;
    }

    private void OnDrawGizmos()
    {
        //목적지점 확인용 기즈모
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(mTargetPosition, Vector3.one * 0.5f);

        //탐지범위 확인용 기즈모
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, 2f);
    }


}
