﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody m_rigidbody;
    private Collider m_collider;

    private float m_speed;
    private float m_jumpForce;
    private float m_elapsedTime;

    [SerializeField]
    private bool m_grounded;
    [SerializeField]
    private bool m_inWind;

    private bool isIcy;

    void Awake()
    {
        m_rigidbody = this.GetComponent<Rigidbody>();
        m_collider = this.GetComponent<Collider>();

        m_speed = 20f;
        m_jumpForce = 5f;
        m_elapsedTime = 0f;

        m_grounded = true;
        m_inWind = false;
    }

    void FixedUpdate()
    {
        Move();
        if (Input.GetButtonDown("Jump") && m_grounded)
        {
            Jump();
        }
    }

    void Move()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        Vector3 currentVelocity = m_rigidbody.velocity;

        if (!isIcy) {
            m_rigidbody.velocity = new Vector3(horizontalMovement * m_speed, currentVelocity.y, verticalMovement * m_speed);
        }

        if (isIcy) {
            m_rigidbody.velocity = new Vector3(horizontalMovement * m_speed * 5, currentVelocity.y, verticalMovement * m_speed * 5);

        }   
    }

    void Jump()
    {
        if (Input.GetKeyDown("space") && m_grounded)
        {

            m_rigidbody.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
            m_grounded = false;
        }
        //m_grounded = false;
        //m_rigidbody.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision other)
    {
        m_grounded = true;

        if (other.collider.CompareTag("Ice"))
        {
            isIcy = true;
        }
    }

    //void OnCollisionStay(Collision collision)
    //{
        //if (collision.collider.CompareTag("Ground"))
        //{
        //    Vector3 feetPosition = new Vector3(this.transform.position.x, m_collider.bounds.min.y, this.transform.position.z);
        //    RaycastHit hitInfo;

        //    bool hitSomething = Physics.Raycast(feetPosition, Vector3.down, out hitInfo, 0.1f);

        //    Debug.DrawRay(feetPosition, Vector3.down * 0.1f, Color.green);

        //    if (hitSomething && hitInfo.collider.CompareTag("Ground"))
        //    {
        //        m_grounded = true;
        //    }
        //}
    //}

    //void OnCollisionExit(Collision collision)
    //{
    //    if (collision.collider.CompareTag("Ground"))
    //    {
    //        m_grounded = false;
    //    }
    //}


    //Fix Sound when you run straight through it
    void OnTriggerStay(Collider other)
    {
        if(m_inWind && other.CompareTag("Wind"))
        {
            WindZone otherWind = other.transform.GetComponent<WindZone>();
            m_rigidbody.AddForce(otherWind.windDirection * otherWind.windStrength, ForceMode.Force);
            if (!other.GetComponent<AudioSource>().isPlaying)//&& (Time.time - m_elapsedTime >= 2f))
            {
                if (Time.time - m_elapsedTime > 0.15f)
                    other.GetComponent<AudioSource>().Play();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wind"))
        {
            m_inWind = true;
            m_elapsedTime = Time.time;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Wind"))
        {
            other.GetComponent<AudioSource>().Stop();
            m_inWind = false;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if(other.collider.CompareTag("Ice"))
        {
            isIcy = false;
        }
    }
}
