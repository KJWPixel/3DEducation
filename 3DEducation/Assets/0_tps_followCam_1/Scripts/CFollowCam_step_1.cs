using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CFollowCam_step_1 : MonoBehaviour
{
    //ī�޶� �ٶ󺸴� ���ӿ�����Ʈ
    [SerializeField] GameObject mLookAtObj = null;

    //ĳ���ͷκ��� ī�޶� ������ �Ÿ�(����)
    [SerializeField] float mArmLength = 0f;

    //ĳ���ͷκ��� �󸶳� ������ �ִ����� ���� ����
    [SerializeField] Vector3 mOffset = Vector3.zero;

    //���콺�� 2D��ǥ���� ����
    float mMouseXVal = 0f;//���콺 X�Է°�( ī�޶� 3D Y���� ȸ�������� �¿� ȸ��)
    float mMouseYVal = 0f;//���콺 Y�Է°�( ī�޶� 3D X���� ȸ�������� ���� ȸ��)

    void Start()
    {
        //mOffset = new Vector3(0f, 0f, -1f * mArmLength);
        //mMouseYValu = 45f;
        mMouseYVal = this.transform.rotation.eulerAngles.x;//<--���Ϸ� ��, x���� ȸ�������� �� ����

        //������ ȸ������ �����ص�
        this.transform.rotation = Quaternion.Euler(mMouseYVal, mMouseXVal, 0f);
    }

    
    void Update()
    {
        float tMouseX = Input.GetAxis("Mouse X");
        float tMouseY = Input.GetAxis("Mouse Y");

        mMouseXVal = mMouseXVal + tMouseX;
        mMouseYVal = mMouseYVal + tMouseY * (-1.0f);
        //Screen������ ������ ��ǥ��(2D)�� ����Ѵ�. ���⼭�� y�� ������ �������� �����Ƿ� -1�� ���Ͽ� �����Ѵ�.

        //���Ϸ� ���� ���� ȸ���� �����ϰ� �̰��� '������� ��ȯ'�Ͽ� ����
        this.transform.rotation = Quaternion.Euler(mMouseYVal, mMouseXVal, 0f);
    }

    private void LateUpdate()
    {
        //���ͳ����� ��������
        //��ġ = ��ġ + ����
        //this.transform.position = mLookAtObj.transform.position + mOffset;

        //��ġ = ��ġ + ����� * ����
        //<--- ����� * ���ʹ� ������ ����� ������ ����Ƽ�� �����Ǿ��ִ�.   
        this.transform.position = mLookAtObj.transform.position + this.transform.rotation * mOffset;
    }
}
