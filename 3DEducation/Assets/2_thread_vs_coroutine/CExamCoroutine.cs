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

    //�ڷ�ƾ �Լ�
    //<-- ����Ÿ���� IEnumerator�� �����.
    //<-- yield return~;�� �����Ѵ�.(������ �帧�� �纸�ϴ� ���̴�)
    IEnumerator DoDispatch()
    {
        Debug.Log("DoDispatch");

        for(;;)
        {
            Debug.Log("DoDispatch 00");
            yield return null;//�� ���������� �纸
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
