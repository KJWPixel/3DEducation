using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;


/*
  �Ϲ� źȯ �߻� Ÿ��




  
*/


public class CTower : MonoBehaviour
{
    //źȯ ������ ����
    //[SerializeField] CBullet PFBullet = null; 

    [SerializeField] GameObject mHead = null; //���� 

   // [SerializeField] GameObject mPosFire = null; //źȯ �߻� ��ġ

    [SerializeField] GameObject mTargetObject = null; //���ô��


    void Start()
    {
        //��� �׷������� �˻��ؼ� ã��, �˻��� �ð��� �ҿ��
        mTargetObject = FindObjectOfType<CPChar_step_1>().gameObject;
    }

    
    void Update()
    {
        //����: ����Haed�� Ÿ���� �ٶ󺸰� ����.

        //case 0
        //������ ũ���� ������ ������ ���� ���ϱ� = �������� - ��������
        //Vector3 tDir = mTargetObject.transform.position - mHead.transform.position;
        //mHead.transform.forward = tDir.normalized; //���� ���͸� ���� ����

        //case 1
        //Vector3 tDir = mTargetObject.transform.position - mHead.transform.position;
        //mHead.transform.rotation = Quaternion.LookRotation(tDir.normalized);//������� �̿��Ͽ� �ش� �������� ȸ��

        //case 2
        Vector3 tDir = mTargetObject.transform.position - mHead.transform.position;
        Quaternion tA = Quaternion.LookRotation(mHead.transform.forward); //���� ������ �����
        Quaternion tB = Quaternion.LookRotation(tDir.normalized); //ȸ���� ���� ������ �����
        mHead.transform.rotation = Quaternion.Slerp(tA, tB, 1f * Time.deltaTime);
        //������� ���鼱�������� �̿��Ͽ� ����ȿ�� ����, ���� �ʰ� ����´�.

    }


}
