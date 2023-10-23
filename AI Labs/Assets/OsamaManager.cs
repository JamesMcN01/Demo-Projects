using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OsamaManager : MonoBehaviour
{
    private Animator OsamaAnimater;
    // right melee attack radius
    public CircleCollider2D knifeAttackRight;
    // left melee attack radius
    public CircleCollider2D knifeAttackLeft;
    // gets tnt object
    public GameObject TNT;
    /// gets rocket object
    public GameObject rocket;
    // player sprite
    private SpriteRenderer OsamaSprite;

    public float speed =5;
    // links to ui text
    public Text rocketCounter;
    // links to ui text
    public Text TNTCounter;
    // links to ui text
    public Text TentCounter;
    //ammo stored
    public int ammo =3;
    // tnt stored
    public int tnt=0;
    // gets rocket sprite
    private SpriteRenderer rocketSprite;
    // checks if in contact with tent
    bool inTentRange;
    // tracks tents destroyed
    public int tentsDestroyed;
    // LINKS LEVEL scripts
    public LevelManager levelManager;

   
    // Start is called before the first frame update
    void Start()
    {
        OsamaAnimater = gameObject.GetComponent<Animator>();

        OsamaSprite = gameObject.GetComponent<SpriteRenderer>();
        // gets rocket sprite
        rocketSprite = rocket.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {   
        // activates victory screen
        if(tentsDestroyed ==9)
        {
            levelManager.Victory();
        }

        // updates ui
        TentCounter.text = " X " + tentsDestroyed.ToString();
         // updates ui
        TNTCounter.text = " X " + tnt.ToString();
        // updates ui
        rocketCounter.text = " X " + ammo.ToString();
        // prevents ammo going over and below 0
        if(ammo >5)
        {
                ammo =5;
        }
        else if(ammo<0)
        {
            ammo =0;
        }
         
          Vector3 input = new Vector3(0.0f,0.0f,0.0f);

        // Move the player based on cursor key inputs
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            input += Vector3.left;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            input += Vector3.right;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            input += Vector3.up;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            input += Vector3.down;
        }

        Vector3 velocity = input.normalized * speed * Time.deltaTime;

        

        transform.position += velocity;


        // activates run animation when moving and flips sprite when moving in either direction
        if(velocity != Vector3.zero)
        {
            OsamaAnimater.SetBool("Run", true);

            if (velocity.x < 0)
            {
                OsamaSprite.flipX = true;
            }
            else{
                OsamaSprite.flipX = false;
            }
        }
        else{
            OsamaAnimater.SetBool("Run" , false);
        }

       
    }

    void OnFire()
    { 
          
        if(ammo > 0)
        {
            // Calls spawn method, stops player runing and enables shoot animation

              OsamaAnimater.SetBool("Run" , false);
              OsamaAnimater.SetTrigger("RangedAttack");
                ammo --;
              Spawn();
        }
        else
        {
            Debug.Log("The player has no ammo");
        }
      
    }

    void Spawn()
    {
        
        // spawns the rocket at the  player , the rocket will be facing the direction the player is  
        if(OsamaSprite.flipX == false)
        {  
            rocketSprite.flipX = true;
             Instantiate(rocket, transform.position + new Vector3(1, 0, 0), transform.rotation);

            
        }
        else
        {
           rocketSprite.flipX = false;
            Instantiate(rocket, transform.position + new Vector3(-2, 0, 0), transform.rotation);

            
        }
       
    }
    void OnAttack()
    {
        // enables melee 
        OsamaAnimater.SetTrigger("Melee");
        // enables melee colliders where facing
        if(OsamaSprite.flipX == false)
        {
            knifeAttackRight.enabled = true;
             StartCoroutine(MeleeCooldown(1f));
        }
        else
        {
             knifeAttackLeft.enabled = true;
              StartCoroutine(MeleeCooldown(1f));
        }
    }
    // disables melee coliders after timer ends
    IEnumerator MeleeCooldown(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        knifeAttackRight.enabled = false;
        knifeAttackLeft.enabled = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "AmmoBox")
        {
            GameObject ammoBox = collision.gameObject;
            // wont pick ammo up
            if(ammo > 4)
            {
                Debug.Log("Aready at ammo Capacity");
            }
            //picks up ammo
            else
            {
            Debug.Log("Osama picked up more ammo");

            int ammoSupply = ammoBox.GetComponent<AmmoBoxBehaviour>().ammo;
            // adds to ammo supply
            ammo +=ammoSupply;
            //destroys ammo box
            Destroy(ammoBox);
            }
          
        }
        if(collision.collider.tag == "TNTCrate")
        {
            GameObject TNTCrate = collision.gameObject;
            // wont pick up tnt 
            if(tnt > 0)
            {
                Debug.Log("Osama can not hold any more Tnt");
            }
            // picks up tnt
            else{
                  Debug.Log("Osama Picked up an explosive");
            
            int tntSupply = TNTCrate.GetComponent<CrateBehaviour>().Tnt;
            // adds tnt 
            tnt += tntSupply;
            //destroys crate
            Destroy(TNTCrate);
            }
          
        }
        if(collision.collider.tag =="Spawner")
        {   
            // enables bool on collision with tent
            inTentRange =true;
        }
    }

    void OnPlant()
    {
        
        //add bool to check in contact with Objective 
        if(tnt> 0)
        {
            if(inTentRange ==true)
            {
                // tnt value decreas
                   tnt--;
            // spawns tnt 
            Instantiate(TNT, transform.position + new Vector3(0, 0, 0), transform.rotation);
           // sets bool false
            inTentRange =false;
            // increase variable
            tentsDestroyed++;
            }
            else{
                Debug.Log("Not in range of a tent");
            }
         }
    }
}   
