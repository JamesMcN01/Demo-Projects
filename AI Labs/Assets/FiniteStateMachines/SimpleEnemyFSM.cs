
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyFSM : MonoBehaviour
{
    public GameObject target;
    public float speed = 1.0f;

    public StateWaypoint waypoint;

    public enum EnemyState
    {
        CHASE,
        IDLE,
        PATTERN
    };

    public EnemyState currentState = EnemyState.CHASE;
// used as a range for the enemy to chase the player
 public float chaserange;
// used as a range for the enemy to go idle
 public float waypointRange;

    // Update is called once per frame
    void Update()
    {

        switch (currentState)
        {
            case EnemyState.CHASE:
                BasicChase();
                break;
            case EnemyState.IDLE:
                Idle();
                break;
            case EnemyState.PATTERN:
                pattern();
                break;

        }
        CheckDistance();
    }

    void BasicChase()
    {
        // disables the patteren movement

        waypoint.enabled =false;

        float speedDelta  = speed * Time.deltaTime;

        if (Mathf.Abs(transform.position.x - target.transform.position.x) > speedDelta)
        {
            if (transform.position.x > target.transform.position.x)
            {
                float deltax = -speedDelta;
                transform.Translate(new Vector3(deltax, 0, 0));
            }
            else if (transform.position.x < target.transform.position.x)
            {
                float deltax = speedDelta;
                transform.Translate(new Vector3(deltax, 0, 0));
            }
        }

        if (Mathf.Abs(transform.position.y - target.transform.position.y) > speedDelta)
        {
            if (transform.position.y > target.transform.position.y)
            {
                float deltay = -speedDelta;
                transform.Translate(new Vector3(0, deltay, 0));
            }
            else if (transform.position.y < target.transform.position.y)
            {
                float deltay = speedDelta;
                transform.Translate(new Vector3(0, deltay, 0));
            }
        }
    }

    void Idle()
    {
        // disables the pattern movement
        waypoint.enabled = false;
    }

    void CheckDistance()
    {
        Vector3 range = (transform.position- target.transform.position);
        float distance = range.magnitude;
        Debug.Log(distance);
        // checks distance from player with chaserange
        if(distance <chaserange  )
        {
            // changes the state to chase
            currentState = EnemyState.CHASE;
        }
        else if ( chaserange < distance )
        {
            // changes the state to pattern
            currentState = EnemyState.PATTERN;
        }
         if (waypointRange < distance)
        {
            // changes the state to idle
           currentState = EnemyState.IDLE;
        }
    }

    void pattern()
    {
        // disables waypoint script
        waypoint.enabled =true;
    }
}






