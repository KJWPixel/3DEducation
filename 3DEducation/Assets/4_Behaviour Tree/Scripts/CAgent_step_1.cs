using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAgent_step_1 : MonoBehaviour
{

    [SerializeField] Vector3 mTargetPosition = new Vector3(0, 0, 0);

    float mSpeed = 3f;

    //�ֻ�� ��Ʈ ���, Sequence���� �ۼ�
    Sequence mRootNode = null;

    void Start()
    {
        //�ൿƮ�� ���� Build BT

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
        //�̵�
        Debug.Log("DoMove");

        //MoveTowards���������� ������� �̵��� �����ϴ� �Լ�
        this.transform.position = Vector3.MoveTowards(this.transform.position, mTargetPosition, mSpeed * Time.deltaTime);

        //�������� �����ߴٸ� ����
        if(Vector3.Distance(this.transform.position, mTargetPosition) <= 0.01f)
        {
            Debug.Log("<color='red'>Move Complete</color>");

            return NodeStates.SUCCESS;
        }
        else//�׷��� �ʴٸ� ����
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
        //�������� Ȯ�ο� �����
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(mTargetPosition, Vector3.one * 0.5f);
    }
}
