using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private int maxLives = 5;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;

    //Player gameplay variables
    private int _lives;
    public int lives
    {
        get => _lives;
        set
        {
            if (value <= 0) GameOver();
            if (value < _lives) Respawn();
            if (value > maxLives) value = maxLives;
            _lives = value;
        }
    }

    public int GetLives()
    {
        return _lives;
    }

    public void SetLives(int value)
    {
        if (value <= 0)
        {
            GameOver();
            return;
        }
        if (value < _lives)
        {
            Respawn();
            return;
        }
        _lives = value;
    }

    //Ground check
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool isAttacking;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask isGroundLayer;
    [SerializeField] private float groundCheckRadius;

    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip coinClip;


    private Rigidbody2D RB;
    private SpriteRenderer SR;
    private Animator anim;
    private AudioSource audioSource;

    private Coroutine jumpForceChange = null;
    public void JumpForceChange()
    {
        if (jumpForceChange != null)
        {
            StopCoroutine(jumpForceChange);
            jumpForceChange = null;
            jumpForce /= 2;
            jumpForceChange = StartCoroutine(JumpForceChangeCoroutine());
            return;
        }

        jumpForceChange = StartCoroutine(JumpForceChangeCoroutine());

    }

    IEnumerator JumpForceChangeCoroutine()
    {
        jumpForce *= 2;
        yield return new WaitForSeconds(5.0f);
        jumpForce /= 2;
    }

    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        SR = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        if (speed <= 0)
        {
            speed = 3f;
            Debug.Log("Speed Set To Default Value");
        }

        if (jumpForce <= 0)
        {
            jumpForce = 4f;
            Debug.Log("JumpForce Set To Default Value");
        }

        if (groundCheckRadius <= 0)
        {
            groundCheckRadius = 0.02f;
            Debug.Log("GCR Set To Default Value");
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
            Debug.Log("GC Transform Created via Code - Did you forget to assign it?");
        }
    }

    // Update is called once per frame
    void Update()
    {
        SetLives(GetLives() + 1);
        lives++;

        float xInput = Input.GetAxis("Horizontal");

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);
        if (isGrounded)
        {
            RB.gravityScale = 1;
        }

        Vector2 moveDirection = new Vector2(xInput * speed, RB.velocity.y);

        RB.velocity = moveDirection;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            RB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            audioSource.PlayOneShot(jumpClip);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            anim.Play("attack");
        }

        if (Input.GetButtonDown("Fire2"))
        {
            anim.Play("jumpattack");
        }

        //Flipping sprite on x-axis
        if (xInput != 0)
            SR.flipX = (xInput < 0);

        anim.SetFloat("speed", Mathf.Abs(xInput));
        anim.SetBool("isGrounded", isGrounded);
        //anim.SetBool("isAttacking", isAttacking);
    }

    private void GameOver()
    {
        Debug.Log("GameOver goes here");
    }

    private void Respawn()
    {
        Debug.Log("Respawn goes here");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       if(other.gameObject.CompareTag("Coin"))
        {
            audioSource.PlayOneShot(coinClip);
            Destroy(other.gameObject);
        }
    }
}
