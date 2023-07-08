using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rb;
    public float jumpForce;
    public float accel;
    public float maxSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SwitchManager.Instance.currentEnemy != this) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * jumpForce);
        }

        Vector2 velocity = rb.velocity;

        velocity.x += accel * Input.GetAxis("Horizontal") * Time.deltaTime;
        velocity.x = Mathf.Clamp(velocity.x, -maxSpeed, maxSpeed);

        rb.velocity = velocity;
    }

    void OnMouseOver()
    {
        SwitchManager.Instance.currentEnemy = this;
    }
}
