using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLookSteering : MonoBehaviour
{
    public float speed = 3.0f;
    public float rotateSpeed = 300.0f;
    public Vector3 forwardDirection = new Vector3(0.0f, 1.0f, 0.0f);

    // Use this for initialization
    void Start ()
    {
        forwardDirection.Normalize();
    }
	
	// Update is called once per frame
	void Update ()
    {
        // Rotate player based on where the mouse cursor is - Option A
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mousePositionDirection = mousePosition - transform.position;



        Debug.DrawRay(transform.position, mousePositionDirection, Color.cyan);

        mousePositionDirection.Normalize();
        transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePositionDirection);

        // Rotate player based on mouse movement - Option B
        // Get the mouse delta. This is not in the range -1...1
        //float h = rotateSpeed * Time.deltaTime * Input.GetAxisRaw("Mouse X"); // Input.GetAxis("Mouse X");
        //transform.Rotate(Vector3.forward * h);


        // Move the player based on cursor key inputs
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            // Tranform our enemy in the direction of our player
            transform.Translate(speed * Time.deltaTime * forwardDirection);
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            // Tranform our enemy in the direction of our player
            transform.Translate(speed * Time.deltaTime * -forwardDirection);
        }
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
