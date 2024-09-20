using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CSceneEfx : MonoBehaviour
{
    [SerializeField] GameObject[] mPPs = null;

    private void OnGUI()
    {
        if(GUI.Button(new Rect(0f, 0f, 100f, 50f), "Test Grain On"))
        {
            foreach(var t in mPPs)
            {
                t.SetActive(false);
            }

            mPPs[0].SetActive(true);
            StartCoroutine(UpdatePP_0_0());
        }

        if (GUI.Button(new Rect(100f, 0f, 100f, 50f), "Test Grain On"))
        {
            foreach (var t in mPPs)
            {
                t.SetActive(false);
            }

            mPPs[0].SetActive(true);
            StartCoroutine(UpdatePP_0_1());
        }




        if (GUI.Button(new Rect(0f, 100f, 100f, 50f), "Test Grain On"))
        {
            foreach (var t in mPPs)
            {
                t.SetActive(false);
            }

            mPPs[1].SetActive(true);
            StartCoroutine(UpdatePP_1_0());
        }

        if (GUI.Button(new Rect(100f, 100f, 100f, 50f), "Test Grain On"))
        {
            foreach (var t in mPPs)
            {
                t.SetActive(false);
            }

            mPPs[1].SetActive(true);
            StartCoroutine(UpdatePP_1_1());
        }
    }

    IEnumerator UpdatePP_0_0()
    {
        for(; ; )
        {
            mPPs[0].GetComponent<PostProcessVolume>().weight += 0.1f;

            if (mPPs[0].GetComponent<PostProcessVolume>().weight >= 1.0f)
            {
                StopAllCoroutines();
            }

            yield return new WaitForSeconds(0.1f);
        }
        
    }

    IEnumerator UpdatePP_0_1()
    {
        for (; ; )
        {
            mPPs[0].GetComponent<PostProcessVolume>().weight -= 0.1f;

            if (mPPs[0].GetComponent<PostProcessVolume>().weight <= 1.0f)
            {
                StopAllCoroutines();
            }

            yield return new WaitForSeconds(0.1f);
        }

    }
    IEnumerator UpdatePP_1_0()
    {
        for (; ; )
        {
            mPPs[1].GetComponent<PostProcessVolume>().weight += 0.1f;

            if (mPPs[1].GetComponent<PostProcessVolume>().weight >= 1.0f)
            {
                StopAllCoroutines();
            }

            yield return new WaitForSeconds(0.1f);
        }

    }

    IEnumerator UpdatePP_1_1()
    {
        for (; ; )
        {
            mPPs[1].GetComponent<PostProcessVolume>().weight -= 0.1f;

            if (mPPs[1].GetComponent<PostProcessVolume>().weight <= 1.0f)
            {
                StopAllCoroutines();
            }

            yield return new WaitForSeconds(0.1f);
        }

    }

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
