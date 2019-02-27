using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody m_rigidbody;
    private Collider m_collider;

    private float m_speed;
    private float m_jumpForce;

    [SerializeField]
    private bool m_grounded;

    void Awake()
    {
        m_rigidbody = this.GetComponent<Rigidbody>();
        m_collider = this.GetComponent<Collider>();

        m_speed = 10f;
        m_jumpForce = 5f;

        m_grounded = true;
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
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");

        Vector3 currentVelocity = m_rigidbody.velocity;
        m_rigidbody.velocity = new Vector3(horizontalMovement * m_speed, currentVelocity.y, verticalMovement * m_speed);
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

    private void OnCollisionEnter(Collision collision)
    {
        m_grounded = true;
    }

    void OnCollisionStay(Collision collision)
    {
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
    }

    //void OnCollisionExit(Collision collision)
    //{
    //    if (collision.collider.CompareTag("Ground"))
    //    {
    //        m_grounded = false;
    //    }
    //}

    void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Wind"))
        {
            WindZone otherWind = other.transform.GetComponent<WindZone>();
            m_rigidbody.AddForce(otherWind.windDirection * otherWind.windStrength, ForceMode.Force);
        }
    }
}
