using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyPatrol : MonoBehaviour
{
    private Enemy enemyBehaviour;
    private List<EnemyBehaviour> enemyStationed;
    private Transform playerTransform;

    void Start()
    {
        enemyBehaviour = GetComponent<Enemy>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void NoticePlayer()
    {
        enemyBehaviour.EnemyDetection = true;
        enemyBehaviour.Alert = true;
        enemyBehaviour.lastPlayerPosition = playerTransform.position;
    }
}
