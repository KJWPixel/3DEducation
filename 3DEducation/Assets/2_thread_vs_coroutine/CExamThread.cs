using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


/*
    아주 간단한 멀티 스레드 프로그램 예시이다.

    프로세스 1개에 스레드 1개는 반드시 동작한다. 이를 주 스레드MainThread라고 한다.
    
    주 스레드가 동작하고 있는 와중에 별도로 만들어 동작시키는 스레드를 
    부 스레드SubThread라고 한다.
    
*/
public class CExamThread : MonoBehaviour
{
    //sub thread
    Thread mThread = null;

    //스레드 반복제어구조 동작 여부
    bool mThreadLoop = false;
    
    void BeginThread()
    {
        mThreadLoop = true;

        mThread = new Thread(new ThreadStart(DoDispatch));
        mThread.Name = "mmm";

        //스레드 시작
        mThread.Start();
    }

    //스레드 함수, 별도의 실행프름을 만드는 함수
    void DoDispatch()
    {
        Debug.Log("DoDispatch ThreadFunction START");
        //별도의 실행흐름을 유지하기 위해 반복제어구조를 배치했다.
        while(mThreadLoop)
        {
            Debug.Log($"DoDispatch ThreadFunction Running. name: {mThread.Name}, id: {mThread.ManagedThreadId.ToString()}");

            //스레드를 잠시 대기 상태로 놓는다(이 스레드의 실행흐름을 잠깐 쉰다 5/1000초)
            Thread.Sleep(5);
        }

        Debug.Log("DoDispatch ThreadFunction END");
    }

    private void OnGUI()
    {
        if(GUI.Button(new Rect(0f, 0f, 300f, 100f), "Abort Thread!!!"))
        {
            //스레드 강제종료
            //.NET Framework에서 스레드 강제 종료 기능이다.(종료를 반드시 보장하지 않음, 개발용으로만 쓴다)
            mThread.Abort();
        }
    }


    void Start()
    {
        //부스레드 시작
        Invoke("BeginThread", 5f);
    }

    
    void Update()
    {
        Debug.Log("MainThread.  Update");
    }
}
