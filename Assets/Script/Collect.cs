using System;
using System.Collections;
using UnityEngine;

public class Collect : MonoBehaviour
{
    public bool touchedBlock = false;
    public float waitTime = 0.5f;
    public int collectibleCount = 0;

    public unlockedArea uA;

    
    private void BlockCheck()
    {
        touchedBlock = true;
        StartCoroutine(BlockWait());

    }
    
    private void Start()
    {
        collectibleCount = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectible"))
        {
            collectibleCount++;
            Debug.Log(collectibleCount);
            Destroy(other.gameObject);
            BlockCheck();
        }
    }
    
    IEnumerator BlockWait()
    {
        yield return new WaitForSeconds(waitTime);
        touchedBlock = false;
    }
}
