using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 12f;
    [SerializeField] float turnSpeed = 5f;

    NavMeshAgent navMeshAgent;
    Animator animator;
    float distanceToTarget = Mathf.Infinity;
    bool isProvoked = false;

    void Start()
    {
        this.navMeshAgent = this.GetComponent<NavMeshAgent>();
        this.animator = this.GetComponent<Animator>();
    }

    void Update()
    {
        this.distanceToTarget = Vector3.Distance(target.position, this.transform.position);

        if(this.isProvoked)
        {
            EngageTarget();
        }
        else if(this.distanceToTarget <= this.chaseRange)
        {
            this.isProvoked = true;
        }
    }

    public void OnDamage()
    {
        this.isProvoked = true;
    }

    private void EngageTarget()
    {
        FaceTarget();

        if(this.distanceToTarget >= this.navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }
        
        if(this.distanceToTarget <= this.navMeshAgent.stoppingDistance)
        {
           AttackTarget();
        }
        else
        {
            this.animator.SetBool("Attack", false);
        }
    }

    private void ChaseTarget()
    {
        this.animator.SetTrigger("Move");
        this.navMeshAgent.SetDestination(this.target.position);
    }
    
    private void AttackTarget()
    {
        this.animator.SetBool("Attack", true);
    }

    private void FaceTarget()
    {
        Vector3 direction = (this.target.position - this.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, lookRotation, Time.deltaTime * this.turnSpeed);
    }

    void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, this.chaseRange);
    }
}
