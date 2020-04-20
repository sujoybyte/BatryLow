using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    //[SerializeField] private Transform moveMax = null, moveMin = null;
    [SerializeField] private Transform raderStart = null;
    [SerializeField] private Vector2 radarArea = Vector2.one;
    public LayerMask radarOverlapLayer;
    private bool playerDetect = false;

    private float lastShotTime;
    [SerializeField] private Transform shootPoint = null;
    [SerializeField] private GameObject shooter = null;
    private GameObject shooterCopy;
    [SerializeField] private float shootSpeed = 200f;

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
        Debug.Log("Player Shoot");
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

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet Area"))
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
            // enemy health reduce
        }
    }
}
