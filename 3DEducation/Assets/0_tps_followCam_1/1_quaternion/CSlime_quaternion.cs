using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//z,x,y순으로 각각 90도 degree 회전



public class CSlime_quaternion : MonoBehaviour
{
    Quaternion mStart = Quaternion.identity; //단위원 사원수 (회전이 없다 라는의미)로 초기화 
    Quaternion mEnd = Quaternion.identity;   //단위원 사원수 (회전이 없다 라는의미)로 초기화 

    //오일러 각에 의한 회전 변환 행렬
    Matrix4x4 mMatRot = Matrix4x4.identity; //단위행렬(행렬 곱셈의 항등원)로 초기화

    MeshFilter mMeshFilter = null; //정점 정보들을 조회하기 위한 메쉬필터 컴포넌트의 참조
    Vector3[] mOriginVertices;     //메쉬의 원래 정점들
    Vector3[] mNewVertices;        //회전 변환이 적용된 새로운 정점들



    //IMGUI 개발용으로 자주 쓰이는 UI제작시스템이다.

    private void OnGUI()
    {
        if (GUI.Button(new Rect(0f, 0f, 100f, 100f), "Quaternion곱셈"))
        {
            //z,x,y순으로 90, 90, 90 사원수의 곱셈으로 회전 표현
            mEnd = Quaternion.Euler(0f, 0f, 90f) * Quaternion.Euler(90f,0f,0f) * Quaternion.Euler(00f, 90f, 0f);
            //z축 90도로 오일러각에 의한 회전을 하고 사원수로 변환해서 리턴한다.

            this.transform.rotation = mEnd; 
        }

        if (GUI.Button(new Rect(0f, 100f, 100f, 100f), "Transform.Rotate"))
        {
            //z,x,y순으로 90, 90, 90 transform에서 제공하는 Rotate함수로 회전 표현
            //<--transform.Rotate는 사원수를 기반으로 만들어져있다.
            this.transform.Rotate(0f, 0f, 90f);
            this.transform.Rotate(90f, 0f, 0f);
            this.transform.Rotate(0f, 90f, 0f);
        }

        if (GUI.Button(new Rect(0f, 300f, 100f, 100f), "Euler\nQuaternion.Euler"))
        {
            //z,x,y순으로 90, 90, 90를 Quaternion.Euler로 표현
            //이것은 오일러 각에 의한 회전을 적용한 결과를 사원수로 만든 것이. 즉, 오일러 각의 의한 회전이다.
            mEnd = Quaternion.Euler(90f, 90f, 90f);

            this.transform.rotation = mEnd;
        }

        if (GUI.Button(new Rect(0f, 400f, 100f, 100f), "Euler\nMatrix Rot"))
        {
            Matrix4x4 tM = Matrix4x4.identity;
            tM.SetTRS(Vector3.zero, Quaternion.Euler(0f, 0f, 90f), Vector3.one);//z축 회전축으로 90도 회전 행렬
            mMatRot = tM * mMatRot;

            tM = Matrix4x4.identity;
            tM.SetTRS(Vector3.zero, Quaternion.Euler(90f, 0f, 0f), Vector3.one);//x축 회전축으로 90도 회전 행렬
            mMatRot = tM * mMatRot;

            tM = Matrix4x4.identity;
            tM.SetTRS(Vector3.zero, Quaternion.Euler(0f, 90f, 0f), Vector3.one);//y축 회전축으로 90도 회전 행렬
            mMatRot = tM * mMatRot;

            mMeshFilter = GetComponentInChildren<MeshFilter>();
            mOriginVertices = mMeshFilter.mesh.vertices; //정점을 조회하여 담아둔다.
        }
    }

    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
}
