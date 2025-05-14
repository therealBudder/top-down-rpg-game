using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    
    public GameObject text;
    public GameObject usedText;
    
    public enum ShopType {Health, Level, None}

    public ShopType type;
    private CharacterMovementController player;
    
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
            player = other.GetComponent<CharacterMovementController>();

            if (type == ShopType.None) {
                text.SetActive(false);
            }
            else {
                text.SetActive(true);
            }
            

            if (Input.GetKey(KeyCode.E)) {
                text.SetActive(false);

                if (type == ShopType.Health) {
                    player.health = player.maxHealth;
                    type = ShopType.None;
                }
                else if (type == ShopType.Level) {
                    player.xp += player.maxXp;
                    type = ShopType.None;
                }
                
            }

            if (type == ShopType.None) {
                usedText.SetActive(true);
            }
            
        }
    }
    
    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            text.SetActive(false);
            usedText.SetActive(false);
        }
    }
}
