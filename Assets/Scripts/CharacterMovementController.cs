using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMovementController : MonoBehaviour {

    public float speed;
    public float turnSpeed;

    public int maxHealth;
    public int health;
    public int hitCounter;
    public float comboDuration;
    
    public GameObject weapon;
    public bool canAttack = true;
    public bool canGetHit = true;
    public bool isAttacking = false;
    public float attackCooldown = 0.5f;
    public float comboCooldown = 0.5f;
    public float attackDuration = 0.4f;

    public Slider healthBar;
    
    private Rigidbody rigidBody;
    // private float mouseY;
    // private float mouseX;

    private float xMovement;
    private float yMovement;

    private bool run;
    private bool jump;
    private bool attack;
    private bool guard;

    private Animator animator;

    private void Start() {
        health = maxHealth;
        healthBar.maxValue = maxHealth;
        animator = GetComponent<Animator>();

    }

    void Awake() {
        rigidBody = GetComponent<Rigidbody>();
    }
    
    private void OnEnable ()
    {
        rigidBody.isKinematic = false;
    }
    
    private void OnDisable ()
    {
        rigidBody.isKinematic = true;
    }

    // Update is called once per frame
    void Update() {
        
        if (health < 0) {health = 0;}
        healthBar.value = health;

        if (health <= 0) {
            animator.SetTrigger("Die");
        }
        else {
            xMovement = Input.GetAxis("Horizontal");
            yMovement = Input.GetAxis("Vertical");
        
            run = Input.GetKey(KeyCode.LeftShift);
            jump = Input.GetKeyDown(KeyCode.Space);
            attack = Input.GetKeyDown(KeyCode.Mouse0);
            guard = Input.GetKey(KeyCode.Mouse1);


            if (xMovement != 0 || yMovement != 0) {
                animator.SetBool("walking", true);
                animator.SetBool("running", run);
            }
            else {
                animator.SetBool("walking", false);
                animator.SetBool("running", false);
            }

        
            animator.SetBool("jumping", jump);
            if (attack && canAttack) {
                Attack();
            }

            if (guard) {
                hitCounter = 0;
            }
            animator.SetBool("guarding", guard);
        }

    }

    private void FixedUpdate() {
        
        Move();
        
    }

    public void Attack() {

        isAttacking = true;
        canAttack = false;
        animator.SetTrigger("attacking");
        hitCounter++;
        
        StartCoroutine(ResetAttacking());
    }

    IEnumerator ResetAttackCooldown() {

        if (hitCounter == 0) {
            StartCoroutine(ResetHitCounter());
        }
        else if (hitCounter >= 5) {
            hitCounter = 0;
            yield return new WaitForSeconds(attackCooldown + comboCooldown);
        }
        else {yield return new WaitForSeconds(attackCooldown);}
        canAttack = true;
    }

    IEnumerator ResetHitCounter() {
        yield return new WaitForSeconds(comboDuration);
        hitCounter = 0;
    }

    IEnumerator ResetAttacking() {
        yield return new WaitForSeconds(attackDuration);
        isAttacking = false;
        StartCoroutine(ResetAttackCooldown());
    }

    void Move() {

        Vector3 movementX = Vector3.right * xMovement;
        Vector3 movementY = Vector3.forward * yMovement;

        Vector3 movementDirection = movementX + movementY;
        
        // rigidBody.MovePosition(rigidBody.position + movementDirection * speed * Time.deltaTime);
        
        if (movementDirection != Vector3.zero) {
            Quaternion rotate = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(movementDirection, Vector3.up), turnSpeed * Time.deltaTime); 
            rigidBody.MoveRotation(rotate);
        }

    }

}
