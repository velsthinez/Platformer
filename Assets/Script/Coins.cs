using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Coins : MonoBehaviour
{
    public GameObject CollectedParticles;
    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("collected");
        
        if (CollectedParticles)
        {
            Instantiate(CollectedParticles, transform.position, quaternion.identity);
        }
        
        Destroy(this.gameObject);
    }
}
