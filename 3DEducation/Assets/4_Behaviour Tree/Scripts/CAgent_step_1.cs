using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAgent_step_1 : MonoBehaviour
{

    [SerializeField] Vector3 mTargetPosition = new Vector3(0, 0, 0);

    float mSpeed = 3f;

    //최상단 루트 노드, Sequence노드로 작성
    Sequence mRootNode = null;

    void Start()
    {
        //행동트리 구축 Build BT

        ActionNode tANMove = new ActionNode(DoMove);
        ActionNode tANAttack = new ActionNode(DoAttack);
        List<Node> tLevel_2 = new List<Node>();
        tLevel_2.Add(tANMove);
        tLevel_2.Add(tANAttack);

        mRootNode = new Sequence(tLevel_2);
    }

    
    void Update()
    {
        mRootNode.Evaluate();
    }

    NodeStates DoMove()
    {
        //이도
        Debug.Log("DoMove");

        //MoveTowards선형보간을 기반으로 이동을 수행하는 함수
        this.transform.position = Vector3.MoveTowards(this.transform.position, mTargetPosition, mSpeed * Time.deltaTime);

        //목적지에 도달했다면 성공
        if(Vector3.Distance(this.transform.position, mTargetPosition) <= 0.01f)
        {
            Debug.Log("<color='red'>Move Complete</color>");

            return NodeStates.SUCCESS;
        }
        else//그렇지 않다면 실패
        {
            return NodeStates.FAILURE;
        }
    }

    NodeStates DoAttack()
    {
        Debug.Log("<color='blue '>Do Attack</color>");

        return NodeStates.SUCCESS;
    }

        private void OnDrawGizmos()
    {
        //목적지점 확인용 기즈모
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(mTargetPosition, Vector3.one * 0.5f);
    }
}
