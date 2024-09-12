using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGrenade : MonoBehaviour
{
    [SerializeField] GameObject PFEfxExplosion = null;
    
    void Start()
    {
        //토크torque: 회전력 적용
        GetComponent<Rigidbody>().AddTorque(Vector3.one * 200.0f);
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //지표면이면 충돌 검사
        if(other.CompareTag("tagFloor"))
        {
            Debug.Log("<size='18'>OnTriggerEnter on floor</size>");

            //폭발 파티클 생성
            GameObject tEfx = Instantiate<GameObject>(PFEfxExplosion, this.transform.position, Quaternion.identity);
            Destroy(tEfx, 2f);

            //수류탄 삭제
            Destroy(this.gameObject);

            //폭발력 적용

            //구를 이용한 충돌체들의 충돌 검출
            //Collider[] tColliders = Physics.OverlapSphere(this.transform.position, 10f);

            // 3번 레이어로 분류된 충돌체들을, 구를 이용한 충돌체들의 충돌 검출
            // Collider[] tColliders = Physics.OverlapSphere(this.transform.position, 10f, 1<<3);
            //이런 비트 연산자가 사용되고 있는 이유는 32개의 레이어를 하나의 정수단위로 표현하기 위해서다.

            // 3번 레이어로 분류된 충돌체들을, 구를 이용한 충돌체들의 충돌 검출
            //Collider[] tColliders = Physics.OverlapSphere(this.transform.position, 10f, 1 << 6);

            // 3번 레이어로 분류된 충돌체들을, 구를 이용한 충돌체들의 충돌 검출
            Collider[] tColliders = Physics.OverlapSphere(this.transform.position, 10f, 1 << 3 | 1<< 6 );

            foreach (var t in tColliders)
            {
                Debug.Log("<color='red'>collider...</color>");

                t.GetComponent<Rigidbody>().AddExplosionForce(1500f, this.transform.position, 10f, 1200f);
            }
        }
    }
}
