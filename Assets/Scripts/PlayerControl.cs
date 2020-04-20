using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float jumpSpeed = 8f, moveSpeed = 5f;
    private SpriteRenderer playerSprite = null;
    private bool playerOnGround;
    [SerializeField] private Transform playerFeet = null;
    public LayerMask overlapingLayer;
    private float maxJump = 2;


    void Start()
    {
        playerSprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // player move and face to the left or right
        float moveDirection = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.A) || moveDirection < 0)
        {
            transform.Translate(new Vector2(moveDirection * Time.deltaTime * moveSpeed, 0f));
            playerSprite.flipX = true;
        }
        else if (Input.GetKeyDown(KeyCode.D) || moveDirection > 0)
        {
            transform.Translate(new Vector2(moveDirection * Time.deltaTime * moveSpeed, 0f));
            playerSprite.flipX = false;
        }
    }

    private void FixedUpdate()
    {
        playerOnGround = Physics2D.OverlapCircle(playerFeet.position, 0.4f, overlapingLayer);
        if (playerOnGround) maxJump = 2;

        // player jump
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && maxJump > 0)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpSpeed;
            maxJump--;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
        }
    }
}
