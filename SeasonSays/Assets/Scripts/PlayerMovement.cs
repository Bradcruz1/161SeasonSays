﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody m_rigidbody;
    private Collider m_collider;

    private float m_speed;
    private float jumpForce;

    [SerializeField]
    private bool m_grounded;
    // Start is called before the first frame update

    void Awake()
    {
        m_rigidbody = this.GetComponent<Rigidbody>();
        m_collider = this.GetComponent<Collider>();

        m_speed = 5f;
        jumpForce = 5f;

        m_grounded = true;
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (Input.GetButtonDown("Jump") && m_grounded)
        {
            Jump();
            //m_grounded = true;
        }
    }

    void Move()
    {
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        //print(horizontalMovement);
        float verticalMovement = Input.GetAxisRaw("Vertical");
        //print(verticalMovement);

        Vector3 currentVelocity = m_rigidbody.velocity;
        m_rigidbody.velocity = new Vector3(horizontalMovement * m_speed, currentVelocity.y, verticalMovement * m_speed);
    }

    void Jump()
    {
        //m_grounded = false;
        m_rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            Vector3 feetPosition = new Vector3(this.transform.position.x, m_collider.bounds.min.y, this.transform.position.z);
            RaycastHit hitInfo;

            bool hitSomething = Physics.Raycast(feetPosition, Vector3.down, out hitInfo, 0.1f);

            Debug.DrawRay(feetPosition, Vector3.down * 0.1f, Color.green);

            if (hitSomething && hitInfo.collider.CompareTag("Ground"))
            {
                m_grounded = true;
                Debug.LogFormat("Grounded5: {0}", m_grounded);
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            m_grounded = false;
            Debug.LogFormat("Grounded6: {0}", m_grounded);
        }
    }
}
