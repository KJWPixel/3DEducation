using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAgent_step_3 : MonoBehaviour
{

    [SerializeField] Vector3 mTargetPosition = new Vector3(0, 0, 0);

    float mSpeed = 5f;

    //�ֻ�� ��Ʈ ���, Sequence���� �ۼ�
    Sequence mRootNode = null;

    void Start()
    {
        //�ൿƮ�� ���� Build BT
        //�ൿƮ���� �ۼ��ϸ� �ڵ� ������ ��谡 �ൿƮ�� ���� �κ����� ����ȭ �ȴ�.

        //Level4
        ActionNode tANMove = new ActionNode(DoMove);

        //Level3
        ActionNode tANIsArrived = new ActionNode(DoisArrived);
        Inverter tNot = new Inverter(tANMove);
        List<Node> tLevel_3 = new List<Node>();
        tLevel_3.Add(tANIsArrived);
        tLevel_3.Add(tNot);

        //Level2
        Selector tSelectArrived = new Selector(tLevel_3);
        ActionNode tANAttack = new ActionNode(DoAttack);
        List<Node> tLevel_2 = new List<Node>();
        tLevel_2.Add(tSelectArrived);
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
        //�������� �����ߴٸ� ����
        if (Vector3.Distance(this.transform.position, mTargetPosition) <= 0.01f)
        {
            Debug.Log("<color='red'>Move Complete</color>");

            return NodeStates.SUCCESS;
        }
        else//�׷��� �ʴٸ� ����
        {
            return NodeStates.FAILURE;
        }
    }

    NodeStates DoMove()
    {
        //�̵�
        Debug.Log("DoMove");

        //MoveTowards���������� ������� �̵��� �����ϴ� �Լ�
        this.transform.position = Vector3.MoveTowards(this.transform.position, mTargetPosition, mSpeed * Time.deltaTime);

        return NodeStates.SUCCESS;

    }

    NodeStates DoAttack()
    {
        Debug.Log("<color='blue'>Do Attack</color>");

        return NodeStates.SUCCESS;
    }

        private void OnDrawGizmos()
    {
        //�������� Ȯ�ο� �����
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(mTargetPosition, Vector3.one * 0.5f);
    }
}
