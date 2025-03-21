using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementController : MonoBehaviour {

    public Transform target;
    public float attackDistance;
    
    private Vector3 startingPosition;
    private NavMeshAgent agent;
    private Animator animator;
    private float distance;
    
    // Start is called before the first frame update

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        startingPosition = agent.transform.position;
        print(startingPosition);
    }

    // Update is called once per frame
    void Update() {

        distance = Vector3.Distance(agent.transform.position, target.position);
        NavMeshPath navMeshPath = new NavMeshPath();
        
        if (distance < attackDistance && agent.CalculatePath(target.position, navMeshPath) && navMeshPath.status == NavMeshPathStatus.PathComplete) {
            agent.isStopped = true;
            animator.SetBool("Attack", true);
            animator.SetBool("Walk", false);
        }
        else if (agent.CalculatePath(target.position, navMeshPath) && navMeshPath.status == NavMeshPathStatus.PathComplete) {
            // agent.SetPath(navMeshPath);
            agent.isStopped = false;
            animator.SetBool("Attack", false);
            animator.SetBool("Walk", true);
            agent.destination = target.position;
            
        }
        else if (Vector3.Distance(agent.transform.position, startingPosition) < 1) {
            
            agent.isStopped = true;
            animator.SetBool("Walk", false);
            
        }
        else {
            agent.isStopped = false;
            animator.SetBool("Walk", true);
            animator.SetBool("Attack", false);
            agent.destination = startingPosition;
        }

    }

    void OnAnimatorMove() {
        if (animator.GetBool("Attack") == false) {
            
        }
    }
    
}
