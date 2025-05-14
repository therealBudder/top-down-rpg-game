using System;
using System.Collections;
using TMPro;
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
    public int maxXp = 100;
    public int xp = 0;
    public int level = 1;
    public Text levelText;
    
    public GameObject weapon;
    public WeaponCollision weaponController;
    public bool canAttack = true;
    public bool canGetHit = true;
    public bool isAttacking = false;
    public float attackCooldown = 0.5f;
    public float comboCooldown = 0.5f;
    public float attackDuration = 0.4f;

    public Slider healthBar;
    public Slider xpBar;
    
    private Rigidbody rigidBody;
    // private float mouseY;
    // private float mouseX;

    private float xMovement;
    private float yMovement;

    private bool run;
    private bool dodge;
    private bool attack;
    private bool guard;

    private Animator animator;

    private void Start() {
        xp = 0;
        health = maxHealth;
        animator = GetComponent<Animator>();
        weaponController = weapon.gameObject.GetComponent<WeaponCollision>();

    }

    void Awake() {
        rigidBody = GetComponent<Rigidbody>();
    }
    
    // private void OnEnable ()
    // {
    //     rigidBody.isKinematic = false;
    // }
    //
    // private void OnDisable ()
    // {
    //     rigidBody.isKinematic = true;
    // }

    // Update is called once per frame

    public void LevelUp() {
        xp -= maxXp;
        maxXp = (int)(maxXp * 1.5);
        level++;
        levelText.text = "Level " + level;
        maxHealth = (int)(maxHealth * 1.4);
        health = maxHealth;
        xpBar.maxValue = maxXp;
        weaponController.damage = (int)(weaponController.damage * 1.4);
        
        UpdateHealth();
    }
    
    void UpdateXp() {
        
        if (xp >= maxXp) {
            LevelUp();
        }
        
        xpBar.value = xp;
    }

    void UpdateHealth() {
        
        if (health < 0) {health = 0;}
        healthBar.maxValue = maxHealth;
        healthBar.value = health;
        
    }
    
    void Update() {
        
        UpdateXp();
        UpdateHealth();
        
        if (health <= 0) {
            animator.SetTrigger("Die");
        }
        else {
            xMovement = Input.GetAxis("Horizontal");
            yMovement = Input.GetAxis("Vertical");
        
            run = Input.GetKey(KeyCode.LeftShift);
            dodge = Input.GetKeyDown(KeyCode.Space);
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
            
            animator.SetBool("dodge", dodge);
            if (dodge) {
                Dodge();
            }
            
            if (attack && canAttack) {
                Attack();
            }

            if (guard) {
                hitCounter = 0;
                animator.SetBool("walking", false);
                animator.SetBool("running", false);
            }
            animator.SetBool("guarding", guard);
        }

    }

    private void FixedUpdate() {
        
        Move();
        
    }

    private void Dodge() {
        canGetHit = false;
        StartCoroutine(ResetInvulnerability());
        StartCoroutine(Roll());
    }

    IEnumerator ResetInvulnerability() {
        yield return new WaitForSeconds(1);
        canGetHit = true;
    }
    
    IEnumerator Roll() {
        float timer = 0.0f;

        while (timer <= 0.4f) {
            timer += Time.deltaTime;
            transform.position += transform.forward * 1 * Time.deltaTime;
            yield return null;
        }
        
    }

    public void Attack() {

        isAttacking = true;
        canAttack = false;
        if (hitCounter == 3) {animator.SetTrigger("combo ender");}
        else {animator.SetTrigger("attacking");}
        hitCounter++;
        
        StartCoroutine(ResetAttacking());
        StartCoroutine(ResetAttackCooldown());
    }

    IEnumerator ResetAttackCooldown() {

        if (hitCounter == 1) {
            StartCoroutine(ResetHitCounter());
        }
        if (hitCounter >= 4) {
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
