using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//rigidbody ��ü
//    ź���� ���� ���� �̻����� ��ü, F = ma <-- ������ �������
// ������ ���������� ������ �������п� ����Ѵ�.

/*
  źȯ ���� �˰���

  i)�Ϲ�ź
    ������ �̸� ������ źȯ

  
  ii)����ź
     �����߿� �����Ͽ� ������ �����ϴ� źȯ

  
  iii)����ź


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
            //źȯ �߻� ��ƾ
            // i)�߻�������� ����
            // ii)źȯ�� �ӵ� ����
            // iii)źȯ Ȱ��ȭz  

            //DoFire();

            //Vector3 tTargetPosition = mTargetObject.transform.position;
            //DoFireAimed(tTargetPosition);

            DoFireCircled();
        }
    }

    //�Ϲ� źȯ �߻�
    void DoFire()
    {
        Vector3 tPositionFire = Vector3.zero;
        tPositionFire = this.transform.position;

        //z�� �������� ������ �Ϲ�źȯ ���� 
        Vector3 tVelocity = Vector3.zero;
        tVelocity = Vector3.forward * 10f;

        CBullet tBullet = Instantiate<CBullet>(PFBullet, tPositionFire, Quaternion.identity);
        tBullet.GetComponent<Rigidbody>().AddForce(tVelocity, ForceMode.Impulse);//��ü�� �̿��Ͽ� F = m�� ���� ���Ѵ�.
        //�̹� �����ӿ� �־��� �� tVelocity�� ��� ���Ѵ�.

    }

    //���� źȯ �߻�
    void DoFireAimed(Vector3 tPositionTarget)
    {
        Vector3 tPositionFire = Vector3.zero;
        tPositionFire = this.transform.position;

        //�����Ͽ� ������ ����
        Vector3 tVelocity = Vector3.zero;
        tVelocity = tPositionTarget - tPositionFire;//������ ���� ����
        tVelocity = tVelocity.normalized;//����ȭ
        tVelocity = tVelocity * 30f;//ũ�� ����
        

        CBullet tBullet = Instantiate<CBullet>(PFBullet, tPositionFire, Quaternion.identity);
        tBullet.GetComponent<Rigidbody>().AddForce(tVelocity, ForceMode.Impulse);//��ü�� �̿��Ͽ� F = m�� ���� ���Ѵ�.
        //�̹� �����ӿ� �־��� �� tVelocity�� ��� ���Ѵ�.

    }

    //���� źȯ �߻�
    //����ź�� �⺻������ ����ź�� ����.
    //�ٸ� źȯ�� �׻� ���� �ð� �������� �������ϴ� ���̴�.

    //���� źȯ �߻�
    void DoFireCircled()
    {
        float tAngleDegree = 0f;

        for(int ti = 0; ti < 8; ++ti)
        {
            Vector3 tPositionFire = Vector3.zero;
            tPositionFire = this.transform.position;

            Vector3 tVelocity = Vector3.zero;

            tVelocity.x = 1f * Mathf.Cos(Mathf.Deg2Rad * tAngleDegree);//45����� ������ Radian���� ��ȯ��Ű�ش�.
            tVelocity.z = 1f * Mathf.Sin(Mathf.Deg2Rad * tAngleDegree);
            tVelocity = tVelocity * 30f;

            tAngleDegree += 45f;

            CBullet tBullet = Instantiate<CBullet>(PFBullet, tPositionFire, Quaternion.identity);
            tBullet.GetComponent<Rigidbody>().AddForce(tVelocity, ForceMode.Impulse);//��ü�� �̿��Ͽ� F = m�� ���� ���Ѵ�.
            //�̹� �����ӿ� �־��� �� tVelocity�� ��� ���Ѵ�.
        }
    }
}
