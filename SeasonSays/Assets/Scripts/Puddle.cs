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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionStay(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
            Debug.Log("Collided");
            SceneManager.LoadScene("Over");
            //trigger some kind of game over screen
        }
    }
}
