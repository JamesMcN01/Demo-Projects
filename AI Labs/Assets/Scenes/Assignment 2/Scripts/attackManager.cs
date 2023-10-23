using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackManager : MonoBehaviour
{
    // used to reference the player
    public GameObject target;
     // used to reference the amount of bombs the players have
    int playerbombs;
    // used to store player health
    int playerHealth;
    // the amount of damage that can be caused
    int damage = 10;

   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // accesss the amount of bombs the player has
      int  totalbombs = target.GetComponent<weaponManager>().bombCount;
        // stores the amount of bombs the player has
      playerbombs = totalbombs;

     
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // checks if collides with player and if player has bombs
        if(collision.collider.tag == "Player" &&  playerbombs < 1)
        {
            Debug.Log("The enemy rammed the player");
            // access the health scrript on the player
          HealthManager HealthManager = collision.gameObject.GetComponent<HealthManager>();
          // calls the hit method on the player script
            HealthManager.Hit(damage);
            
        }
    }

   

    
}
