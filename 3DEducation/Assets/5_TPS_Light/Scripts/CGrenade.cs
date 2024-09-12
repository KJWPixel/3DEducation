using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGrenade : MonoBehaviour
{
    [SerializeField] GameObject PFEfxExplosion = null;
    
    void Start()
    {
        //��ũtorque: ȸ���� ����
        GetComponent<Rigidbody>().AddTorque(Vector3.one * 200.0f);
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //��ǥ���̸� �浹 �˻�
        if(other.CompareTag("tagFloor"))
        {
            Debug.Log("<size='18'>OnTriggerEnter on floor</size>");

            //���� ��ƼŬ ����
            GameObject tEfx = Instantiate<GameObject>(PFEfxExplosion, this.transform.position, Quaternion.identity);
            Destroy(tEfx, 2f);

            //����ź ����
            Destroy(this.gameObject);

            //���߷� ����

            //���� �̿��� �浹ü���� �浹 ����
            //Collider[] tColliders = Physics.OverlapSphere(this.transform.position, 10f);

            // 3�� ���̾�� �з��� �浹ü����, ���� �̿��� �浹ü���� �浹 ����
            // Collider[] tColliders = Physics.OverlapSphere(this.transform.position, 10f, 1<<3);
            //�̷� ��Ʈ �����ڰ� ���ǰ� �ִ� ������ 32���� ���̾ �ϳ��� ���������� ǥ���ϱ� ���ؼ���.

            // 3�� ���̾�� �з��� �浹ü����, ���� �̿��� �浹ü���� �浹 ����
            //Collider[] tColliders = Physics.OverlapSphere(this.transform.position, 10f, 1 << 6);

            // 3�� ���̾�� �з��� �浹ü����, ���� �̿��� �浹ü���� �浹 ����
            Collider[] tColliders = Physics.OverlapSphere(this.transform.position, 10f, 1 << 3 | 1<< 6 );

            foreach (var t in tColliders)
            {
                Debug.Log("<color='red'>collider...</color>");

                t.GetComponent<Rigidbody>().AddExplosionForce(1500f, this.transform.position, 10f, 1200f);
            }
        }
    }
}
