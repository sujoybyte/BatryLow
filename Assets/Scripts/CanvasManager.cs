﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public GameObject winner;
    public GameObject enemy;

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

    private void Start()
    {
        player = playerObject.GetComponent<PlayerControl>();
        rEnemy = rEnemyObject.GetComponent<EnemyControl>();
        gEnemy = gEnemyObject.GetComponent<EnemyControl>();
        bEnemy = bEnemyObject.GetComponent<EnemyControl>();
        winner.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        if (enemy.GetComponentsInChildren<Transform>().Length == 0)
        {
            winner.SetActive(true);
        }

        playerHealth.value = player.playerHealth;
        if (playerHealth.value <= 0)
        {
            playerFill.SetActive(false);
            // Loser
        }

        Enemy(rEnemyHealth, rEnemy);
        Enemy(gEnemyHealth, gEnemy);
        Enemy(bEnemyHealth, bEnemy);
    }

    private void Enemy(Slider enemyHealth, EnemyControl enemy)
    {
        enemyHealth.value = enemy.enemyHealth;
    }
}
