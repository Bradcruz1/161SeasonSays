using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        playSound();
    }

    void playSound()
    {
        this.GetComponent<AudioSource>().Play();
    }
}
