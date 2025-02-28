using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float speed;

    private float xMovement;
    private float yMovement;

    private Vector3 offset;
    private Vector3 newPos;
    
    [SerializeField]
    private Transform lookAtTarget;

    [SerializeField] 
    private Transform followTarget;
    
    // Start is called before the first frame update
    void Start() {
        offset = lookAtTarget.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        xMovement = Input.GetAxis("Horizontal");
        yMovement = Input.GetAxis("Vertical");
    }

    private void LateUpdate() {
        // transform.LookAt(lookAtTarget);

        newPos = transform.position;
        newPos = lookAtTarget.position - offset;
        // newPos.z = lookAtTarget.position.z - offset.z;
        transform.position = newPos;
        
        Move();
    }

    void Move() {

        Vector3 movementY = Vector3.forward * yMovement;
        Vector3 movementX = Vector3.right * xMovement;

        Vector3 movementDirection = movementX + movementY;
        
        // transform.position += movementDirection * speed * Time.deltaTime;

    }
}
