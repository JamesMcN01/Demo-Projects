using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNTBeahviour : MonoBehaviour
{
    public GameObject Explosion;
    public CapsuleCollider2D TNT;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Fuse(6f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BlowUp()
    {
        TNT.enabled=true;
        Spawn();
        StartCoroutine(Despawn(0.03f));
    }

    IEnumerator Despawn(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }
    
    IEnumerator Fuse(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        BlowUp();
    }

     //test
     void OnCollisionEnter2D(Collision2D collision)
     {
         if(collision.collider.tag=="Spawner")
        {
            Debug.Log(collision +"has been destroyed");
            Destroy(collision.gameObject);
        }
     }
    void Spawn()
    {
        Instantiate(Explosion, transform.position + new Vector3(0, 0, 0), transform.rotation);
    }  
    
}
