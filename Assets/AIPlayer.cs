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

        velocity.x = speed * (direction ? 1 : -1);
        rb.velocity = velocity;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Switch"))
        {
            direction = !direction;
            
            var next = other.gameObject.GetComponent<PlayerTrigger>().nextTrigger;
            
            if (next != null) next.SetActive(true);
            print("sqwitc");
        }
        
        if (other.gameObject.CompareTag("Jump"))
        {
            rb.AddForce(Vector2.up * jumpForce);

            var next = other.gameObject.GetComponent<PlayerTrigger>().nextTrigger;
            
            if (next != null) next.SetActive(true);
            print("what");
        }
    }
}
