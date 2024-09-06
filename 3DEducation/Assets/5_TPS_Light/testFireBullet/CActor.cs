using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//rigidbody 강체
//    탄성이 없는 가장 이상적인 물체, F = ma <-- 뉴턴의 운동방정식
// 게임의 물리엔진은 뉴턴의 고전역학에 기반한다.

/*
  탄환 궤적 알고리즘

  i)일반탄
    방향이 미리 정해진 탄환

  
  ii)조준탄
     실행중에 조준하여 방향을 결정하는 탄환

  
  iii)원형탄


*/
public class CActor : MonoBehaviour
{

    [SerializeField] CBullet PFBullet = null;

    [SerializeField] GameObject mTargetObject = null;

    void Start()
    {

    }


    void Update()
    {
        //do fire a bullet
        if (Input.GetMouseButtonDown(0))
        {
            //탄환 발사 루틴
            // i)발사시작지점 설정
            // ii)탄환의 속도 설정
            // iii)탄환 활성화z  

            //DoFire();

            //Vector3 tTargetPosition = mTargetObject.transform.position;
            //DoFireAimed(tTargetPosition);

            DoFireCircled();
        }
    }

    //일반 탄환 발사
    void DoFire()
    {
        Vector3 tPositionFire = Vector3.zero;
        tPositionFire = this.transform.position;

        //z축 방향으로 설정된 일반탄환 가정 
        Vector3 tVelocity = Vector3.zero;
        tVelocity = Vector3.forward * 10f;

        CBullet tBullet = Instantiate<CBullet>(PFBullet, tPositionFire, Quaternion.identity);
        tBullet.GetComponent<Rigidbody>().AddForce(tVelocity, ForceMode.Impulse);//강체를 이용하여 F = m로 힘을 가한다.
        //이번 프레임에 주어진 힘 tVelocity를 모두 가한다.

    }

    //조준 탄환 발사
    void DoFireAimed(Vector3 tPositionTarget)
    {
        Vector3 tPositionFire = Vector3.zero;
        tPositionFire = this.transform.position;

        //조준하여 방향을 결정
        Vector3 tVelocity = Vector3.zero;
        tVelocity = tPositionTarget - tPositionFire;//임의의 벡터 구함
        tVelocity = tVelocity.normalized;//정규화
        tVelocity = tVelocity * 30f;//크기 적용
        

        CBullet tBullet = Instantiate<CBullet>(PFBullet, tPositionFire, Quaternion.identity);
        tBullet.GetComponent<Rigidbody>().AddForce(tVelocity, ForceMode.Impulse);//강체를 이용하여 F = m로 힘을 가한다.
        //이번 프레임에 주어진 힘 tVelocity를 모두 가한다.

    }

    //유도 탄환 발사
    //유도탄은 기본적으로 조준탄과 같다.
    //다만 탄환이 항상 일정 시간 간격으로 재조준하는 것이다.

    //원형 탄환 발사
    void DoFireCircled()
    {
        float tAngleDegree = 0f;

        for(int ti = 0; ti < 8; ++ti)
        {
            Vector3 tPositionFire = Vector3.zero;
            tPositionFire = this.transform.position;

            Vector3 tVelocity = Vector3.zero;

            tVelocity.x = 1f * Mathf.Cos(Mathf.Deg2Rad * tAngleDegree);//45도라는 각도를 Radian으로 변환시키준다.
            tVelocity.z = 1f * Mathf.Sin(Mathf.Deg2Rad * tAngleDegree);
            tVelocity = tVelocity * 30f;

            tAngleDegree += 45f;

            CBullet tBullet = Instantiate<CBullet>(PFBullet, tPositionFire, Quaternion.identity);
            tBullet.GetComponent<Rigidbody>().AddForce(tVelocity, ForceMode.Impulse);//강체를 이용하여 F = m로 힘을 가한다.
            //이번 프레임에 주어진 힘 tVelocity를 모두 가한다.
        }
    }
}
