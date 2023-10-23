using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class KillCounter : MonoBehaviour
{
    public int kills = 0;
    public Text killText;

    public MainMenu mainMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        killText.text = "Kills: " +  kills.ToString();
        {
            if (kills > 7)
            {
                // win screen activater
                mainMenu.Victory();
            }
        }
    }

    public void Death(int death)
    {
        kills = kills+ death;
        Debug.Log("Enemy was killed");
    }
}
