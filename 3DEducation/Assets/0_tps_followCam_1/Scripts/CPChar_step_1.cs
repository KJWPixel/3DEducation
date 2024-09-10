using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPChar_step_1 : MonoBehaviour
{
    //ĳ������ ������(�̵�)�� ����ϴ� ������Ʈ�� �̰��� �׻� ����� ���̶�� ����
    //<-- �׷��� ����Ƽ �����Ϳ��� �����ϵ��� �ϰڴ� ��� ����
    [SerializeField]
    CharacterController mCharController = null;
    //Rigidbody ������ ������� �ʴ� ��� ���


    //�ӷ�
    [SerializeField] float mSpeed = 0f;

    //�ӵ�
    [SerializeField] Vector3 mVelocity = Vector3.zero;

    //�߷°��ӵ� y����, �� ��Į��
    [SerializeField] float Gravity = -9.8f;

    //���� ���� ũ��(�ӵ��� y���п� �����Ǵ� ��Į��)
    [SerializeField] float mJumpower = 0f;

    //���콺 Ŀ�� ����
    [SerializeField] CursorLockMode mCurcorLockMode = CursorLockMode.None;

    void Start()
    {
        //mCurcorLockMode = CursorLockMode.Confined;//â ������ ���콺 Ŀ���� ���� �����ϱ�
        //mCurcorLockMode = CursorLockMode.Locked;//���콺 �������� ��װ� ǥ�ø� ����

        //Ŀ�� ���� ����
        Cursor.lockState = mCurcorLockMode;
    }

    
    void Update()
    {
        //��ǥ�鿡 ����ִٸ�
        if (mCharController.isGrounded)
        {
            //���Է�
            float tV = Input.GetAxisRaw("Vertical");//[-1, +1]
            float tH = Input.GetAxisRaw("Horizontal");

            //������ �����غ��� Move�� ������ǥ�� �������� �۵��Ѵ�.
            //�׷��Ƿ� ���ΰ�ĳ���� �����̶�� ǥ���� �ʿ��ϴ�.
            //�Է°��� �������, zx��鿡���� �ӵ� ����
            Vector3 tVelocity = new Vector3(tH, 0f, tV); //local��ǥ�� �󿡼� ���� ����
            tVelocity = mCharController.transform.TransformDirection(tVelocity); //local --> world��ȯ�� ����
            mVelocity = tVelocity.normalized * mSpeed;//���ǟ� �ӵ� ����
        }
        else//��ǥ�鿡 ������� �ʴٸ� (��, �����̶��)
        {
            //���� �ӵ� = �����ӵ� + ���ӵ� * �ð�����
            //���� ��ġ = ������ġ + �ӵ� * �ð�����

            //���Ϸ� ����� ���� �ӵ� ������
            mVelocity.y = mVelocity.y + Gravity * Time.deltaTime;
        }
        
        if(Input.GetKeyDown(KeyCode.Space))
        {
            mVelocity.y = mJumpower;
        }
        
        //CharacterController���� �����ϴ� Move�Լ��� �̿��Ѵ�.
        //<-- ���Ϸ� ��ġ�ؼ� ����� ���� �۵��ϴ� �Լ��̴�.
        mCharController.Move(mVelocity * Time.deltaTime);//<--�ð���� ����




        //ī�޶� �ٶ󺸴� �������� ��ȸ
        Vector3 tDir = Camera.main.transform.forward;
        tDir.y = 0f;
        //ī�޶� ���� ������ ������ ������ ���Ѵ�.
        Vector3 tLookAtPosition = this.transform.position + tDir;
        //ī�޶� ���� ������ ������ ������ �ٶ󺸰� �Ѵ�.
        this.transform.LookAt(tLookAtPosition);


        //���� ���콺 ��ư Ŭ���� �߻� 
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("<size='14'> PChar Do Fire </size>");
            DoFire_RayCast();
        }
    }

    private void DoFire_RayCast()
    {
        //ī�޶󿡼� �߻�Ǵ� ������ ���ϱ� (Screen --> World Space)
        Ray Ray = Camera.main.ScreenPointToRay(Input.mousePosition);


        //�������� �浹ü�� ���� �浹 ����
        RaycastHit tHit;
        //������
        bool tIsCollision = Physics.Raycast(Ray, out tHit, Mathf.Infinity);

        if (tIsCollision)
        {
            Debug.Log("Fire!");

            //if (tHit.collider.CompareTag("tagActor"))
            //{
            //    Debug.Log("<color='red'>Hit Actor! </color>");
            //}
        }
        else
        {
            Debug.Log("NOT collision!");
        }

    }
}
