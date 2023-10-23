using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steering : MonoBehaviour
{
    public enum ChaseBehaviour
    {
        LINEOFSIGHT,
        STEER,
        SMOOTHSTEER
    };

    public ChaseBehaviour chaseType = ChaseBehaviour.SMOOTHSTEER;
    public GameObject target;
    public float speed = 1.0f;
    public float rotationSpeed = 1.0f;
    public Vector3 forwardDirection = new Vector3(0.0f, -1.0f, 0.0f);

    private void Start()
    {
        forwardDirection.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        switch (chaseType)
        {
            case ChaseBehaviour.LINEOFSIGHT:
                LineOfSight();
                break;
            case ChaseBehaviour.STEER:
                Steer();
                break;
            case ChaseBehaviour.SMOOTHSTEER:
                SmoothSteer();
                break;
        }
    }

    void LineOfSight()
    {
        // Find the direction of the player of the player
        // Find the range to close vector
        Vector3 playerPos = target.transform.position;
        Vector3 enemyPos = transform.position;
        Vector3 rangeToClose = playerPos - enemyPos;

        // Get the distance to the target
        float distance = rangeToClose.magnitude;

        // How far do we move each frame.
        // speedDelta is how much we want to move in 1 sec
        // We need to scale this down to how much we move in a frame
        // Time.deltaTime is the time elapsed since the last frame (typically 1/60s)
        float speedDelta = speed * Time.deltaTime;

        // Only move in the direction of the player if our distance
        // to the player is greater than how much we will move in the frame
        if (distance > speedDelta)
        {
            // Get the target direction
            Vector3 targetDirection = rangeToClose.normalized;

            // Draw this vector at the position of the enemy
            Debug.DrawRay(enemyPos, targetDirection, Color.cyan);

            // Multipling the speedDelta by the normalizedRangeToClose
            // will give us our displacement vector.
            Vector3 delta = speedDelta * targetDirection;

            // Tranform our enemy in the direction of our player
            transform.Translate(delta, Space.World);

        }
    }

    void Steer()
    {
        // Find the direction of the player of the player
        // Find the range to close vector
        Vector3 playerPos = target.transform.position;
        Vector3 enemyPos = transform.position;
        Vector3 rangeToClose = playerPos - enemyPos;

        // Get the distance to the target
        float distance = rangeToClose.magnitude;

        // How far do we move each frame.
        // speedDelta is how much we want to move in 1 sec
        // We need to scale this down to how much we move in a frame
        // Time.deltaTime is the time elapsed since the last frame (typically 1/60s)
        float speedDelta = speed * Time.deltaTime;

        // Only move in the direction of the player if our distance
        // to the player is greater than how much we will move in the frame
        if (distance > speedDelta)
        {
            // Get the target direction
            Vector3 targetDirection = rangeToClose.normalized;

            // Draw this vector at the position of the enemy
            Debug.DrawRay(enemyPos, targetDirection, Color.cyan);

            // Multipling the speedDelta by the normalizedRangeToClose
            // will give us our displacement vector.
            Vector3 delta = speedDelta * targetDirection;

            // Tranform our enemy in the direction of our player
            transform.Translate(delta, Space.World);
// Rotate the enemy ship to point at player Option A - Works for 2D and 3D
            Quaternion rotationToTarget = Quaternion.LookRotation(targetDirection, Vector3.forward);
            rotationToTarget.x = 0.0f;
            rotationToTarget.y = 0.0f;
            transform.rotation = rotationToTarget;
            

            // Rotate the enemy ship to point at player Option B - Works for 2D only
            // float angle = Mathf.Atan2(rangeToClose.y, rangeToClose.x) * Mathf.Rad2Deg;
            // transform.rotation = Quaternion.AngleAxis(angle + angleOffset, Vector3.forward);
        }
    }

    void SmoothSteer()
    { 
        // Find the direction of the player of the player
        // Find the range to close vector
        Vector3 playerPos = target.transform.position;
        Vector3 enemyPos = transform.position;
        Vector3 rangeToClose = playerPos - enemyPos;

        // Get the distance to the target
        float distance = rangeToClose.magnitude;

        // How far do we move each frame.
        // speedDelta is how much we want to move in 1 sec
        // We need to scale this down to how much we move in a frame
        // Time.deltaTime is the time elapsed since the last frame (typically 1/60s)
        float speedDelta = speed * Time.deltaTime;

        // Only move in the direction of the player if our distance
        // to the player is greater than how much we will move in the frame
        if (distance > speedDelta)
        {
            // Get the target direction
            Vector3 targetDirection = rangeToClose.normalized;
            // Draw this vector at the position of the enemy
            Debug.DrawRay(enemyPos, targetDirection, Color.cyan);

            // What is our direction
            Vector3 heading = transform.TransformDirection(forwardDirection);

            Vector3 newHeading = Vector3.RotateTowards(heading, targetDirection, rotationSpeed * Time.deltaTime, 0.0f);
            //// Draw this vector at the position of the enemy
            Debug.DrawRay(enemyPos, newHeading, Color.white);

            // Rotate the enemy ship to the new heading
            Quaternion rotationToTarget = Quaternion.LookRotation(newHeading, new Vector3(0, 0, 1));
            rotationToTarget.x = 0.0f;
            rotationToTarget.y = 0.0f;
            transform.rotation = rotationToTarget;

            Vector3 delta = speedDelta * forwardDirection;
            /// Tranform our enemy in the direction of our player
            transform.Translate(delta);
        }
    }        
}



















