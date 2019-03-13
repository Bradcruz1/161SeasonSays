using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Puddle : MonoBehaviour
{
    public GameObject player;

    //void OnCollisionStay(Collision other)
    //{
    //    var normal = other.contacts[0].normal;
    //    Debug.Log(normal.y);
    //    if (other.collider.CompareTag("Player") && normal.y <= -.99)
    //    {
    //        Debug.Log("Collided");
    //        SceneManager.LoadScene("Over");
    //        //trigger some kind of game over screen
    //    }
    //}
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Collided with Puddle");
            SceneManager.LoadScene("Over");
        }
    }
}
