using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private int speed;
    [SerializeField] private int jumpForce;
    [SerializeField] private bool isGrounded;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask isGroundLayer;
    [SerializeField] private float groundCheckRadius;
    

    private Rigidbody2D rb;
    private SpriteRenderer SR;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SR = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        if (speed <= 0)
        {
            speed = 2;
        }
        if (jumpForce <= 0)
        {
            jumpForce = 3;
        }

        if (groundCheckRadius <= 0)
        {
            groundCheckRadius = 0.02f;
        }

        if (groundCheck == null)
        {
            GameObject obj = GameObject.FindGameObjectWithTag("GroundCheck");
            if (obj != null)
            {
                groundCheck = obj.transform;
                return;
            }
            GameObject newObj = new GameObject();
            newObj.transform.SetParent(transform);
            newObj.transform.localPosition = Vector3.zero;
            newObj.name = "GroundCheck";
            newObj.tag = newObj.name;
            groundCheck = newObj.transform;

        }

    }

    // Update is called once per frame
    void Update()
    {

        float xInput = Input.GetAxis("Horizontal");

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);

        Vector2 moveDirection = new Vector2(xInput * speed, rb.velocity.y);

        rb.velocity = moveDirection;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        //Flipping sprite on x-axis
        if (xInput != 0) SR.flipX = (xInput < 0);

        anim.SetFloat("speed", Mathf.Abs(xInput));
        anim.SetBool("isGrounded", isGrounded);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
        }
    }
}
