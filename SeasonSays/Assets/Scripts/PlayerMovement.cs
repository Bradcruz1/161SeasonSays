using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody m_rigidbody;

    private float m_speed;
    // Start is called before the first frame update

    void Awake()
    {
        m_rigidbody = this.GetComponent<Rigidbody>();
        m_speed = 5f;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        //print(horizontalMovement);
        float verticalMovement = Input.GetAxisRaw("Vertical");
        //print(verticalMovement);

        Vector3 currentVelocity = m_rigidbody.velocity;
        m_rigidbody.velocity = new Vector3(horizontalMovement * m_speed, verticalMovement * m_speed);
    }
}
