using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//z,x,y������ ���� 90�� degree ȸ��



public class CSlime_quaternion : MonoBehaviour
{
    Quaternion mStart = Quaternion.identity; //������ ����� (ȸ���� ���� ����ǹ�)�� �ʱ�ȭ 
    Quaternion mEnd = Quaternion.identity;   //������ ����� (ȸ���� ���� ����ǹ�)�� �ʱ�ȭ 

    //���Ϸ� ���� ���� ȸ�� ��ȯ ���
    Matrix4x4 mMatRot = Matrix4x4.identity; //�������(��� ������ �׵��)�� �ʱ�ȭ

    MeshFilter mMeshFilter = null; //���� �������� ��ȸ�ϱ� ���� �޽����� ������Ʈ�� ����
    Vector3[] mOriginVertices;     //�޽��� ���� ������
    Vector3[] mNewVertices;        //ȸ�� ��ȯ�� ����� ���ο� ������



    //IMGUI ���߿����� ���� ���̴� UI���۽ý����̴�.

    private void OnGUI()
    {
        if (GUI.Button(new Rect(0f, 0f, 100f, 100f), "Quaternion����"))
        {
            //z,x,y������ 90, 90, 90 ������� �������� ȸ�� ǥ��
            mEnd = Quaternion.Euler(0f, 0f, 90f) * Quaternion.Euler(90f,0f,0f) * Quaternion.Euler(00f, 90f, 0f);
            //z�� 90���� ���Ϸ����� ���� ȸ���� �ϰ� ������� ��ȯ�ؼ� �����Ѵ�.

            this.transform.rotation = mEnd; 
        }

        if (GUI.Button(new Rect(0f, 100f, 100f, 100f), "Transform.Rotate"))
        {
            //z,x,y������ 90, 90, 90 transform���� �����ϴ� Rotate�Լ��� ȸ�� ǥ��
            //<--transform.Rotate�� ������� ������� ��������ִ�.
            this.transform.Rotate(0f, 0f, 90f);
            this.transform.Rotate(90f, 0f, 0f);
            this.transform.Rotate(0f, 90f, 0f);
        }

        if (GUI.Button(new Rect(0f, 300f, 100f, 100f), "Euler\nQuaternion.Euler"))
        {
            //z,x,y������ 90, 90, 90�� Quaternion.Euler�� ǥ��
            //�̰��� ���Ϸ� ���� ���� ȸ���� ������ ����� ������� ���� ����. ��, ���Ϸ� ���� ���� ȸ���̴�.
            mEnd = Quaternion.Euler(90f, 90f, 90f);

            this.transform.rotation = mEnd;
        }

        if (GUI.Button(new Rect(0f, 400f, 100f, 100f), "Euler\nMatrix Rot"))
        {
            Matrix4x4 tM = Matrix4x4.identity;
            tM.SetTRS(Vector3.zero, Quaternion.Euler(0f, 0f, 90f), Vector3.one);//z�� ȸ�������� 90�� ȸ�� ���
            mMatRot = tM * mMatRot;

            tM = Matrix4x4.identity;
            tM.SetTRS(Vector3.zero, Quaternion.Euler(90f, 0f, 0f), Vector3.one);//x�� ȸ�������� 90�� ȸ�� ���
            mMatRot = tM * mMatRot;

            tM = Matrix4x4.identity;
            tM.SetTRS(Vector3.zero, Quaternion.Euler(0f, 90f, 0f), Vector3.one);//y�� ȸ�������� 90�� ȸ�� ���
            mMatRot = tM * mMatRot;

            mMeshFilter = GetComponentInChildren<MeshFilter>();
            mOriginVertices = mMeshFilter.mesh.vertices; //������ ��ȸ�Ͽ� ��Ƶд�.
        }
    }

    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
}
