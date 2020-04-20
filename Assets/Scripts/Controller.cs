using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private float jumpSpeed = 8f;
    [SerializeField] private float moveSpeed = 5f;
    private SpriteRenderer playerSprite = null;

    void Start()
    {
        playerSprite = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpSpeed;
        
        float moveDirection = Input.GetAxis("Horizontal");
        transform.Translate(new Vector2(moveDirection * Time.fixedDeltaTime * moveSpeed, 0f));

        if (moveDirection < 0) playerSprite.flipX = true;
        else if (moveDirection > 0) playerSprite.flipX = false;
    }
}
