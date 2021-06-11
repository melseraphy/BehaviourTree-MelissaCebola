using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]private Camera _camera;
    [SerializeField]private NavMeshAgent _agent;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _agent.autoTraverseOffMeshLink = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                _agent.SetDestination(hit.point);
            }
        }

        if (_agent.isOnOffMeshLink)
        {
            StartCoroutine(Move());
        }
    }

    IEnumerator Move()
    {
        _animator.SetBool("Move", true);
        yield return new WaitForSeconds(1f);

        _animator.SetBool("Move", false);
        _agent.CompleteOffMeshLink();
        yield return null;
    }
}