using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 7f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private int jumpCount = 1; 

    [Header("Respawn Settings")]
    public Vector2 respawnPoint; // The checkpoint where the player respawns
    public float fallThreshold = -10f; // Y position where player respawns

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        respawnPoint = transform.position; // Set initial respawn point
    }

    void Update()
    {
        Move();
        HandleJump();
        CheckRespawn();
    }

    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount--;
        }

        if (IsGrounded())
        {
            jumpCount = 1; // Reset jump count when landing
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, groundLayer);
        return hit.collider != null;
    }

    private void CheckRespawn()
    {
        if (transform.position.y < fallThreshold)
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        transform.position = respawnPoint;
        rb.velocity = Vector2.zero; // Reset velocity to prevent momentum carryover
    }
}
