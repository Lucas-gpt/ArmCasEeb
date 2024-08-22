using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public float speed = 5f;
    private float inputX;
    Rigidbody2D rb;
    [SerializeField] private bool facingRight = true;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float checkRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private bool isGrounded;
    private int jumpCount = 0; // T�ller antal hop
    private int maxJumpCount = 2; // Max antal hop


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpCount = 0; //Starter med at s�tte hoppe t�lleren til 0
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
        inputX = Input.GetAxisRaw("Horizontal"); // Altid brug Raw af GetAxis
        if (inputX < 0 && facingRight)
        {
            Flip();
        }
        if (inputX > 0 && !facingRight)
        {
            Flip();
        }
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                jumpCount = 0; //Nulstiller hop t�lleren, n�r vi er p� jorden
                rb.velocity = new Vector2(rb.velocity.x, jumpForce); 
            }
            else if (jumpCount < maxJumpCount) //T�ller om antal hop er mindre end max
            {
                jumpCount++; //tilf�j til hop t�lleren
                rb.velocity = new Vector2(rb.velocity.x, 0); // til at nulstille y-v�rdien
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
    }
        private void FixedUpdate()
        {
            rb.velocity = new Vector2(inputX * speed, rb.velocity.y); //Opretholder kraft i y, og s�tter input p� en ny vektor2

        }
        void Flip()
        {
            Vector3 tempScale = transform.localScale; //Gemmer st�rrelsen i tempScale
            tempScale.x *= -1f; //For at �ndre retning ganges st�rrelsen med -1
            transform.localScale = tempScale;
            facingRight = !facingRight;
        }
    }

