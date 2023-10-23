
using System;
using UnityEngine;

public class WaypointPatternMovement2 : MonoBehaviour
{
    [Serializable]
   public struct controlData
    {
         public GameObject location;
         public int speed;
    }
    public controlData[] pattern;


    public float [] speeds;
    private int patternIndex = 0;
    public float speed = 1;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
  /*      // Process the current instruction in our control data array
        GameObject waypoint = pattern[patternIndex];
        speed = speeds [patternIndex];

        // Find the range to close vector
        Vector3 rangeToClose = waypoint.transform.position - transform.position;

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
            waypoint = pattern[patternIndex];

            // Find the new range to close vector
            rangeToClose = waypoint.transform.position - transform.position;
        }

        // In what direction is our waypoint?
        Vector3 normalizedRangeToClose = rangeToClose.normalized;

        // Draw this vector at the position of the waypoint
        Debug.DrawRay(transform.position, normalizedRangeToClose, Color.magenta);

        Vector3 delta = speedDelta * normalizedRangeToClose;

        transform.Translate(delta);*/
    } 
    
}
