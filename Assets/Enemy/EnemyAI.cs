using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 5f;

    NavMeshAgent navMeshAgent;
    float distanceToTarget = Mathf.Infinity;

    void Start()
    {
        this.navMeshAgent = this.GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        this.distanceToTarget = Vector3.Distance(target.position, this.transform.position);
        if(this.distanceToTarget <= this.chaseRange)
        {
            navMeshAgent.SetDestination(this.target.position);
        }
    }
}
