using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{

    public GameObject target;

    public int chaseRange;
    public int speed;

  
    // Start is called before the first frame update
    void Start()
    {
   //     StartCoroutine(Despawn(4f));

       
    }

    // Update is called once per frame
    void Update()
    {
        
          float speedDelta = speed * Time.deltaTime;
        
         Vector3 newPosition = EnemyMoveTowards(transform.position, target.transform.position, speedDelta);

        transform.position = newPosition;
    }

       Vector3 EnemyMoveTowards(Vector3 bulletPosition,Vector3 target,  float maxDistanceDelta)
    {
        Vector3 rangeToClose = target - bulletPosition;

        float distance =rangeToClose.magnitude;

        Vector3 normRangeToClose = rangeToClose.normalized;
      
      
      if(distance > chaseRange && distance < chaseRange + 2)
      {
         Debug.Log("The bullet was Destroyed");
            Destroy(gameObject);
            return bulletPosition;
      }
      
      if(distance <= chaseRange)
      {
      

        Vector3 newPosition = bulletPosition + normRangeToClose * maxDistanceDelta;
        
        return newPosition;
      }
      else{
        return bulletPosition;
      }
       
    }

    /*
    IEnumerator Despawn(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }*/

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Osama")
        {
            Destroy(gameObject);
        }
    }
}
