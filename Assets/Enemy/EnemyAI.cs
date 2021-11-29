using System;
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
    bool isProvoked = false;

    void Start()
    {
        this.navMeshAgent = this.GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        this.distanceToTarget = Vector3.Distance(target.position, this.transform.position);

        if(isProvoked)
        {
            EngageTarget();
        }
        else if(this.distanceToTarget <= this.chaseRange)
        {
            isProvoked = true;
        }
    }

    private void EngageTarget()
    {
        if(this.distanceToTarget >= this.navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }
        
        if(this.distanceToTarget <= this.navMeshAgent.stoppingDistance)
        {
           AttackTarget();
        }
    }

    private void ChaseTarget()
    {
        this.navMeshAgent.SetDestination(this.target.position);
    }
    
    private void AttackTarget()
    {
        Debug.Log(name + " nom nom, brains");
    }

    void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, this.chaseRange);
    }
}
