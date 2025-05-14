using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponCollision : MonoBehaviour
{
    public int damage;
    public EnemyMovementController thisEnemy;
    private CharacterMovementController player;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player" && thisEnemy.isAttacking) {
            player = other.GetComponent<CharacterMovementController>();
            Animator playerAnimator = other.GetComponent<Animator>();
            if (player.health > 0 && !playerAnimator.GetBool("guarding") && player.canGetHit) {
                // player.canGetHit = false;
                playerAnimator.SetTrigger("Get Hit");
                player.health -= damage;
                // StartCoroutine(ResetHitCooldown());
            }
            else if (player.health > 0 && playerAnimator.GetBool("guarding") && player.canGetHit) {
                playerAnimator.SetTrigger("Block Attack");
            }
            
        }
    }

    // IEnumerator ResetHitCooldown() {
    //     yield return new WaitForSeconds(hitCooldown);
    //     player.canGetHit = true;
    // }
}
