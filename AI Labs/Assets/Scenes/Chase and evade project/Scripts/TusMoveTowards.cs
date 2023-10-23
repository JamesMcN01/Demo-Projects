using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TusMoveTowards : MonoBehaviour
{
  // allows the enemy to target player
    public GameObject target;
  // sets the movement speed
   public float speed = 1.0f;
  //  double test = 1.0;

  // range where the chase can be begin
    public float  ChaseRange;

  // range where weapons can be fired
  public float fireRange;
  // used to track how many bombs the player has
  int playerbombs = 0;

  public GameObject rocket;

  float score;

  float spawnTime;

  public float timeBetweenSpawn;

  int death =1;
        // Start is called before the first frame update
    void Start()
    {
      
     
        
    }

    // Update is called once per frame
    void Update()
    {
        float speedDelta = speed * Time.deltaTime;

        Vector3 newPosition = tusMoveTowards(transform.position, target.transform.position, speedDelta);

        transform.position = newPosition;

        int bombValue =  target.GetComponent<weaponManager>().bombCount;  

        playerbombs = bombValue; 

        // gets the score value from the score counter script
        float totalScore = target.GetComponent<scoreCounter>().scoreTotal;

        score = totalScore;

        

        // used to increase the enemy movement speed when the score reaches the below values
        if(score > 100 && score < 300)
        {
            speed = 2.5f;
        }
        else if( score > 300 && score< 500)
        {
          speed = 3.0f;
        }
        else if( score > 500 && score < 700)
        {
          speed = 3.5f;
        }

    }

      Vector3 tusMoveTowards(Vector3 predatorPosition, Vector3 target, float maxDistanceDelta)
      {
          // Draws vector that contaons distance from the predator to the target and also contains distance
          Vector3 rangeToClose = target - predatorPosition;
          // draws a line that represents the distance from the enemy to the player
          Debug.DrawRay(predatorPosition, rangeToClose, Color.green);
        // extracts distance of rangetoclose
        float distance =rangeToClose.magnitude;
        // could use distance for when player is in range

      //     Debug.Log("The distance" + distance);
        // extracts direction of rangetoclose
            Vector3 normRangeToClose = rangeToClose.normalized;
            // draws normal
            Debug.DrawRay(predatorPosition, normRangeToClose, Color.white);

        // checks to see if the player has bombs
        if (playerbombs < 1)
        {
            if(distance < fireRange +1 && Time.time > spawnTime)
            {
              
            Debug.Log("The enemy has fired a rocket");
            
            spawnTime= Time.time + timeBetweenSpawn;
            // spawns in the bomb
            Spawn();
            }
           // checks the distance from the player and sees if its in the chaserange
          // if true the player will be chased by the enemy 
           if(distance  < ChaseRange){

              // new position is going to be current predator position + our speed delta along the direction
              Vector3 newPosition = predatorPosition + normRangeToClose * maxDistanceDelta;
              return newPosition;
           }
            // if false the enemy will remain in the same position
            else{
                  return predatorPosition;
                }        
          }
      // if the player posses bombs the enemy will get away from the player 
     else{
            // force the enemy beond the chase range 
        if (distance < ChaseRange +3)
        {
           // new position is going to be current predator position - our speed delta along the direction
              Vector3 newPosition = predatorPosition - normRangeToClose * maxDistanceDelta;
              return newPosition;
        }
        else{
          return predatorPosition;
        }
     }
       
   }

   public void OnCollisionEnter2D(Collision2D collision)
   {

      
        // if the bomb collides wth the enemy the bomb will be destroyed
        if(collision.collider.tag == "bomb")
        {
          GameObject bomb = collision.gameObject;

          KillCounter killCounter =target.gameObject.GetComponent<KillCounter>();

          killCounter.Death(death);

          Debug.Log("The bomb has been destroyed");
        
          Destroy(bomb);
        } 
   }

        void Spawn()
    {
      
        // spawns the rocket at the base of the player 
        Instantiate(rocket, transform.position + new Vector3(0, 0, 0), transform.rotation);
    }

  


    
    
}
