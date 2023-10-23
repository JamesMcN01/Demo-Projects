using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnmeyFSM : MonoBehaviour
{   
    public GameObject player;

    public float speed =1.0f;
    // linka wayoint script
    public StateWaypoint waypoint;

    public enum EnemyState{
        CHASE,
        IDLE,
        PATROL,
        RELOAD,
        SHOOT,
        MELEE,
        SUICIDE,
        HIT,
        DEATH
    };
    // Sets the current state to patrol
    public EnemyState currentState =EnemyState.PATROL;
    // Range that allows a chase to occur
    public float chaseRange;
    // range that allows firing to occur
    public float FireingRange;
    // range that allows melee to occur
    public float MeleeRange;
    // range that allows suicide to occur
    public float SuicideRange;
    // used to keep track of ammo
    public int ammo;
    // used to track if player has bombs
    public int playerBombs;
    //used as range for the enemy to go into idle
    public int waypointRange;
    // used to link chase script
   public EnmeyChase enemyChase;
    // used to link to the enemy health script
   public EnemyHealthManager EnemyHealth;
    // used to link to enemy animator;
    private Animator enemyAnimator;
    // will be used to store values of health  
    int health;
    // right attack radius
    public CircleCollider2D right;
    // left attack radius
    public CircleCollider2D left;
    // links sprite renderer
    SpriteRenderer sprite;
    // links player manager  script
    public OsamaManager playerManager;
    // used to access the explosion object
    public GameObject BigExplosion;
    // links suiceide blast radius 
    public CircleCollider2D suicideRadius;
    // bullet object
    public GameObject bullet;
    // links the reload waypoint script
    public EnemyReload reloadScript;
    // will be used to track the amount of tents destroyed
    public int tent;
    // used to moderate shots fired
     float spawnTime;
    // delay between shots fired
    float timeBetweenSpawn =3;
    // Start is called before the first frame update
    void Start()
    {

        enemyAnimator = gameObject.GetComponent<Animator>();

        sprite = gameObject.GetComponent<SpriteRenderer>();
        // lets tent be equal to the amount of tents destroyed
        tent = playerManager.tentsDestroyed;
       
    }

    // Update is called once per frame
    void Update()
    {
        // tracks current state
        Debug.Log(currentState);
         
        // prevents ammo from going below 0
        if(ammo <0)
        {
            ammo =0;
        }
        // player bombs now logs the amount of tnt the player carries
        playerBombs = playerManager.tnt;


        float speedDelta= speed * Time.deltaTime;
        
        switch(currentState)
        {
            case EnemyState.CHASE:
             Chase();
                break;
            case EnemyState.PATROL:
                Patrol();
                break;
            case EnemyState.IDLE:
                Idle();
                break;
            case EnemyState.SHOOT:
                Shoot();
                break;
            case EnemyState.RELOAD:
                Reload();
                break;
            case EnemyState.MELEE:
                Melee();
                break;
            case EnemyState.SUICIDE:
                Suicide();
                break;
            case EnemyState.HIT:
                Hit();
                break;
            case EnemyState.DEATH:
                Death();
                break;
        }
        CheckDistance();
    }
 public   void CheckDistance()
    {
        Vector3 range = (transform.position - player.transform.position);

        float distance = range.magnitude;

 //       Debug.Log("The player is " + distance + " from the enemy ");


        // lets health be equal to the current enemy health
         health  =EnemyHealth.health;

        // enables Death if player has less then 1 HP
        if(health < 1)
        {
            currentState = EnemyState.DEATH;
        }
        // enabels Melee attack when in melee range and player has no tnt
        else if (distance < MeleeRange &&playerBombs < 1)
        {
            currentState = EnemyState.MELEE;
        }
        // enabels patrol if player is out of range and if the enemy has full ammo
        else if(waypointRange < distance && ammo >=5)
        {
            currentState = EnemyState.PATROL;
        }
        // enables shooting if in firing range and if distance is greater the melee range and if ammo is greater then 0
        else if( distance < FireingRange && ammo > 0 && distance > MeleeRange)
        {
            currentState= EnemyState.SHOOT;
        }
        // chases player if in chase range
        else if (distance < chaseRange && distance > MeleeRange +1)
        {
            currentState = EnemyState.CHASE;
        }
        // if enemy has no ammo and player distance is greater then Chase range
        else if (ammo < 5 && distance > chaseRange)
        {
            currentState = EnemyState.RELOAD;
        }
        // checks if player has bomb and is in suicide range
        else if(playerBombs> 0 && distance < SuicideRange)
        {
            currentState = EnemyState.SUICIDE;
        }
        // enable idle when waypoint range is less then distance and distance is greater then chase range 
        else if(waypointRange > distance && distance > chaseRange &&ammo >=5)
        {
            currentState = EnemyState.IDLE;
        }
        
    }

  public  void OnCollisionEnter2D(Collision2D collision)
    {
        // enables hit state when colliding with player melee colliders
         if(collision.collider.tag == "rightAttack" || collision.collider.tag == "leftAttack" && health > 1 )
        {

            currentState =EnemyState.HIT;
        }
        // enables hit state when colliding with player rocket colliders
        if(collision.collider.tag == "Rocket" && health >1)
        {
            currentState =EnemyState.HIT;
        }

        if (collision.collider.tag =="TNT")
        {
            currentState =EnemyState.HIT;
        }

        // causes damage and explosion on suicide state
        if(currentState == EnemyState.SUICIDE && collision.collider.tag == "Osama")
        {   
            // gets player health script
            OsamaHealth player = collision.gameObject.GetComponent<OsamaHealth>();
            // used to track tents destroyed and increses damage appropriatly
            if(tent <=3)
            {
                player.Hit(35);
            }
            else if(tent > 3 && tent <=5)
            {
                player.Hit(45);
            }
            else if(tent > 6 && tent<9)
            {
                player.Hit(55);
            }
            // spawns Big explosion game object on enemy origion
            Instantiate(BigExplosion, transform.position + new Vector3(0, 0, 0), transform.rotation);
            // destroys enemy
            Destroy(gameObject);
        }
        // when collideing with the "Spawn" Tent it will reset/ refill ammo
        if(collision.collider.tag =="Spawner")
        {
            // used to track tents destroyed and increses ammo appropriatly
             if(tent <=3)
            {
                ammo = 5;
            }
            else if(tent > 3 && tent <=5)
            {
                ammo = 7;
            }
            else if(tent > 6 && tent<9)
            {
                ammo =9;
            }
            
        }
    } 

    void Chase()
    {
        // disables reload script
        reloadScript.enabled =false;
        // enables enemy chase
         enemyChase.enabled=true;
         // disables waypoint
         waypoint.enabled = false;
        // Activates run animation
         enemyAnimator.SetBool("Run" , true);
    }

    void Idle()
    {
         // disables reload script
        reloadScript.enabled =false;
          // disables waypoint
        waypoint.enabled = false;
         // disables enemy chase
        enemyChase.enabled=false;
        //Disables run animation
        enemyAnimator.SetBool("Run" , false);
    }

    void Shoot()
    {
         // disables reload script
        reloadScript.enabled =false;
          // disables waypoint
          waypoint.enabled = false;
           // disables enemy chase
        enemyChase.enabled=false;
        //Disables run animation
        enemyAnimator.SetBool("Run" , false);
        // checks if time is greater then spawn time 
        if(player.transform.position.x < transform.position.x)
        {
            sprite.flipX =true;
        }
        else{
            sprite.flipX =false;
        }
        if(Time.time > spawnTime)
        {
            Debug.Log("Enemy has fired a bullet");
            // calls spawn(spawns bullet)
            Spawn();
            // spawntime increases over time
            spawnTime =Time.time + timeBetweenSpawn;
            // ammo decreases
            ammo--;
        }
        
        
        
    }


    void Reload()
    {
        // enables reload script
        reloadScript.enabled =true;
          // disables waypoint
        waypoint.enabled = false;
         // disables enemy chase
         enemyChase.enabled=false;
         // Activates run animation
          enemyAnimator.SetBool("Run" , true);
    }

    void Suicide()
    {
         // disables reload script
         reloadScript.enabled =false;
           // disables waypoint
         waypoint.enabled = false;
         // disables enemy chase
         enemyChase.enabled=false;
         //Disables run animation
         enemyAnimator.SetBool("Run" , false);
         // Acctivates Suicde Blast
         suicideRadius.enabled =true;
    }

   void Melee()
    {
         // disables reload script
        reloadScript.enabled =false;
          // disables waypoint
          waypoint.enabled = false;
        // disables enemy chase
            enemyChase.enabled=false;
        //Disables run animation
         enemyAnimator.SetBool("Run" , false);
         // triggers Melee animation
          enemyAnimator.SetTrigger("Melee");     

          // activates either right or left melee colliders depending on which way the enemy is facing
            if(sprite.flipX ==false)
            {
                left.enabled =true;
                right.enabled =false;
            //used to turn off colliders
                StartCoroutine(MeleeCoolDown(0.5f));
            }
            else
            {
                right.enabled =true;
                left.enabled =false;
                 //used to turn off colliders
                 StartCoroutine(MeleeCoolDown(0.1f));
            }
           
         
    }

    void Patrol()
    {
         // disables reload script
        reloadScript.enabled =false;
          // enables waypoint
        waypoint.enabled = true;
        // disables enemy chase
        enemyChase.enabled=false;
         // Activates run animation
        enemyAnimator.SetBool("Run" , true);
    }

    void Hit()
    {
         // disables reload script
        reloadScript.enabled =false;
          // disables waypoint
        waypoint.enabled = false;
        // disables enemy chase
        enemyChase.enabled=false;
        //Disables run animation
        enemyAnimator.SetBool("Run" , false);
        if(health > 1)
        {
            // activates hit animation
             enemyAnimator.SetTrigger("Hurt");
        }
        
       
    }
    
    void Death()
    {
         // disables reload script
        reloadScript.enabled =false;
          // disables waypoint
         waypoint.enabled = false;
         // disables enemy chase
        enemyChase.enabled=false;
        //Disables run animation
         enemyAnimator.SetBool("Run" , false);

         enemyAnimator.SetBool("Run" , false);

         
    }

    IEnumerator MeleeCoolDown(float waitTime)
    {
        
        yield return new WaitForSeconds(waitTime);
        // disables melee colliders when time ends
        right.enabled = false;
        left.enabled = false;
    }

    
    // used to spawn bullets 
    void Spawn()
    {
        // activates shooting animation
        enemyAnimator.SetTrigger("RangeAttack");

        if(ammo> 0)
        {
            if(sprite.flipX ==true)
            {
                // spawns ammo on left side if facing left
                Instantiate(bullet,  transform.position + new Vector3(-1, 0, 0), transform.rotation);
                ammo--;

            }
            else{
                // spawns ammo on right side of enemy if facing right
                Instantiate(bullet,  transform.position + new Vector3(1, 0, 0), transform.rotation);
                ammo--;
            }
        }   
    }
}
