using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTowerLaser : MonoBehaviour
{
    [SerializeField] GameObject PFEfxSpark = null;

    [SerializeField] GameObject mPosFire = null; //발사 위치

    //라인렌더러 컴포넌트 참조
    //LineRenderer <-- 선 형태의 가시적인 결과물을 만들어내는 컴포넌트. N개의 정점을 가진다.
    [SerializeField] LineRenderer mLineRenderer = null;

    private void Awake()
    {
        mLineRenderer = GetComponent<LineRenderer>();
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        //반직선과 충돌체를 통한 충돌 검출
        RaycastHit tHit;
        //반직선
        bool tIsCollision = Physics.Raycast(mPosFire.transform.position, mPosFire.transform.forward, out tHit, Mathf.Infinity);
            
        if(tIsCollision)
        {
            Debug.Log("collision!");

            //충돌 지점의 평면의 법선벡터 방향으로 회전
            Quaternion tRot = Quaternion.LookRotation(tHit.normal);
            GameObject tEfx = Instantiate<GameObject>(PFEfxSpark, tHit.point, tRot);
            Destroy(tEfx, 1f);

            if(tHit.collider.CompareTag("tagActor"))
            {
                Debug.Log("<color='red'>Hit Actor! </color>");
            }


            //레이저 외관 표현
            //여기서는 맞은 지점을 끝점으로 삼겠다.
            Vector3 tStart = mPosFire.transform.position;
            Vector3 tEnd = tHit.point;

            mLineRenderer.SetPosition(0, tStart);
            mLineRenderer.SetPosition(1, tEnd);
        }
        else
        {
            Debug.Log("NOT collision!");

            //레이저 외관 표현
            //여기서는 시점에서 100미터 떨어진 곳을 끝점을 삼겠다.
            Vector3 tStart = mPosFire.transform.position;
            Vector3 tDirFire = mPosFire.transform.forward.normalized;
            Vector3 tEnd = tStart + tDirFire * 100f;
            //<-- 100미터가 사정거리라고 가정

            mLineRenderer.SetPosition(0, tStart);
            mLineRenderer.SetPosition(1, tEnd);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(mPosFire.transform.position, mPosFire.transform.forward * 100f);
    }
}
