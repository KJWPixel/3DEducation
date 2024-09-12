using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CEnemyThrow : MonoBehaviour
{
    [SerializeField] GameObject PFGrenade = null;

    //�߻� ��������
    [SerializeField] GameObject mPosFire = null;

    //ź������(��ǥ����)
    [SerializeField] GameObject mTarget = null;

    //����ź �߻�(������)
    public void DoFire()//DoFire(GameObject tTarget)
    {
        //���ΰ� ĳ���Ϳ��� ������ ������ ����
        mTarget = FindObjectOfType<CPChar_step_1>().gameObject;
        //mTarget = tTarget;//DoFire�� �Ű������� ������

        //test
        //mTarget.transform.position = new Vector3(10, 0, 10);

        //ź������ ������ ������ �׸��� �ӷ� ���ϱ� �˰���
        //������ 45�� ��������.

        //zx��鿡���� ���͸� ������.
        Vector3 tVelocity = Vector3.zero;

        //��ǥ����
        Vector3 tTargetPos = mTarget.transform.position;
        tTargetPos.y = 0f; //<-- zx��鸸 ���������Ƿ�
        //��������
        Vector3 tStartPos = mPosFire.transform.position;
        tStartPos.y = 0f;

        Vector3 tZXVector = (tTargetPos - tStartPos);

        //45�� ����
        tVelocity = (tZXVector.normalized + Vector3.up).normalized;//������ ���⺤�͸� ����

        //ũ��
        //45���� ������ �ʱ�ӷ�
        //zx��鿡���� ������ ���� ������ �Ÿ�
        float tDZX = tZXVector.magnitude;

        //y�࿡�� ������ ���������� �Ÿ�
        float tDY = tTargetPos.y - tStartPos.y;//�ϴ� 0
        

        float tCos = Mathf.Cos(45f * Mathf.Deg2Rad);
        float tSin = Mathf.Sin(45f * Mathf.Deg2Rad);
        float tTan = tSin / tCos;// y��ȭ��/x��ȭ��

        //�ʱ� �ӷ� ����
        float tScalarSpeed = (tDZX / tCos) * Mathf.Sqrt((-1) * 9.8f / (2f * (tDY - tTan * tDZX)));

        //ũ����� �����Ͽ� �ӵ� ����
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
