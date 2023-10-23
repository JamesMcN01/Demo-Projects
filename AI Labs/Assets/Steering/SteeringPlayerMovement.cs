using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringPlayerMovement : MonoBehaviour
{
    public float speed = 3.0f;
    public float rotateSpeed = 300.0f;
    public Vector3 forwardDirection = new Vector3(0.0f, 1.0f, 0.0f);
    private Vector3 velocity;

    // Use this for initialization
    void Start ()
    {
        forwardDirection.Normalize();
        velocity = new Vector3(0.0f, 0.0f, 0.0f);
    }
	
	// Update is called once per frame
	void Update ()
    {
        // Draw axes for world space
        Debug.DrawRay(new Vector3(0,0,0), Vector3.up, Color.white);
        Debug.DrawRay(new Vector3(0,0,0), Vector3.right, Color.cyan);

        // Draw player space (local space)
        Vector3 playerUpInWorldSpace = transform.TransformDirection(Vector3.up);
        Debug.DrawRay(transform.position, playerUpInWorldSpace, Color.white);
        //Debug.DrawRay(transform.position, Vector3.up, Color.white);
        Vector3 playerRightInWorldSpace = transform.TransformDirection(Vector3.right);
        Debug.DrawRay(transform.position, playerRightInWorldSpace, Color.cyan);
        //Debug.DrawRay(transform.position, Vector3.right, Color.cyan);

        velocity = new Vector3(0.0f, 0.0f, 0.0f);


        // Move the player ship to where the mouse is clicked on-screen
        if (Input.GetButton("Fire1"))
        {
            Vector3 newpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);  
            transform.position = new Vector3(newpos.x, newpos.y, 0.0f);
        }

        // Move the player based on cursor key inputs
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotateSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            // Tranform our player
            Vector3 velocityls = speed * Time.deltaTime * forwardDirection;
            transform.Translate(velocityls, Space.Self);

            velocity = transform.TransformVector(velocityls);

            //Vector3 forwardWorldSpace = transform.TransformDirection(forwardDirection);
            //transform.Translate(speed * Time.deltaTime * forwardWorldSpace, Space.World);

        }

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            // Tranform our enemy in the direction of our player
            Vector3 velocityls = speed * Time.deltaTime * -forwardDirection;
            transform.Translate(velocityls, Space.Self);

            velocity = transform.TransformVector(velocityls);

            //Vector3 forwardWorldSpace = transform.TransformDirection(forwardDirection);
            //transform.Translate(speed * Time.deltaTime * -forwardWorldSpace, Space.World);
        }


    }

    public Vector3 getVelocity()
    {
        return velocity;
    }

    void OnBecameInvisible()
    {
        Camera cam = Camera.main;

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
