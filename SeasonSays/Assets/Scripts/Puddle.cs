using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Puddle : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        //playSound();
    }

    //void playSound()
    //{
    //    this.GetComponent<AudioSource>().Play();
    //}

    void OnCollisionStay(Collision other)
    {
        var normal = other.contacts[0].normal;
        Debug.Log(normal.y);
        if (other.collider.CompareTag("Player") && normal.y <= -.99)
        {
            Debug.Log("Collided");
            SceneManager.LoadScene("Over");
            //trigger some kind of game over screen
        }
    }
}
