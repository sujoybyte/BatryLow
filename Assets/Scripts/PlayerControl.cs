using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float jumpSpeed = 8f, moveSpeed = 5f;
    private SpriteRenderer playerSprite = null;
    private bool playerOnPlatform;
    [SerializeField] private Transform playerFeet = null;
    public LayerMask overlapingLayer;
    private float maxJump = 2;

    private float lastShotTime;
    [SerializeField] private float shootSpeed = 100f;
    [SerializeField] private GameObject shooter = null;
    [SerializeField] private Transform shootPoint = null;
    private GameObject shooterCopy;

    public float playerHealth = 1f;
    public float healthReduceRate = 0.05f;
    public GameObject canvasObject;
    [HideInInspector] public CanvasManager canvasControl;
    [SerializeField] private GameObject helpText = null;
    [SerializeField] private GameObject restart = null;

    private void Start()
    {
        playerSprite = GetComponent<SpriteRenderer>();
        canvasControl = GetComponent<CanvasManager>();
    }

    private void Update()
    {
        // player move and face to the left or right
        float moveDirection = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.A) || moveDirection < 0)
        {
            DisableHelpText();
            transform.Translate(new Vector2(moveDirection * Time.deltaTime * moveSpeed, 0f));
            playerSprite.flipX = true;
            shootPoint.localPosition = new Vector2(-1f, shootPoint.localPosition.y);
        }
        else if (Input.GetKeyDown(KeyCode.D) || moveDirection > 0)
        {
            DisableHelpText();
            transform.Translate(new Vector2(moveDirection * Time.deltaTime * moveSpeed, 0f));
            playerSprite.flipX = false;
            shootPoint.localPosition = new Vector2(1f, shootPoint.localPosition.y);
        }
    }

    private void FixedUpdate()
    {
        // 
        playerOnPlatform = Physics2D.OverlapCircle(playerFeet.position, 0.4f, overlapingLayer);
        if (playerOnPlatform) maxJump = 2;

        // player jump
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && maxJump > 0)
        {
            DisableHelpText();

            GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpSpeed;
            maxJump--;
        }

        // player shoot
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.S)) && Time.time > lastShotTime)
        {
            lastShotTime = Time.time + 0.1f;
            Shot();
        }
    }

    private void Shot()
    {
        float direction = 1f;
        if (!GetComponent<SpriteRenderer>().flipX)
            direction = 1f;
        else if (GetComponent<SpriteRenderer>().flipX)
            direction = -1f;
        
        shooterCopy = Instantiate(shooter, shootPoint.position, Quaternion.identity);
        shooterCopy.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction * shootSpeed, 0f));
    }

    private void DisableHelpText()
    {
        if (helpText.activeSelf) helpText.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject bullet = collision.gameObject;
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(bullet);

            // player health reduce
            playerHealth -= healthReduceRate;
            if (playerHealth <= 0)
            {
                restart.SetActive(true);
                StartCoroutine(Restarting());
            }
        }
        if (collision.gameObject.CompareTag("Orb"))
        {
            playerHealth += 0.2f;
            Destroy(collision.gameObject);
        }
    }

    private IEnumerator Restarting()
    {
        Time.timeScale = 0;
        yield return new WaitForSeconds(2.2f);
        SceneManager.LoadScene(0);
    }
}
