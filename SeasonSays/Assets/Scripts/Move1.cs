using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move1 : MonoBehaviour {

    private Rigidbody rb;
    public float speed;
    public float jump = 5;
    private bool onGround = true;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        move();
        Jump();

     }
    void move()
    {
        float hori = Input.GetAxis("Horizontal");
        float verti = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(hori, 0f, verti);

        rb.AddForce(movement * speed);
    }
    void Jump()
    {
        if (Input.GetKeyDown("space") && onGround)
        {
            rb.AddForce(Vector3.up * jump, ForceMode.Impulse);
            onGround = false;
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        onGround = true;
    }
}
