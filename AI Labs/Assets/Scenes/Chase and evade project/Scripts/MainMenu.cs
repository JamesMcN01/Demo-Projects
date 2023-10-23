using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public int startSceneID =1;
    public int MenuID =0;
    public int DefeatID =3;
    public int victorySceneID =2;
    public int wayPointId = 4;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

      public void StartGame()
    {
        SceneManager.LoadScene(startSceneID);
    }

    public void Menu()
    {
        SceneManager.LoadScene(MenuID);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void Victory()
    {
        SceneManager.LoadScene(victorySceneID);
    }

    public void Defeat()
    {
        SceneManager.LoadScene(DefeatID);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(wayPointId);
    }
}
