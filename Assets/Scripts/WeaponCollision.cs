using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollision : MonoBehaviour {

    public CharacterMovementController character;
    public int damage;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Enemy" && CharacterMovementController.isAttacking) {
            EnemyMovementController enemy = other.GetComponent<EnemyMovementController>();
            if (enemy.health > 0) {
                other.GetComponent<Animator>().SetTrigger("Get Hit");
                enemy.health -= damage;
            }
            
        }
    }
}
