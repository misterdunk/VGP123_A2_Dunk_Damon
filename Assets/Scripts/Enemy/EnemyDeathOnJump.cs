using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathOnJump : MonoBehaviour
{
    public float bounceForce = 2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Apply bounce force to the player
            Rigidbody2D playerRb = collision.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                playerRb.velocity = new Vector2(playerRb.velocity.x, bounceForce);
            }

            // Destroy the enemy
            Destroy(gameObject);
        }
    }
}
