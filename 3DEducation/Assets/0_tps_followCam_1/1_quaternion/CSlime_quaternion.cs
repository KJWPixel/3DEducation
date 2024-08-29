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
        //사원수의 보간(Interpolation: 근사치를 구한다)
        if (GUI.Button(new Rect(200f, 0f, 100f, 100f), "Quaternion\n보간"))
        {
            //z,x,y순으로 90, 90, 90 사원수의 곱셈으로 회전 표현
            mEnd = Quaternion.Euler(0f, 0f, 90f) * Quaternion.Euler(90f, 0f, 0f) * Quaternion.Euler(00f, 90f, 0f);
            //z축 90도로 오일러각에 의한 회전을 하고 사원수로 변환해서 리턴한다.

            //this.transform.rotation = mEnd;

            //선형보간의 가중치는 0부터 시작
            mWeight = 0f;

            //상태변경
            mState = STATE.WITH_INTERPLATION;
        }



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

            //변환된 위치 값들을 담을 새로운 원소들의 배열을 만들자
            mMeshFilter = GetComponentInChildren<MeshFilter>();
            mOriginVertices = mMeshFilter.mesh.vertices; //정점을 조회하여 담아둔다.(벡터타입의 원소들은 값타입이므로 북사되어 담기도록 만들어져 있다.)

            int ti = 0;   
            while(ti<mOriginVertices.Length)
            {
                mNewVertices[ti] = mMatRot.MultiplyPoint3x4(mOriginVertices[ti]);
                //MultiplyPoint3x4 = (4 x 4) + (4 x 1) = 4 x 1이지만 해당 함수를 사용하여 3 x 1로 변환

                ++ti;
            }

            //메쉬필터 컴포넌트(회전 변환 행렬이 적용된) 새로운 메쉬를 적용한다.
            mMeshFilter.mesh.vertices = mNewVertices;   
        }
    }

    void Start()
    {

    }

    enum STATE
    {
        WITH_NONE = 0,
        WITH_INTERPLATION  //보간 상태
    }

    STATE mState = STATE.WITH_NONE;

    float mWeight = 0.0f;


    void Update()
    {
        if(STATE.WITH_INTERPLATION == mState)
        {
            //사원수의 선형보간
            //this.transform.rotation = Quaternion.Lerp(mStart, mEnd, mWeight);
            this.transform.rotation = Quaternion.Slerp(mStart, mEnd, mWeight);//Lerp보다 좀더 변화가 다이나믹한 즉 부드러운 움직임을 만든다.
            //Slerp Sphere Linear Interpolation 구면 성형 보간
            /*
            선형 보간은 일차함수(직선의 방정식)를 이용하여 근사치를 구한다.
            구면 성형 보간은 두 점 사이의 (원의 틀에의 일부인)) '호'를 사용하여 근사치를 구한다.

            원의 호는 곡선이므로
            기울기가 변한다.
            그러므로 보다 역동적인(일반적인 선형보간보다 좀더 변화가 다이나믹함)
            표현이 가능하다.
            */ 
             
            

            mWeight += Time.deltaTime;
        }
    }
}
