using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tusMoveTowardss : MonoBehaviour
{
    public GameObject key;
  // allows the enemy to target player
    public GameObject target;
  // sets the movement speed
   public float speed = 1.0f;
  //  double test = 1.0;

  private Animator enemyAnimator;

  private SpriteRenderer enemySprite;

  public WaypointPatternMovement patrol;

  // range where the chase can be begin
    public float  ChaseRange;

  public CircleCollider2D leftAttack;

  public CircleCollider2D rightAttack;

  int attackRange =5;

  public bool hit = false;

     private bool dead = false;
 
        // Start is called before the first frame update
    void Start()
    {
      
      enemyAnimator = gameObject.GetComponent<Animator>();

      enemySprite = gameObject.GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    { 
        if (dead)
        {
          return;
        }
        float speedDelta = speed * Time.deltaTime;

        Vector3 newPosition = TusMoveTowardss(transform.position, target.transform.position, speedDelta);

        transform.position = newPosition;

       

    }

      Vector3 TusMoveTowardss(Vector3 predatorPosition, Vector3 target, float maxDistanceDelta)
      {
          // Draws vector that contaons distance from the predator to the target and also contains distance
          Vector3 rangeToClose = target - predatorPosition;
          // draws a line that represents the distance from the enemy to the player
  //        Debug.DrawRay(predatorPosition, rangeToClose, Color.green);
        // extracts distance of rangetoclose
        float distance =rangeToClose.magnitude;
        // could use distance for when player is in range
          if(rangeToClose.x < 0)
          {
              enemySprite.flipX= true;
            
          }
          else{
            enemySprite.flipX = false;
         
          }
      //     Debug.Log("The distance" + distance);
        // extracts direction of rangetoclose
            Vector3 normRangeToClose = rangeToClose.normalized;
            // draws normal
      //      Debug.DrawRay(predatorPosition, normRangeToClose, Color.white);

    // checks the distance from the player and sees if its in the chaserange
          // if true the player will be chased by the enemy 
           if(distance  < ChaseRange){
              // checks if the player is in the attack range and the hit variable is false
              if(distance < attackRange && hit == false)
              {
                // sets hit true
                hit =true;
                
                   if(rangeToClose.x < 0)
                    {
                      // enables the left attack radius iff facing left
                      leftAttack.enabled = true;
                    }
                     else{
                       // enables the right attack radius if facing right
                        rightAttack.enabled = true;
                      
                    }
                    StartCoroutine(CoolDown(10f));
              }
              else
              {
                  
                // disables left attack   
                leftAttack.enabled = false;
                  
               // disables right attack 
                rightAttack.enabled = false;
                         
              }
              // disables the patrol script so that the enemy may chase the player
              patrol.enabled= false;
              // new position is going to be current predator position + our speed delta along the direction
              Vector3 newPosition = predatorPosition + normRangeToClose * maxDistanceDelta;
              // enables run animation
               enemyAnimator.SetBool("Run", true);
              // go to the player position
              return newPosition;

             
           }
       
            // if false the enemy will remain in the same position
            else{
                  // disable run animation
                   enemyAnimator.SetBool("Run", false);

                // enables the patrol script
                 patrol.enabled = true;
                 
                 return predatorPosition;
                }        
   }

   void OnCollisionEnter2D(Collision2D collision)
   {
        // checks if the enemy is colliding with the player attack radius
     if (collision.collider.tag == "rightAttack" || collision.collider.tag == "leftAttack")
     {
        // stop runnning
       enemyAnimator.SetBool("Run", false);
        // starts death animation
       enemyAnimator.SetTrigger("Death");
        // sets a timer on Despawn
       StartCoroutine(Despawn(10));
     }
   }

   IEnumerator CoolDown(float waitTime)
   {
     
      yield return new WaitForSeconds(waitTime);
      hit =false;
   }

   IEnumerator Despawn(float waitTime)
   {
      //Sets dead true
     dead = true;
    // takes in time from timer
     yield return new WaitForSeconds(waitTime);
     // Destroys self
     Destroy(gameObject);
    // calls spawn
     if(gameObject.tag == "Brute")
        {
            Spawn();
        }
   }
    
    public void Spawn()
    {
        // spawns a key at dead enemy position
        Instantiate(key, transform.position + new Vector3(0, 0, 0), transform.rotation);
    }

 
    
}
