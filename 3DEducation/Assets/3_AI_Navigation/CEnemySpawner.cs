using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemySpawner : MonoBehaviour
{
    [SerializeField] CEnemy PFEnemy = null;

    [SerializeField] float mTimeInterval = 5f;

    IEnumerator OnSpawnEnemy()
    {
        for(; ; )
        {
            yield return new WaitForSeconds(mTimeInterval);

            Debug.Log("<color='blue'>OnSpawnEnemy</color>");

            Vector3 tPosition = this.transform.position;

            Instantiate( PFEnemy, tPosition, Quaternion.identity);
        }
    }

    void Start()
    {
        StartCoroutine(OnSpawnEnemy());
    }

    
    void Update()
    {
        
    }
}
