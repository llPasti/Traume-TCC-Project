using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed, jumpHeight;
    float velX, velY;
    Rigidbody2D rb;
    public Transform groundCheck;
    public bool isGrounded;
    public float groundCheckRadius;
    public LayerMask whatIsGround;

    public static PlayerController instance;

    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        if (isGrounded)
        {
            anim.SetBool("Jump", false);
        }
        else
        {
            anim.SetBool("Jump", true);
        }
        FlipCharacter();
        Attack();

    }
    private void FixedUpdate()
    {
        Movement();
        Jump();
    }
    public void Attack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetBool("Attack", true);
        }
        else
        {
            anim.SetBool("Attack", false);
        }

    }
    public void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        }
    }
    public void Movement()
    {
        velX = Input.GetAxisRaw("Horizontal");
        velY = rb.velocity.y;

        rb.velocity = new Vector2(velX * speed, velY);

        if(rb.velocity.x != 0)
        {
            anim.SetBool("Walk", true);
        }
        else
        {
            anim.SetBool("Walk", false);
        }
    }
    public void FlipCharacter()
    {
        if (rb.velocity.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
