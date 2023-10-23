using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrenchBehaviour : MonoBehaviour
{
    int repair = 12;
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
          // if the rocket collides with the enemy the enemy will be destroyed
        if(collision.collider.tag == "Player")
        {
         

               // access the health scrript on the player
          HealthManager HealthManager = collision.gameObject.GetComponent<HealthManager>();
          // calls the hit method on the player script
            HealthManager.Repair(repair);

           Debug.Log("Health was increased to " + HealthManager.health);
            Destroy(gameObject);
        }
    }
}
