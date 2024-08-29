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
        //������� ����(Interpolation: �ٻ�ġ�� ���Ѵ�)
        if (GUI.Button(new Rect(200f, 0f, 100f, 100f), "Quaternion\n����"))
        {
            //z,x,y������ 90, 90, 90 ������� �������� ȸ�� ǥ��
            mEnd = Quaternion.Euler(0f, 0f, 90f) * Quaternion.Euler(90f, 0f, 0f) * Quaternion.Euler(00f, 90f, 0f);
            //z�� 90���� ���Ϸ����� ���� ȸ���� �ϰ� ������� ��ȯ�ؼ� �����Ѵ�.

            //this.transform.rotation = mEnd;

            //���������� ����ġ�� 0���� ����
            mWeight = 0f;

            //���º���
            mState = STATE.WITH_INTERPLATION;
        }



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

            //��ȯ�� ��ġ ������ ���� ���ο� ���ҵ��� �迭�� ������
            mMeshFilter = GetComponentInChildren<MeshFilter>();
            mOriginVertices = mMeshFilter.mesh.vertices; //������ ��ȸ�Ͽ� ��Ƶд�.(����Ÿ���� ���ҵ��� ��Ÿ���̹Ƿ� �ϻ�Ǿ� ��⵵�� ������� �ִ�.)

            int ti = 0;   
            while(ti<mOriginVertices.Length)
            {
                mNewVertices[ti] = mMatRot.MultiplyPoint3x4(mOriginVertices[ti]);
                //MultiplyPoint3x4 = (4 x 4) + (4 x 1) = 4 x 1������ �ش� �Լ��� ����Ͽ� 3 x 1�� ��ȯ

                ++ti;
            }

            //�޽����� ������Ʈ(ȸ�� ��ȯ ����� �����) ���ο� �޽��� �����Ѵ�.
            mMeshFilter.mesh.vertices = mNewVertices;   
        }
    }

    void Start()
    {

    }

    enum STATE
    {
        WITH_NONE = 0,
        WITH_INTERPLATION  //���� ����
    }

    STATE mState = STATE.WITH_NONE;

    float mWeight = 0.0f;


    void Update()
    {
        if(STATE.WITH_INTERPLATION == mState)
        {
            //������� ��������
            //this.transform.rotation = Quaternion.Lerp(mStart, mEnd, mWeight);
            this.transform.rotation = Quaternion.Slerp(mStart, mEnd, mWeight);//Lerp���� ���� ��ȭ�� ���̳����� �� �ε巯�� �������� �����.
            //Slerp Sphere Linear Interpolation ���� ���� ����
            /*
            ���� ������ �����Լ�(������ ������)�� �̿��Ͽ� �ٻ�ġ�� ���Ѵ�.
            ���� ���� ������ �� �� ������ (���� Ʋ���� �Ϻ���)) 'ȣ'�� ����Ͽ� �ٻ�ġ�� ���Ѵ�.

            ���� ȣ�� ��̹Ƿ�
            ���Ⱑ ���Ѵ�.
            �׷��Ƿ� ���� ��������(�Ϲ����� ������������ ���� ��ȭ�� ���̳�����)
            ǥ���� �����ϴ�.
            */ 
             
            

            mWeight += Time.deltaTime;
        }
    }
}
