using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponManager : MonoBehaviour
{
   public  int bombCount = 0;
    
   public GameObject bomb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
           // enables the player to pick up bombs
         if (collision.collider.tag == "BombPickup")
        {
            GameObject bomb = collision.gameObject;

            Debug.Log("The player collided with: " + bomb.name);
            // assisgns the ammount of bombs the player has to the inventory
            bombCount += bomb.GetComponent<BombValue>().ammo;

            Debug.Log("The player now has " + bombCount + "bombs");
            // destroys the pickup
            Destroy(bomb);
        }
        else if(collision.collider.tag =="Rocket")
        {
             GameObject bomb = collision.gameObject;

              Destroy(bomb);
        }
    }
    
    void OnFire()
    {
        
        
           
        if(bombCount > 0)
        {
            // decreases the bomb storage by one
            bombCount --; 
            Debug.Log("The bomb was removed from the inventory");
            // spawns in the bomb
            Spawn();
          
        }
       
        
       
    }
    
    void Spawn()
    {
      
        // spawns the bomb at the base of the player 
        Instantiate(bomb, transform.position + new Vector3(0, 0, 0), transform.rotation);
    } 
   
}
