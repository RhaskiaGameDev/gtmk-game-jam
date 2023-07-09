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
    public float shootDelay = .2f;
    private float lastShot;
    public AiRenderer er;

    public float health; 
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

        if (health < 1)
        {
            er.dead = true;
            er.delay = 1f / 6f;
            Destroy(gameObject, .5f);
        }

        velocity.x = speed * (direction ? 1 : -1);
        rb.velocity = velocity;
        
        Vector2 dir = meshRenderer.flipX ? Vector2.left : Vector2.right; 
        Ray2D ray = new Ray2D((Vector2)transform.position + dir/2, dir);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        Debug.DrawRay(ray.origin, ray.direction, Color.red, 10.0f);
        
        // If it hits something...
        if (hit.collider != null && Time.time - lastShot > shootDelay)
        {
            lastShot = Time.time;
            print(hit.collider.gameObject);
            var hitobject = hit.collider.gameObject.GetComponent<Enemy>();
            if (hitobject != null)
            {
                hitobject.health--;
            }
        }
        
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
