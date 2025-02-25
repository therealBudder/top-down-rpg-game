using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float speed;

    private float xMovement;
    private float yMovement;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        xMovement = Input.GetAxis("Horizontal");
        yMovement = Input.GetAxis("Vertical");
        
        Move();
    }
    
    void Move() {

        Vector3 movementY = Vector3.forward * yMovement;
        Vector3 movementX = Vector3.right * xMovement;

        Vector3 movementDirection = movementX + movementY;
        
        // transform.position += movementDirection * speed * Time.deltaTime;

    }
}
