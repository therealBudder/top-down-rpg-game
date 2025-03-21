using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMovementController : MonoBehaviour {

    public float speed;
    public float turnSpeed;
    
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
        if (attack) {animator.SetTrigger("attacking");}
        animator.SetBool("guarding", guard);

    }

    private void FixedUpdate() {
        
        Move();
        
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
