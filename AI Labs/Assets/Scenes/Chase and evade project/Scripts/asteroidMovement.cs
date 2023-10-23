using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroidMovement : MonoBehaviour
{
    // stores thrust
    public float maxThrust; 
    // stores torque
    public float maxTorque;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        
        Vector2 thrust = new Vector2(Random.Range (-maxThrust,maxThrust), Random.Range(-maxThrust,maxThrust));

        float torque = Random.Range( - maxThrust, maxTorque);
        // applys thrust to the rigidbody
        rb.AddForce (thrust);
       // applus torque to the rigidbody
        rb.AddTorque (torque);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
     void OnBecameInvisible()
    {
        Camera cam = Camera.main;
        // checks to see if the position is not the same as the camerea view and repositions it at a random vector to the camerea
        if (cam != null)
        {
            Vector3 viewportPosition = cam.WorldToViewportPoint(transform.position);

            Vector3 newPosition = transform.position;

            if (viewportPosition.x > 1 || viewportPosition.x < 0)
            {
                newPosition.x = -newPosition.x;
            }

            if (viewportPosition.y > 1 || viewportPosition.y < 0)
            {
                newPosition.y = -newPosition.y;
            }

            transform.position = newPosition;
        }
    }
}
