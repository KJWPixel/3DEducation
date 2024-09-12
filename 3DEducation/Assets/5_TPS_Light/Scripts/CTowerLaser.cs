using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTowerLaser : MonoBehaviour
{
    [SerializeField] GameObject PFEfxSpark = null;

    [SerializeField] GameObject mPosFire = null; //�߻� ��ġ

    //���η����� ������Ʈ ����
    //LineRenderer <-- �� ������ �������� ������� ������ ������Ʈ. N���� ������ ������.
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
        //�������� �浹ü�� ���� �浹 ����
        RaycastHit tHit;
        //������
        bool tIsCollision = Physics.Raycast(mPosFire.transform.position, mPosFire.transform.forward, out tHit, Mathf.Infinity);
            
        if(tIsCollision)
        {
            Debug.Log("collision!");

            //�浹 ������ ����� �������� �������� ȸ��
            Quaternion tRot = Quaternion.LookRotation(tHit.normal);
            GameObject tEfx = Instantiate<GameObject>(PFEfxSpark, tHit.point, tRot);
            Destroy(tEfx, 1f);

            if(tHit.collider.CompareTag("tagActor"))
            {
                Debug.Log("<color='red'>Hit Actor! </color>");
            }


            //������ �ܰ� ǥ��
            //���⼭�� ���� ������ �������� ��ڴ�.
            Vector3 tStart = mPosFire.transform.position;
            Vector3 tEnd = tHit.point;

            mLineRenderer.SetPosition(0, tStart);
            mLineRenderer.SetPosition(1, tEnd);
        }
        else
        {
            Debug.Log("NOT collision!");

            //������ �ܰ� ǥ��
            //���⼭�� �������� 100���� ������ ���� ������ ��ڴ�.
            Vector3 tStart = mPosFire.transform.position;
            Vector3 tDirFire = mPosFire.transform.forward.normalized;
            Vector3 tEnd = tStart + tDirFire * 100f;
            //<-- 100���Ͱ� �����Ÿ���� ����

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
