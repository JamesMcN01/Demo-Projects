using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OsamaHealth : MonoBehaviour
{
    public int health = 100;

    public Text healthText;
    Animator animator;

    public LevelManager levelManager;
    // Start is called before the first frame update
    void Start()
    {
        animator =gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = " : " + health.ToString();

        if(health > 100)
        {
            health = 100;
            Debug.Log("You are now at full health");

            
            Debug.Log(health);
        }
        if(health<1)
        {
            levelManager.Defeat();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "EnemyLeft"|| collision.collider.tag == "EnemyRight")
        {
            Hit(15);
        }
        if(collision.collider.tag == "Bullet")
        {
            Hit(5);
        }
        if(collision.collider.tag == "MedKit")
        {
            GameObject medKit = collision.gameObject;

            if(health == 100)
            {
                Debug.Log("Player at max HP");
            }
            else{

            Debug.Log("Osama picked up a MedKit");

            int heal = medKit.GetComponent<MedKitBehavior>().healthRestore;

            health +=heal;
            
            Destroy(medKit);

            }

        }

    }

   public void Hit(int damage)
    {
        health -=damage;
        animator.SetTrigger("Hurt");
    }


}
