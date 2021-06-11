using System;
using System.Collections;
using System.Globalization;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Panda;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;

    private Transform playerTransform;

    [Task]

    private bool playerNoticed;

    [Task]

    private bool alert;

    [Task]

    private bool enemyDetection;
    public UnityEvent AlertOthersEnemies;
    private bool canAlertOthersEnemies;
    private Vector3 startPlayerPosition;
    public Vector3 lastPlayerPosition;
    public bool PlayerNoticed => playerNoticed;
    public bool EnemyDetection
    {
        get => enemyDetection;
        set => enemyDetection = value;
    }
    public bool Alert
    {
        get => alert;
        set => alert = value;
    }

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        startPlayerPosition = transform.position;
        canAlertOthersEnemies = false;
        playerNoticed = false;
        alert = false;
    }

    void Update()
    {
        Ray raycast = new Ray(transform.position, (playerTransform.position - transform.position).normalized);
        if (Physics.Raycast(raycast, out RaycastHit hit, 50) && hit.transform.CompareTag("Player"))
        {
            alert = true;
            enemyDetection = false;
            playerNoticed = true;
            lastPlayerPosition = playerTransform.position;
        }
        else
        {
            playerNoticed = false;
        }
    }

    [Task]
    void ChasePlayer()
    {
        agent.SetDestination(lastPlayerPosition);
        Task.current.Succeed();
    }

    [Task]
    void SurveyAround()
    {
        if (playerNoticed)
        {
            Task.current.Fail();
            return;
        }

        agent.SetDestination(transform.position + Random.rotation * Vector3.forward * 3);
        Task.current.Succeed();
    }

    [Task]
    void SeeLastPosition()
    {
        agent.SetDestination(lastPlayerPosition);
        if (transform.position == lastPlayerPosition)
        {
            Task.current.Succeed();
        }
    }

    [Task]
    void LeavePlayer()
    {
        alert = false;
        Task.current.Succeed();
    }

    [Task]
    void ReturnToOrigin()
    {
        agent.SetDestination(startPlayerPosition);

        if (transform.position == startPlayerPosition)
        {
            Task.current.Succeed();
        }
    }
}