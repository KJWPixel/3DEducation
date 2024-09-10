using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTowerFireBullet : MonoBehaviour
{
    //źȯ ������ ����
    [SerializeField] CBullet PFBullet = null;

    [SerializeField] GameObject mPosFire = null; //źȯ �߻� ��ġ

    [SerializeField] float minterval = 1f;

    void Start()
    {
        //�ڷ�ƾ���� �ص���
        InvokeRepeating("DoFire", 3f, minterval);
    }

    
    void Update()
    {
        
    }

    void DoFire()
    {
        Vector3 tPositionFire = Vector3.zero;
        tPositionFire = mPosFire.transform.position;

        //z�� �������� ������ �Ϲ�źȯ ���� 
        Vector3 tVelocity = Vector3.zero;
        tVelocity = mPosFire.transform.forward * 30f;

        CBullet tBullet = Instantiate<CBullet>(PFBullet, tPositionFire, Quaternion.identity);
        tBullet.GetComponent<Rigidbody>().AddForce(tVelocity, ForceMode.Impulse);//��ü�� �̿��Ͽ� F = m�� ���� ���Ѵ�.
        //�̹� �����ӿ� �־��� �� tVelocity�� ��� ���Ѵ�.
    }
}
