using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


/*
    ���� ������ ��Ƽ ������ ���α׷� �����̴�.

    ���μ��� 1���� ������ 1���� �ݵ�� �����Ѵ�. �̸� �� ������MainThread��� �Ѵ�.
    
    �� �����尡 �����ϰ� �ִ� ���߿� ������ ����� ���۽�Ű�� �����带 
    �� ������SubThread��� �Ѵ�.
    
*/
public class CExamThread : MonoBehaviour
{
    //sub thread
    Thread mThread = null;

    //������ �ݺ������ ���� ����
    bool mThreadLoop = false;
    
    void BeginThread()
    {
        mThreadLoop = true;

        mThread = new Thread(new ThreadStart(DoDispatch));
        mThread.Name = "mmm";

        //������ ����
        mThread.Start();
    }

    //������ �Լ�, ������ ���������� ����� �Լ�
    void DoDispatch()
    {
        Debug.Log("DoDispatch ThreadFunction START");
        //������ �����帧�� �����ϱ� ���� �ݺ�������� ��ġ�ߴ�.
        while(mThreadLoop)
        {
            Debug.Log($"DoDispatch ThreadFunction Running. name: {mThread.Name}, id: {mThread.ManagedThreadId.ToString()}");

            //�����带 ��� ��� ���·� ���´�(�� �������� �����帧�� ��� ���� 5/1000��)
            Thread.Sleep(5);
        }

        Debug.Log("DoDispatch ThreadFunction END");
    }

    private void OnGUI()
    {
        if(GUI.Button(new Rect(0f, 0f, 300f, 100f), "Abort Thread!!!"))
        {
            //������ ��������
            //.NET Framework���� ������ ���� ���� ����̴�.(���Ḧ �ݵ�� �������� ����, ���߿����θ� ����)
            mThread.Abort();
        }
    }


    void Start()
    {
        //�ν����� ����
        Invoke("BeginThread", 5f);
    }

    
    void Update()
    {
        Debug.Log("MainThread.  Update");
    }
}
