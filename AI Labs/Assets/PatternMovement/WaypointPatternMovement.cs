using System;
using System.Collections;
using UnityEngine;

public class WaypointPatternMovement : MonoBehaviour
{
    public knightManager goldTotal;
   [Serializable]
   public struct WaypointData
   {
       public GameObject location;
       public int speed;
       public float timeBetween;
      
   }
   public Animator enemyAnimator;

    private SpriteRenderer enemySprite;

    public WaypointData[] pattern;

    private int patternIndex = 0;

    public float speed =0;
    
    

    private bool paused = false;

    // Use this for initialization
    void Start ()
    {
		enemyAnimator = gameObject.GetComponent<Animator>();

        enemySprite = gameObject.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(paused)
        {
            return;
        }
         // Process the current instruction in our control data array
        WaypointData data = pattern[patternIndex];
        speed = data.speed; 

        if(goldTotal.gold == 100)
        {
          
            speed ++;
              Debug.Log("speed" +speed);
        }
        else if(goldTotal.gold == 300)
        {
            speed ++;
            data.timeBetween --;
        }
        else if( goldTotal.gold == 500)
        {
            speed ++;
        }
     

        // Find the range to close vector
        Vector3 rangeToClose = data.location.transform.position - transform.position;

        if( rangeToClose != Vector3.zero)
        {
            enemyAnimator.SetBool("Run", true);
            if(rangeToClose.x < 0)
            {
                enemySprite.flipX = true;
            }
            else{
                enemySprite.flipX = false;
            }
        }
       
        // Draw this vector at the position of the enemy
//        Debug.DrawRay(transform.position, rangeToClose, Color.cyan);

        // What's our distance to the waypoint?
        float distance = rangeToClose.magnitude;

        // How far do we move each frame
        float speedDelta = speed * Time.deltaTime;

        // If we're close enough to the current waypoint 
        // then increase the pattern index
        
       
            if (distance <= speedDelta)
            {
                        
                patternIndex++;
                        
                // Reset the patternIndex if we are at the end of the instruction array
              if (patternIndex >= pattern.Length)
             {
                         patternIndex = 0;
             }

             StartCoroutine(PauseEnemy(data.timeBetween));

               // Process the current instruction in our control data array
                data = pattern[patternIndex];
                speed = data.speed;
                // Find the new range to close vector
                rangeToClose = data.location.transform.position - transform.position;
            
                        
            }   
        

        // In what direction is our waypoint?
        Vector3 normalizedRangeToClose = rangeToClose.normalized;

        // Draw this vector at the position of the waypoint
        Debug.DrawRay(transform.position, normalizedRangeToClose, Color.magenta);

        Vector3 delta = speedDelta * normalizedRangeToClose;

        transform.Translate(delta);
        
       
      
    } 

    IEnumerator PauseEnemy(float waitTime)
    {
        paused = true;
        enemyAnimator.SetBool("Run", false);
        yield return  new WaitForSeconds (waitTime);
        enemyAnimator.SetBool("Run", true);
        paused = false;
    }

    
    
}
