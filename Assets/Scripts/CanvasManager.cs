using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [Header("Player")]
    public GameObject playerObject;
    [HideInInspector] public PlayerControl player;
    public Slider playerHealth;
    public GameObject playerFill;

    [Space(1)]

    [Header("Enemy")]
    public GameObject rEnemyObject;
    public GameObject gEnemyObject;
    public GameObject bEnemyObject;
    [HideInInspector] public EnemyControl rEnemy, gEnemy, bEnemy;
    public Slider rEnemyHealth, gEnemyHealth, bEnemyHealth;
    public GameObject rEnemyFill, gEnemyFill, bEnemyFill;

    [Space(1)]

    [Header("Game")]
    public bool rHealthReduce;
    public bool gHealthReduce;
    public bool bHealthReduce;

    void Start()
    {
        player = playerObject.GetComponent<PlayerControl>();
        rEnemy = rEnemyObject.GetComponent<EnemyControl>();
        gEnemy = gEnemyObject.GetComponent<EnemyControl>();
        bEnemy = bEnemyObject.GetComponent<EnemyControl>();
    }

    // Update is called once per frame
    void Update()
    {
        playerHealth.value = player.playerHealth;
        if (playerHealth.value <= 0)
        {
            playerFill.SetActive(false);
            // Loser
        }
        Enemy(rEnemyHealth, rEnemy, rEnemyFill);
        Enemy(gEnemyHealth, gEnemy, gEnemyFill);
        Enemy(bEnemyHealth, bEnemy, bEnemyFill);
    }

    private void Enemy(Slider enemyHealth, EnemyControl enemy, GameObject enemyFill)
    {
        enemyHealth.value = enemy.enemyHealth;
        if (enemyHealth.value <= 0)
        {
            enemyFill.SetActive(false);
            // Winner Next level
        }
    }
}
