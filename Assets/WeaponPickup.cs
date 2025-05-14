using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour {

    public GameObject text;
    public GameObject newWeapon;
    public GameObject oldWeapon;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other) {
        if (other.tag == "Player") {
            text.SetActive(true);

            if (Input.GetKey(KeyCode.E)) {
                newWeapon.SetActive(true);
                oldWeapon.SetActive(false);
                gameObject.SetActive(false);
                text.SetActive(false);
            }
            
        }
    }
    
    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            text.SetActive(false);
        }
    }
}
