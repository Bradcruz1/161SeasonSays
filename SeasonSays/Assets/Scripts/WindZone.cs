﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindZone : MonoBehaviour
{
    public int windStrength;
    public Vector3 windDirection;
    private void Start()
    {
        RandDirWind();
    }

    //Spawns Wind with a Random Direction
    private void RandDirWind()
    {
        //Flip a Coin to see if the X or Z direction should be changed
        if (Random.value >= 0.5)
        {
            if (Random.value >= 0.5)
            {
                //Set the X value of the windDirection to be positive
                windDirection = new Vector3(5f, 0f, 0f);
                this.GetComponent<Transform>().localRotation = Quaternion.Euler(0f, 0f, 0f);
            }
            else
            {
                //Set the X value of the windDirection to be negative
                windDirection = new Vector3(-5f, 0f, 0f);
                this.GetComponent<Transform>().localRotation = Quaternion.Euler(0f, 180f, 0f);
            }
        }
        else
        {
            if(Random.value >= 0.5)
            {
                //Set the Z value of the windDirection to be positive
                windDirection = new Vector3(0f, 0f, 5f);
                this.GetComponent<Transform>().localRotation = Quaternion.Euler(0f, 270f, 0f);
            }
            else
            {
                //Set the Z value of the windDirection to be negative
                windDirection = new Vector3(0f, 0f, -5f);
                this.GetComponent<Transform>().localRotation = Quaternion.Euler(0f, 90f, 0f);
            }            
        }
    }
}
