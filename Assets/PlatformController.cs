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
    private int jumpCount = 0; // Tæller antal hop
    private int maxJumpCount = 2; // Max antal hop


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpCount = 0; //Starter med at sætte hoppe tælleren til 0
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
                jumpCount = 0; //Nulstiller hop tælleren, når vi er på jorden
                rb.velocity = new Vector2(rb.velocity.x, jumpForce); 
            }
            else if (jumpCount < maxJumpCount) //Tæller om antal hop er mindre end max
            {
                jumpCount++; //tilføj til hop tælleren
                rb.velocity = new Vector2(rb.velocity.x, 0); // til at nulstille y-værdien
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
    }
        private void FixedUpdate()
        {
            rb.velocity = new Vector2(inputX * speed, rb.velocity.y); //Opretholder kraft i y, og sætter input på en ny vektor2

        }
        void Flip()
        {
            Vector3 tempScale = transform.localScale; //Gemmer størrelsen i tempScale
            tempScale.x *= -1f; //For at ændre retning ganges størrelsen med -1
            transform.localScale = tempScale;
            facingRight = !facingRight;
        }
    }

