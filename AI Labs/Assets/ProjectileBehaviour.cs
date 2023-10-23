using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    SpriteRenderer rocketSprite;


    public GameObject Explosion;

    public int Speed  = 5;
    // Start is called before the first frame update
    void Start()
    {
        rocketSprite =gameObject.GetComponent<SpriteRenderer>();

        StartCoroutine(DeSpawn(6));
    }

    // Update is called once per frame
    void Update()
    {


       
       
        if(rocketSprite.flipX == true)
        {
          transform.position += transform.right *Time.deltaTime *Speed;
        }
        else
        {
            
              transform.position += -transform.right *Time.deltaTime *Speed;
        } 
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag =="Bullet"||collision.collider.tag == "Enemy")
        {   
            if(collision.collider.tag =="Bullet")
            {
                Destroy(collision.gameObject);
            }
            Spawn();
            Debug.Log("Collision");
            Destroy(gameObject);
        }
       
    }

    void Despawn()
    {
        Destroy(gameObject);
    }

    IEnumerator DeSpawn(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Despawn();
    }

    void Spawn()
    {
        Instantiate(Explosion, transform.position + new Vector3(0, 0, 0), transform.rotation);
    }
}
