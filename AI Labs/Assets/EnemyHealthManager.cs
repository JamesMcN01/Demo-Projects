using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    // health
    public int health;

    public EnmeyChase enemyChase;

    public EnmeyFSM enemyFsm;

    private Animator enemyAnimator;
    // player manager
   public OsamaManager playerManager;
    
    // will bwe used t0 track tents destroyed
     int tent;
    // Start is called before the first frame update
    void Start()
    {
        enemyAnimator = gameObject.GetComponent<Animator>();

        health = 100;
        // tracks tents destroyed
        tent = playerManager.tentsDestroyed;
        
    }

    // Update is called once per frame
    void Update()
    {
       
        
    }

    void Hit(int damage)
    {   
        if(health >= 1)
        {
            // stops player moving
             enemyChase.enabled = false;
             // deal damage according the means of attack 
            health -= damage;
        Debug.Log("The enemy was hit  and their health is now" + health);
        }
       
        if(health < 1 )
        {
           // calls dead method when hp less then 1
             Debug.Log("Dead is called");
            Dead();
        }
       
    }

    void Dead()
    {   
      /*  
        enemyFsm.enabled =false;
        // disables chase
          enemyChase.enabled = false;*/

          // triggers death animation
            enemyAnimator.SetTrigger("Death");
         // calls despawn times
           StartCoroutine(Despawn(6f));
           
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "rightAttack" || collision.collider.tag == "leftAttack"  && health> 0)
        {
            // damage dealt when stabed with either player collider 
            Hit(10);
        }
        if(collision.collider.tag == "Rocket" )
        {
            // damage dealt when hit by rocket
            Hit(35);
        }
        // on collision with spawn tent health will be adjusted according to tents destroyed
        if(collision.collider.tag =="Spawner")
        {
             if(tent <=3)
            {
              health =100;
            }
            else if(tent > 3 && tent <=5)
            {
                health =120;
            }
            else if(tent > 6 && tent<9)
            {
                health =150;
            }
        }
    }
    // Used to time until enemy body is destroyed 
    IEnumerator Despawn(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
       Destroy(gameObject);
    }
    
}
