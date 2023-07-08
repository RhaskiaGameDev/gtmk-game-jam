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
    private SpriteRenderer meshRenderer;
    public float jumps;
    private float jumpsLeft;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        meshRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        meshRenderer.color = SwitchManager.Instance.currentEnemy == this ? new Color(2f, 2f, 2f, 1f) : new Color(1f, 1f, 1f,  .6f);
        
        if (SwitchManager.Instance.currentEnemy != this) return;
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && jumpsLeft > 0)
        {
            rb.AddForce(Vector2.up * jumpForce);
            jumpsLeft--;
        }

        Vector2 velocity = rb.velocity;

        velocity.x += accel * Input.GetAxis("Horizontal") * Time.deltaTime;
        velocity.x = Mathf.Clamp(velocity.x, -maxSpeed, maxSpeed);

        rb.velocity = velocity;
    }

    void OnMouseDown()
    {
        SwitchManager.Instance.currentEnemy = this;
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            jumpsLeft = jumps;
        }
    }
}
