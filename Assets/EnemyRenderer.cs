using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRenderer : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    public bool isShooting;
    public bool dead;
    public Sprite[] idle;
    public Sprite[] run;
    public Sprite[] shoot;
    public Sprite[] death;
    public float delay;
    public int frame;
    
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        Invoke("UpdateAnimation", delay);
    }

    // Update is called once per frame
    void UpdateAnimation()
    {
        frame++;
        if (dead)
        {
            sr.sprite = death[frame % death.Length];
        }
        else if (isShooting)
        {
            sr.sprite = shoot[frame % shoot.Length];
        }
        else if (Mathf.Abs(rb.velocity.x) > 0.05f)
        {
            sr.sprite = run[frame % run.Length];
        }
        else
        {
            sr.sprite = idle[frame % idle.Length];
        }

        if (rb.velocity.x > 0.05f) sr.flipX = false;
        if (rb.velocity.x < -0.05f) sr.flipX = true;

        Invoke("UpdateAnimation", delay);
    }
}
