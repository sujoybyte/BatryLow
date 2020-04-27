using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyControl : MonoBehaviour
{
    public int enemyNumber;
    [SerializeField] private Transform raderStart = null;
    [SerializeField] private Vector2 radarArea = Vector2.one;
    public LayerMask radarOverlapLayer;
    private bool playerDetect = false;

    private float lastShotTime;
    [SerializeField] private float shootSpeed = 100f;
    [SerializeField] private Transform shootPoint = null;
    [SerializeField] private GameObject shooter = null;
    private GameObject shooterCopy;

    public float enemyHealth = 1f;
    public float healthReduceRate = 0.05f;
    public GameControl gameControl;
    [SerializeField] private GameObject orb = null;
    public Slider rEnemySlider, gEnemySlider, bEnemySlider;

    private void Start()
    {
        lastShotTime = Time.time;
    }

    private void FixedUpdate()
    {
        Collider2D radarCollider = Physics2D.OverlapBox(raderStart.position, radarArea, 0f, radarOverlapLayer);
        playerDetect = radarCollider;

        if (playerDetect && Time.time > lastShotTime)
        {
            if (radarCollider.transform.position.x > transform.position.x) 
                GetComponent<SpriteRenderer>().flipX = true;
            else if (radarCollider.transform.position.x < transform.position.x) 
                GetComponent<SpriteRenderer>().flipX = false;

            Shoot();
            lastShotTime = Time.time + 0.2f;
        }
    }

    private void Shoot()
    {
        float direction = -1f;
        if (GetComponent<SpriteRenderer>().flipX)
            direction = 1f;
        else if (!GetComponent<SpriteRenderer>().flipX)
            direction = -1f;

        shooterCopy = Instantiate(shooter, shootPoint.position, Quaternion.identity);
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

                Destroy(gameObject);
            }
        }
    }
}
