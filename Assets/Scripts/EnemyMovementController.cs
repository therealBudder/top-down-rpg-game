using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementController : MonoBehaviour {

    public Transform target;
    public float attackDistance;
    
    private NavMeshAgent agent;
    private Animator animator;
    private float distance;
    
    // Start is called before the first frame update

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update() {

        distance = Vector3.Distance(agent.transform.position, target.position);

        if (distance < attackDistance) {
            agent.isStopped = true;
            animator.SetBool("Attack", true);
            animator.SetBool("Walk", false);
        }
        else {
            agent.isStopped = false;
            animator.SetBool("Attack", false);
            animator.SetBool("Walk", true);
            agent.destination = target.position;
        }

    }

    void OnAnimatorMove() {
        if (animator.GetBool("Attack") == false) {
            
        }
    }
    
}
