using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//3��Ī ������ ���� ������ ī�޶��ũ�� ������.


public class CFollowCam_step_0 : MonoBehaviour
{
    [SerializeField] float mDistance = 0f;//�Ĺ�Ÿ�
    [SerializeField] float mHeight = 0f;//���Ÿ�
    [SerializeField] GameObject mLookAtObj = null;//ī�޶� �ٶ󺸴� ���ӿ�����Ʈ

    [SerializeField] float mDampingTrace = 0f;


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    //��� ������ ����� ����� ���� ī�޶� ��ġ ������ �Ф� ���� LateUpdate�� �ڵ带 �ۼ�
    private void LateUpdate()
    {
        Vector3 tOffset = Vector3.zero;
        tOffset = -1f * mLookAtObj.transform.forward * mDistance + Vector3.up * mHeight;


        Vector3 tPosition = Vector3.zero;
        tPosition = mLookAtObj.transform.position + tOffset;
        //ī�޶��� ��ġ ����


        //ī�޶��� ���� ��ġ ����
        //this.transform.position = tPosition;
        this.transform.position = Vector3.Lerp(this.transform.position, tPosition, mDampingTrace);
        //Lerp Linear Interpolation ��������, �����Լ��� ����Ͽ� �ٻ�ġ�� ���Ѵ�. 
        //���� Linear: ������ ������, ���� �Լ�
        //���� Interpolation: �ٻ�ġ�� ���Ѵ�.

        //�����Ͽ� ������ ���� ������ �̾߱�.
        

        /*
         ����Ǵ� ����

          mDampingTrac�� 0.5��� ����

          Cur           New
          =================
          0              1
                 0.5
          =================
                  0      1
          =================
                      0  1
          =================
                        01
          =================
                        ...       
        */

        //ī�޶� �ٶ󺸴� ���� ����(��, ī�޶��� ȸ��)
        this.transform.LookAt(mLookAtObj.transform.position);
        
    }
}
