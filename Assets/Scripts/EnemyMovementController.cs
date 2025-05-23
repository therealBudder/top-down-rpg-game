using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyMovementController : MonoBehaviour {

    public bool canAttack = true;
    public bool isAttacking = false;
    public bool stunned = false;
    public float attackCooldown;
    public float attackDuration;
    public float stunDuration;
    public float range = 25;
    public int xp = 10;
    public Slider healthBar;
    
    private Transform target;
    public float attackDistance;
    public int maxHealth;
    public int health;
    private bool alive = true;
    
    private Vector3 startingPosition;
    private NavMeshAgent agent;
    private Animator animator;
    private float distance;
    
    // Start is called before the first frame update

    void Start() {
        target = GameObject.FindWithTag("Player").transform;
        health = maxHealth;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        startingPosition = agent.transform.position;
        print(startingPosition);
        healthBar.maxValue = maxHealth;
    }

    // Update is called once per frame
    void Update() {
        
        healthBar.value = health;
        
        healthBar.gameObject.SetActive(false);

        if (health < maxHealth) {
            healthBar.gameObject.SetActive(true);
        }
        
        distance = Vector3.Distance(agent.transform.position, target.position);
        NavMeshPath navMeshPath = new NavMeshPath();

        if (health <= 0) {
            health = 0;
            agent.isStopped = true;
            healthBar.gameObject.SetActive(false);
            animator.SetTrigger("Die");
            animator.SetBool("Walk", false);
            if (alive) {
                StartCoroutine(DeleteObject());
                alive = false;
            }
        }
        else if (distance < attackDistance && agent.CalculatePath(target.position, navMeshPath) && navMeshPath.status == NavMeshPathStatus.PathComplete) {
            agent.isStopped = true;
            animator.SetBool("Walk", false);
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, target.position - transform.position, 2 * Time.deltaTime, 0.0f);
            // Debug.DrawRay(transform.position, newDirection, Color.red);
            transform.rotation = Quaternion.LookRotation(newDirection);
            if (canAttack && !isAttacking && !stunned) {Attack();}
            
        }
        else if (distance <= range && agent.CalculatePath(target.position, navMeshPath) && navMeshPath.status == NavMeshPathStatus.PathComplete) {
            // agent.SetPath(navMeshPath);
            agent.isStopped = false;
            // animator.SetBool("attacking", false);
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
            // animator.SetBool("atacking", false);
            agent.destination = startingPosition;
        }

    }
    
    public void Attack() {

        isAttacking = true;
        canAttack = false;
        animator.SetTrigger("attacking");
        
        StartCoroutine(ResetAttacking());
        StartCoroutine(ResetAttackCooldown());

    }

    public void GetHit(int damage) {

        stunned = true;
        
        if (health > 0) {
            animator.SetTrigger("Get Hit");
            health -= damage;
            
            
            StartCoroutine(ResetStun());
            StartCoroutine(Knockback());
        }

    }

    IEnumerator Knockback() {
        float timer = 0.0f;

        while (timer <= 0.1f) {
            timer += Time.deltaTime;
            transform.position -= transform.forward * 3 * Time.deltaTime;
            yield return null;
        }
        
    }

    IEnumerator ResetStun() {
        yield return new WaitForSeconds(stunDuration);
        stunned = false;
    }

    IEnumerator ResetAttackCooldown() {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    IEnumerator ResetAttacking() {
        yield return new WaitForSeconds(attackDuration);
        isAttacking = false;
    }

    IEnumerator DeleteObject() {
        yield return new WaitForSeconds(3);
        target.GetComponent<CharacterMovementController>().xp += xp;
        Destroy(gameObject);
    }
    
}
