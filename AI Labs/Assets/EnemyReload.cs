using System;
using UnityEngine;

public class EnemyReload : MonoBehaviour
{

      private float speed =0;[Serializable]
   public struct ReloadPoint
   {
       public GameObject location;
       public int speed;
   }
    
    public ReloadPoint[] pattern;

    private int patternIndex = 0;

    private SpriteRenderer enemySprite;
    // Start is called before the first frame update
    void Start()
    {
         enemySprite = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
            // Process the current instruction in our control data array
        ReloadPoint data = pattern[patternIndex];
        speed = data.speed; 

        // Find the range to close vector
        Vector3 rangeToClose = data.location.transform.position - transform.position;
        if(rangeToClose.x < 0)
        {
            enemySprite.flipX = true;
        }
        else{
            enemySprite.flipX = false;
        }
        // Draw this vector at the position of the enemy
        Debug.DrawRay(transform.position, rangeToClose, Color.cyan);

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

            // Process the current instruction in our control data array
            data = pattern[patternIndex];

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
    
}
