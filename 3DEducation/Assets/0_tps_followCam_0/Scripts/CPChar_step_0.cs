using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPChar_step_0 : MonoBehaviour
{
    //�̸� ������ ��Ƶξ� ���� ������ ����
    Transform mTransform = null;

    //����Ƽ �����Ϳ� ���ս��� �ݺ������� �����ϰ� ���ش�.
    [SerializeField] float playerRotateSpeed = 0f;//public���� �������� ���� �� private�ڵ� ����
    [SerializeField] float playerMoveSpeed = 0f;  //private�̹Ƿ� ����encapsulation �޼�

    void Start()
    {
        mTransform = this.transform;//������ ĳ���صд�.
    }

    
    void Update()
    {
        //���Է�
        float tV = Input.GetAxis("Vertical");//[-1, +1]
        float tH = Input.GetAxis("Horizontal");//[-1, +1]


        //�¿� ��ȸ
        //Rotate�Լ��� '�����'�� ������� �۵��Ѵ�. 
        mTransform.Rotate(Vector3.up, tH * playerRotateSpeed * Time.deltaTime);



        //�ӵ� = �Ÿ��� ��ȭ��/�ð��� ��ȭ��.    �����̴�
        //Vector3 tVelocity = Vector3.zero;//0���ͷ� �ʱ�ȭ
        //tVelocity = Vector3.forward * 10.0f * tV * Time.deltaTime; //������ ��Į�����
        ////������ �ð��� ���Ͽ� �ð���� ������ �Ѵ�.

        ////mTransform.Translate(tVelocity, Space.World);//World ��ǥ�� �������� �ӵ� ����
        //mTransform.Translate(tVelocity, Space.Self);   //Local��ǥ�� �������� �ӵ� ����


        //�ӵ� = �Ÿ��� ��ȭ��/�ð��� ��ȭ��.    �����̴�.
        Vector3 tVelocity = Vector3.zero;//0���ͷ� �ʱ�ȭ
        tVelocity = mTransform.forward * playerMoveSpeed * tV * Time.deltaTime; //������ ��Į�����

        //�����Ͱ� �ƴϸ� �̵�
        if(!tVelocity.Equals(Vector3.zero))
        {
            //������ �ð��� ���Ͽ� �ð���� ������ �Ѵ�.
            mTransform.Translate(tVelocity, Space.World);//World ��ǥ�� �������� �ӵ� ����
        }

        

        //mTransform.forward�� ������ǥ�� ���� ���溤�͸� ������ǥ�� ������ ��ġ�� ���³� ��

        /*
         ���ӿ������� �̵��� �����ϴ� ������
        i)   ���� ��ǥ�� �����ϴ� ���

             ���� ��ǥ = ���� ��ǥ + �ӵ� * �ð�����     
             <--���Ϸ� ��ġ�ؼ� ����� ���� �̵�     
             
             ���� ���α׷��ֿ����� ���� ����Ȯ�ص� ������ �������� �����Ͽ�
             ���Ϸ� ��ġ�ؼ��� ���� �̵� ����� ����.

        ii)  ���ӿ������� �����ϴ� �̵��Լ��� �̿��ϴ� ���
        iii) ���������� �̿��ϴ� ���
        iv)  �ִϸ��̼� �ȿ� �̵��� �����ϴ� ���
        */
    }
}
