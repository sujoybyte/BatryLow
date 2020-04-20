using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    //[SerializeField] private Transform moveMax = null, moveMin = null;
    [SerializeField] private Transform raderStart = null;
    private bool playerDetect = false;
    private float radarRadius = 9f;
    public LayerMask radarOverlapLayer;
    private float lastShotTime;
    [SerializeField] private Transform shootPoint = null;
    [SerializeField] private GameObject shooter = null;
    private GameObject shooterCopy;

    private void Start()
    {
        lastShotTime = Time.time;
    }

    private void FixedUpdate()
    {
        playerDetect = Physics2D.OverlapCircle(raderStart.position, radarRadius, radarOverlapLayer);

        if (playerDetect && Time.time > lastShotTime)
        {
            Shoot();
            lastShotTime = Time.time + 0.2f;
        }
    }

    private void Shoot()
    {
        Debug.Log("Player Shoot");
        shooterCopy = Instantiate(shooter, shootPoint.position, Quaternion.identity);
        shooterCopy.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1f * 200f, 0f));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(raderStart.position, radarRadius);
    }
}
