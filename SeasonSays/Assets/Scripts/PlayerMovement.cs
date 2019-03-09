using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    [SerializeField]
    public float playerHealth;
    
    public Text healthbar;

    void Awake()
    {
        m_rigidbody = this.GetComponent<Rigidbody>();
        m_collider = this.GetComponent<Collider>();

        m_speed = 20f;
        m_jumpForce = 10f;
        m_elapsedTime = 0f;

        m_grounded = true;
        m_inWind = false;

        playerHealth = 30;
    }

    private void Update()
    {
        HealthText();
        if (playerHealth <= 0)
        {
            SceneManager.LoadScene("Over");
        }
    }

    void HealthText()
    {
        if(healthbar != null)
            healthbar.text = "Health: " + playerHealth.ToString();
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
    }

    //private void OnCollisionEnter(Collision other)
    //{
    //    m_grounded = true;

    //    if (other.collider.CompareTag("Ice"))
    //    {
    //        isIcy = true;
    //    }
    //}

    //Fix Sound when you run straight through it
    void OnTriggerStay(Collider other)
    {
        if(m_inWind && other.CompareTag("Wind"))
        {
            WindZone otherWind = other.transform.GetComponent<WindZone>();
            m_rigidbody.AddForce(otherWind.windDirection * otherWind.windStrength, ForceMode.Force);
            if (!other.GetComponent<AudioSource>().isPlaying)
            {
                if (Time.time - m_elapsedTime > 0.15f)
                    other.GetComponent<AudioSource>().Play();
            }
        }
        if (other.CompareTag("Fire"))
        {
            if (!other.GetComponent<AudioSource>().isPlaying)
                other.GetComponent<AudioSource>().Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wind"))
        {
            m_inWind = true;
            m_elapsedTime = Time.time;
        }
        if (other.CompareTag("Ice"))
        {
            isIcy = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Wind"))
        {
            other.GetComponent<AudioSource>().Stop();
            m_inWind = false;
        }
        if (other.CompareTag("Fire"))
        {
            //other.GetComponent<AudioSource>().Stop();
        }
        if (other.CompareTag("Ice"))
        {
            isIcy = false;
        }
    }

    //void OnCollisionExit(Collision other)
    //{
    //    if(other.collider.CompareTag("Ice"))
    //    {
    //        isIcy = false;
    //    }
    //}
}
