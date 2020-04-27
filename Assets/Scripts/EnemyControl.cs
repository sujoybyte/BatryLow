using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyControl : MonoBehaviour
{
    [SerializeField] private int enemyNumber = 0;
    private SpriteRenderer enemySprite;
    [SerializeField] private Transform raderStart = null;
    [SerializeField] private Vector2 radarArea = Vector2.one;
    public LayerMask radarOverlapLayer;
    private bool playerDetect = false;
    [SerializeField] private Animator animEnemy = null;

    private float lastShotTime;
    [SerializeField] private float shootInterval = 0.2f;
    [SerializeField] private float shootSpeed = 100f;
    [SerializeField] private Transform shootPoint = null;
    [SerializeField] private GameObject shooter = null;
    private GameObject shooterCopy;

    public float enemyHealth = 1f;
    [SerializeField] private float healthReduceRate = 0.05f;
    [SerializeField] private SpriteRenderer mist = null;
    private Vector2 dMistPosition;
    private bool gEnemyDead = false, rEnemyDead = false;
    public PlayerControl playerControl;
    public GameControl gameControl;
    [SerializeField] private GameObject enemyNameText = null;
    [SerializeField] private GameObject orb = null;
    public Slider rEnemySlider, gEnemySlider, bEnemySlider;

    private void Start()
    {
        lastShotTime = Time.time;
        enemySprite = GetComponent<SpriteRenderer>();
        dMistPosition = mist.transform.position;
    }

    private void Update()
    {
        if (enemyNumber == 2 && gEnemySlider.value == 0) gEnemyDead = true;
        if (enemyNumber == 2 && rEnemySlider.value == 0) rEnemyDead = true;

        if (gEnemyDead)
        {
            gEnemySlider.gameObject.SetActive(false);
            enemySprite.color = new Color(1f, 0.6f, 0.6f);
            mist.transform.position = new Vector2(dMistPosition.x + 3f, dMistPosition.y);
        }
        if (rEnemyDead)
        {
            rEnemySlider.gameObject.SetActive(false);
            gEnemySlider.gameObject.SetActive(false);
            enemySprite.color = new Color(1f, 0f, 0f);
            mist.transform.position = new Vector2(dMistPosition.x + 7f, dMistPosition.y);
        }
    }

    private void FixedUpdate()
    {
        Collider2D radarCollider = Physics2D.OverlapBox(raderStart.position, radarArea, 0f, radarOverlapLayer);
        playerDetect = radarCollider;

        if (playerDetect && Time.time > lastShotTime)
        {
            animEnemy.enabled = true;
            mist.enabled = true;
            if (enemyNumber == 2) playerControl.playerHealth -= 0.005f;

            if (radarCollider.transform.position.x > transform.position.x)
                enemySprite.flipX = true;
            else if (radarCollider.transform.position.x < transform.position.x)
                enemySprite.flipX = false;

            Shoot();
            lastShotTime = Time.time + shootInterval;
        }
        else if (!playerDetect)
        {
            animEnemy.enabled = false;
        }
    }

    private void Shoot()
    {
        float direction = -1f;
        if (enemySprite.flipX)
            direction = 1f;
        else if (!enemySprite.flipX)
            direction = -1f;

        shooterCopy = Instantiate(shooter, shootPoint.position, Quaternion.identity);
        if (enemyNumber == 0) shooterCopy.GetComponent<SpriteRenderer>().color = Color.green;
        if (enemyNumber == 1) shooterCopy.GetComponent<SpriteRenderer>().color = Color.red;
        if (enemyNumber == 2) shooterCopy.GetComponent<SpriteRenderer>().color = new Color(0f, 0.5f, 1f);
        shooterCopy.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction * shootSpeed, 0f));
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(raderStart.position, radarArea);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject bullet = collision.gameObject;
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(bullet);

            // enemy health reduce
            if (enemyNumber < 2 || (enemyNumber == 2 && rEnemySlider.value == 0 && gEnemySlider.value == 0))
                enemyHealth -= healthReduceRate;

            if (enemyHealth <= 0)
            {
                if (enemyNumber == 2) gameControl.Win();
                else if (enemyNumber < 2) Instantiate(orb, transform.position, transform.rotation);

                enemyNameText.SetActive(false);
                gameObject.SetActive(false);
            }
        }
    }
}
