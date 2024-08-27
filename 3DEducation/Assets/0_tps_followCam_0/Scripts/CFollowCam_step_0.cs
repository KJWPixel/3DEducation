using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//3인칭 시점에 보다 유연한 카메라워크를 만들어보자.


public class CFollowCam_step_0 : MonoBehaviour
{
    [SerializeField] float mDistance = 0f;//후방거리
    [SerializeField] float mHeight = 0f;//상방거리
    [SerializeField] GameObject mLookAtObj = null;//카메라가 바라보는 게임오브젝트

    [SerializeField] float mDampingTrace = 0f;


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    //모든 렌더링 결과를 만들고 나서 카메라 위치 조정을 학ㅣ 위해 LateUpdate에 코드를 작성
    private void LateUpdate()
    {
        Vector3 tOffset = Vector3.zero;
        tOffset = -1f * mLookAtObj.transform.forward * mDistance + Vector3.up * mHeight;


        Vector3 tPosition = Vector3.zero;
        tPosition = mLookAtObj.transform.position + tOffset;
        //카메라의 위치 구함


        //카메라의 현재 위치 설정
        //this.transform.position = tPosition;
        this.transform.position = Vector3.Lerp(this.transform.position, tPosition, mDampingTrace);
        //Lerp Linear Interpolation 선형보간, 일차함수를 사용하여 근사치를 구한다. 
        //선형 Linear: 직선의 방정식, 일차 함수
        //보간 Interpolation: 근사치를 구한다.

        //관련하여 직선의 벡터 방정식 이야기.
        

        /*
         감쇠되는 원리

          mDampingTrac를 0.5라고 가정

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

        //카메라가 바라보는 지점 설정(즉, 카메라의 회전)
        this.transform.LookAt(mLookAtObj.transform.position);
        
    }
}
