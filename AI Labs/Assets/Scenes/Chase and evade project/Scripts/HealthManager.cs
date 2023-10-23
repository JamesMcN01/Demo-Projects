using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class HealthManager : MonoBehaviour
{
    // health value
    public int health = 100;

    public Text healthText;

    public MainMenu mainMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "HP: " + health.ToString();
        if(health > 100)
        {
            health =100;
        }
    }

    public void Hit(int damage)
    {
        if(health > 0){

             // takes damage from health
            health = health - damage;

            Debug.Log("Health is reduced to " + health);
        }
        else
        {
            // if health is zero then player is destroyed
            

            mainMenu.Defeat();
        }
    }
    public void Repair(int repair)
    {
        health = health + repair;

        

    } 
}
