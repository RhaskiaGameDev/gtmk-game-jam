using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : MonoBehaviour
{
    public Rigidbody2D rb;
    public float jumpForce;
    public float speed;
    public bool direction;
    private SpriteRenderer meshRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        meshRenderer = GetComponent<SpriteRenderer>(); 
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 velocity = rb.velocity;

        velocity.x = speed * (direction ? 1 : 0);
        rb.velocity = velocity;
    }
}
