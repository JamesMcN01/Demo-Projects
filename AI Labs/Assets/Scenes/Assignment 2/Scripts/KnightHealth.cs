using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class KnightHealth : MonoBehaviour
{
    
    // health value
    public int health = 100;
    // Health Text
    public Text healthText;
    
    public int waypoint;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Converts health value to the health display
        healthText.text = " : " + health.ToString();
        if(health > 100)
        {
            health =100;
        }
        if(health < 1)
        {
            SceneManager.LoadScene("Assignment2 - Pattern Movement");
        }


    }

    public void Hit(int damage)
    {
        if(health > 0){
            animator.SetTrigger("Hit");
             // takes damage from health
            health = health - damage;

            Debug.Log("Health is reduced to " + health);
        }
      
    }
    
}

