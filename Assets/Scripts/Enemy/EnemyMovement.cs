using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveDistance = 0.6f;
    public float moveSpeed = 0.8f;

    private Vector3 startPosition;
    private bool movingRight = true;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        startPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (movingRight)
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            if (transform.position.x >= startPosition.x + moveDistance)
            {
                movingRight = false;
                Flip();
            }
        }
        else
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            if (transform.position.x <= startPosition.x - moveDistance)
            {
                movingRight = true;
                Flip();
            }
        }
    }

    void Flip()
    {
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }
}
