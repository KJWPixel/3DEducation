using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAgent_step_4 : MonoBehaviour
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

    NodeStates DoisDetect()
    {
        //���
        GameObject tOpposite = GameObject.FindGameObjectWithTag("tagOpposite");

        if (tOpposite == null)
        {
            return NodeStates.FAILURE;
        }

        //Ž�� ���� �ȿ� ����� �ִٸ� ����
        if (Vector3.Distance(this.transform.position, tOpposite.transform.position) <= 2f)
        {
            Debug.Log("<color='yellow'>Detect Success!!!</color>");

            return NodeStates.SUCCESS;
        }
        else//�׷��� �ʴٸ� ����
        {
            return NodeStates.FAILURE;
        }
    }

    NodeStates DoDetect()
    {
        //Ž��
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
        //�������� Ȯ�ο� �����
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(mTargetPosition, Vector3.one * 0.5f);

        //Ž������ Ȯ�ο� �����
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, 2f);
    }


}
