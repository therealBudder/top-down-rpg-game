using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollision : MonoBehaviour {

    public CharacterMovementController character;
    private EnemyMovementController enemy;
    public int damage;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Enemy" && character.isAttacking) {
            enemy = other.GetComponent<EnemyMovementController>();
            enemy.GetHit(damage);
            
        }
    }
    
}
