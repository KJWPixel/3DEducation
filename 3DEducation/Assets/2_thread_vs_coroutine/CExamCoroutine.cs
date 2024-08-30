using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CExamCoroutine : MonoBehaviour
{
    IEnumerator mDoDispatch = null;

    void BeginCoroutine()
    {
        mDoDispatch = DoDispatch();

        StartCoroutine(mDoDispatch);
    }

    //코루틴 함수
    //<-- 리턴타입을 IEnumerator로 만든다.
    //<-- yield return~;로 리턴한다.(실행의 흐름을 양보하는 것이다)
    IEnumerator DoDispatch()
    {
        Debug.Log("DoDispatch");

        for(;;)
        {
            Debug.Log("DoDispatch 00");
            yield return null;//한 프레임정도 양보
            Debug.Log("DoDispatch 01");
        }
        Debug.Log("//DoDispatch");
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(0f, 0f, 300f, 100f), "Abort Coroutine"))
        {
            StopCoroutine(mDoDispatch);
        }
    }


    void Start()
    {
        Invoke("BeginCoroutine", 5f);
    }

 

    void Update()
    {
        Debug.Log("MainThread.  Update");
    }
}
