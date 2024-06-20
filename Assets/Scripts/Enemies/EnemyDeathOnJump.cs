using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyDeathOnJump : MonoBehaviour
{
    public float bounceForce = 2f;
    protected Animator anim;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        anim = GetComponent<Animator>();

        if (collision.CompareTag("Player"))
        {
            // Apply bounce force to the player
            Rigidbody2D playerRb = collision.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                playerRb.velocity = new Vector2(playerRb.velocity.x, bounceForce);
            }

            if (collision.CompareTag("Goomba"))
            {
                anim.Play("gdeath");
            }
            else if (collision.CompareTag("GKoop"))
            {
                anim.Play("gkdeath");
            }
            // Destroy the enemy
            Destroy(gameObject);
        }
    }
}
