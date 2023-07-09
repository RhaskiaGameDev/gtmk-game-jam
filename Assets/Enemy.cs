using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rb;
    public float jumpForce;
    public float speed;
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
        bool current = SwitchManager.Instance.currentEnemy.Contains(this);
        
        meshRenderer.color = current ? new Color(2f, 2f, 2f, 1f) : new Color(1f, 1f, 1f,  .6f);
        
        if (!current) return;
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && jumpsLeft > 0)
        {
            rb.AddForce(Vector2.up * jumpForce);
            jumpsLeft--;
        }

        Vector2 velocity = rb.velocity;
        velocity.x = speed * Input.GetAxis("Horizontal");
        rb.velocity = velocity;
    }

    void OnMouseDown()
    {
        if (SwitchManager.Instance.currentEnemy.Contains(this))
        {
            SwitchManager.Instance.currentEnemy.Remove(this);
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                SwitchManager.Instance.currentEnemy.Add(this);
            }
            else
            {
                SwitchManager.Instance.currentEnemy.RemoveAll(x => true);
                SwitchManager.Instance.currentEnemy.Add(this);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            jumpsLeft = jumps;
        }
    }
}
