using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombBehaviour : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
         animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Enemy")
        {
            GameObject enemy = collision.gameObject;

          //  animator.SetTrigger("Explode");
            Destroy(enemy);
            
        }
    } 
}
