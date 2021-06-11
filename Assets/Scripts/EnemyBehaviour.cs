using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class EnemyBehaviour : MonoBehaviour
{
    private Enemy enemyBehaviour;
    private List<EnemyPatrol> _enemyBehaviour;
    public UnityEvent AlertOthers;
    
    void Start()
    {
        enemyBehaviour = GetComponent<Enemy>();
        AlertOthers = new UnityEvent();
        _enemyBehaviour = FindObjectsOfType<EnemyPatrol>().ToList();
        foreach (var enemy in _enemyBehaviour)
        {
            AlertOthers.AddListener(enemy.NoticePlayer);
        }
    }
    
    void Update()
    {
        if (enemyBehaviour.PlayerNoticed)
        {
            AlarmAll();
        }
    }
    
    void AlarmAll()
    {
        AlertOthers.Invoke();
    }
}
