using System;
using UnityEngine;

public class unlockedArea : MonoBehaviour
{
    public float necessaryAmount;
    public GameObject player;
    
    private void Start()
    {
        
        gameObject.SetActive(true);
    }

    public void Update()
    {
        Invoke(nameof(Unlock), 2);
    }

    public void Unlock()
    {
        if (player.GetComponent<Collect>().collectibleCount >= necessaryAmount)
        {
            gameObject.SetActive(false);
        }
    }
}
