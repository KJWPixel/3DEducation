using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CFollowCam_step_1 : MonoBehaviour
{
    //카메라가 바라보는 게임오브젝트
    [SerializeField] GameObject mLookAtObj = null;

    //캐릭터로부터 카메라가 떨어진 거리(길이)
    [SerializeField] float mArmLength = 0f;

    //캐릭터로부터 얼마나 떨어져 있는지에 대한 범위
    [SerializeField] Vector3 mOffset = Vector3.zero;

    //마우스는 2D좌표계의 개념
    float mMouseXVal = 0f;//마우스 X입력값( 카메라를 3D Y축을 회전축으로 좌우 회전)
    float mMouseYVal = 0f;//마우스 Y입력값( 카메라를 3D X축을 회전축응로 상하 회전)

    void Start()
    {
        //mOffset = new Vector3(0f, 0f, -1f * mArmLength);
        //mMouseYValu = 45f;
        mMouseYVal = this.transform.rotation.eulerAngles.x;//<--오일러 각, x축을 회전축으로 한 각도

        //설정된 회전값을 적용해둠
        this.transform.rotation = Quaternion.Euler(mMouseYVal, mMouseXVal, 0f);
    }

    
    void Update()
    {
        float tMouseX = Input.GetAxis("Mouse X");
        float tMouseY = Input.GetAxis("Mouse Y");

        mMouseXVal = mMouseXVal + tMouseX;
        mMouseYVal = mMouseYVal + tMouseY * (-1.0f);
        //Screen에서는 윈도우 좌표계(2D)를 사용한다. 여기서는 y축 방향이 뒤집어져 있으므로 -1를 곱하여 반전한다.

        //오일러 각에 의한 회전을 연산하고 이것을 '사원수로 변환'하여 적용
        this.transform.rotation = Quaternion.Euler(mMouseYVal, mMouseXVal, 0f);
    }

    private void LateUpdate()
    {
        //벡터끼리의 덧셈연산
        //위치 = 위치 + 벡터
        //this.transform.position = mLookAtObj.transform.position + mOffset;

        //위치 = 위치 + 사원수 * 벡터
        //<--- 사원수 * 벡터는 벡터의 결과를 내도록 유니티에 구현되어있다.   
        this.transform.position = mLookAtObj.transform.position + this.transform.rotation * mOffset;
    }
}
