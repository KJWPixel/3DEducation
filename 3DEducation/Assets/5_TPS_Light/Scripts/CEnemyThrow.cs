using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CEnemyThrow : MonoBehaviour
{
    [SerializeField] GameObject PFGrenade = null;

    //발사 시작지점
    [SerializeField] GameObject mPosFire = null;

    //탄착지점(목표지점)
    [SerializeField] GameObject mTarget = null;

    //수류탄 발사(던지기)
    public void DoFire()//DoFire(GameObject tTarget)
    {
        //주인공 캐릭터에게 던지는 것으로 가정
        mTarget = FindObjectOfType<CPChar_step_1>().gameObject;
        //mTarget = tTarget;//DoFire의 매개변수가 있을때

        //test
        //mTarget.transform.position = new Vector3(10, 0, 10);

        //탄착지점 포물선 궤적을 그리는 속력 구하기 알고리즘
        //각도는 45도 가정하자.

        //zx평면에서의 벡터를 구하자.
        Vector3 tVelocity = Vector3.zero;

        //목표지점
        Vector3 tTargetPos = mTarget.transform.position;
        tTargetPos.y = 0f; //<-- zx평면만 가정했으므로
        //시작지점
        Vector3 tStartPos = mPosFire.transform.position;
        tStartPos.y = 0f;

        Vector3 tZXVector = (tTargetPos - tStartPos);

        //45도 가정
        tVelocity = (tZXVector.normalized + Vector3.up).normalized;//순수한 방향벡터를 구함

        //크기
        //45도를 가정한 초기속력
        //zx평면에서의 시점과 종점 사이의 거리
        float tDZX = tZXVector.magnitude;

        //y축에서 시점과 종점사이의 거리
        float tDY = tTargetPos.y - tStartPos.y;//일단 0
        

        float tCos = Mathf.Cos(45f * Mathf.Deg2Rad);
        float tSin = Mathf.Sin(45f * Mathf.Deg2Rad);
        float tTan = tSin / tCos;// y변화량/x변화량

        //초기 속력 결정
        float tScalarSpeed = (tDZX / tCos) * Mathf.Sqrt((-1) * 9.8f / (2f * (tDY - tTan * tDZX)));

        //크기까지 적용하여 속도 결정
        tVelocity = tVelocity * tScalarSpeed;

        GameObject tGrenade = Instantiate<GameObject>(PFGrenade, mPosFire.transform.position, Quaternion.identity);

        tGrenade.GetComponent<Rigidbody>().AddForce(tVelocity, ForceMode.Impulse);
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnGUI()
    {
        if(GUI.Button(new Rect(0,0,100,100), "test Throw"))
        {
            DoFire();
            //DoFire(mTarget);
            //DoFire()
        }
    }
}
