using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocketBehaviour : MonoBehaviour
{
    public GameObject enemy;
    // speed of rocket
    public float speed = 4.5f;
    // range of rocket
    public int chaseRange;

    int damage = 15;

    int pointReduce = 100;

    public GameObject rocket;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float speedDelta = speed * Time.deltaTime;

        Vector3 newPosition = RocketMoveTowards(transform.position, enemy.transform.position, speedDelta);

        transform.position = newPosition;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        // if the rocket collides with the enemy the enemy will be destroyed
        if(collision.collider.tag == "Player")
        {
         

               // access the health scrript on the player
          HealthManager HealthManager = collision.gameObject.GetComponent<HealthManager>();
          // calls the hit method on the player script
            HealthManager.Hit(damage);
        // access the score counter script
        scoreCounter scoreCounter = collision.gameObject.GetComponent<scoreCounter>();
        // calls player hit method
        scoreCounter.PLayerHit(pointReduce);

          
        }
    }
    
    Vector3 RocketMoveTowards(Vector3 rocketPosition, Vector3 enemy, float maxDistanceDelta)
    {
        // Draws vector that contaons distance from the rocket to the enemy and also contains distance
        Vector3 rangeToClose = enemy - rocketPosition;
        // draws a line that represents the distance from the rocket to the enemy
  //      Debug.DrawRay(rocketPosition, rangeToClose, Color.red);
        // extracts distance of rangetoclose
        float distance = rangeToClose.magnitude;

        Vector3 normRangeToClose = rangeToClose.normalized;

        Debug.DrawRay(rocketPosition, normRangeToClose, Color.blue);
        if(distance > chaseRange && distance < chaseRange + 2)
        {
            Debug.Log("The Rocket was Destroyed");
            Destroy(rocket);
            return rocketPosition;
        }
       else{
        if (distance < chaseRange)
        {

            // new position is going to be current rocket position + our speed delta along the direction
            Vector3 newPosition = rocketPosition + normRangeToClose * maxDistanceDelta;
            return newPosition;
        }
       else{
        return rocketPosition;
       }
       }
        
      
        // if false the rocket will remain in the same position
      
        

    } 

    
}
