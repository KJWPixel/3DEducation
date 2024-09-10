using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTowerFireBullet : MonoBehaviour
{
    //탄환 프리팹 참조
    [SerializeField] CBullet PFBullet = null;

    [SerializeField] GameObject mPosFire = null; //탄환 발사 위치

    [SerializeField] float minterval = 1f;

    void Start()
    {
        //코루틴으로 해도됨
        InvokeRepeating("DoFire", 3f, minterval);
    }

    
    void Update()
    {
        
    }

    void DoFire()
    {
        Vector3 tPositionFire = Vector3.zero;
        tPositionFire = mPosFire.transform.position;

        //z축 방향으로 설정된 일반탄환 가정 
        Vector3 tVelocity = Vector3.zero;
        tVelocity = mPosFire.transform.forward * 30f;

        CBullet tBullet = Instantiate<CBullet>(PFBullet, tPositionFire, Quaternion.identity);
        tBullet.GetComponent<Rigidbody>().AddForce(tVelocity, ForceMode.Impulse);//강체를 이용하여 F = m로 힘을 가한다.
        //이번 프레임에 주어진 힘 tVelocity를 모두 가한다.
    }
}
