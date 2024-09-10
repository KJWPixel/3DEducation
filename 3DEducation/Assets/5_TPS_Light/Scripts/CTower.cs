using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;


/*
  일반 탄환 발사 타워




  
*/


public class CTower : MonoBehaviour
{
    //탄환 프리팹 참조
    //[SerializeField] CBullet PFBullet = null; 

    [SerializeField] GameObject mHead = null; //포신 

   // [SerializeField] GameObject mPosFire = null; //탄환 발사 위치

    [SerializeField] GameObject mTargetObject = null; //응시대상


    void Start()
    {
        //장면 그래프에서 검색해서 찾음, 검색에 시간이 소요됨
        mTargetObject = FindObjectOfType<CPChar_step_1>().gameObject;
    }

    
    void Update()
    {
        //응시: 포신Haed가 타겟을 바라보게 하자.

        //case 0
        //임의의 크기의 임의의 방향의 벡터 구하기 = 목적지점 - 시작지점
        //Vector3 tDir = mTargetObject.transform.position - mHead.transform.position;
        //mHead.transform.forward = tDir.normalized; //전방 벡터를 직접 설정

        //case 1
        //Vector3 tDir = mTargetObject.transform.position - mHead.transform.position;
        //mHead.transform.rotation = Quaternion.LookRotation(tDir.normalized);//사원수를 이용하여 해당 방향으로 회전

        //case 2
        Vector3 tDir = mTargetObject.transform.position - mHead.transform.position;
        Quaternion tA = Quaternion.LookRotation(mHead.transform.forward); //원래 벡터의 사원수
        Quaternion tB = Quaternion.LookRotation(tDir.normalized); //회전된 후의 벡터의 사원수
        mHead.transform.rotation = Quaternion.Slerp(tA, tB, 1f * Time.deltaTime);
        //사원수의 구면선형보간을 이용하여 감쇠효과 적용, 조금 늦게 따라온다.

    }


}
