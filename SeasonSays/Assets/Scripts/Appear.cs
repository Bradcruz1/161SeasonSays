using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Appear : MonoBehaviour
{
    private GameObject Instructions;

    void Start()
    {
        Instructions = GameObject.Find("Instructions");
        Instructions.gameObject.SetActive(false);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Instructions.gameObject.SetActive(true);
        }
    }
}
