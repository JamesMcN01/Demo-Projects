using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{

    public int StartId =5;

    public int defeat =6;

    public int victory =7;

    public int menu =8;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        SceneManager.LoadScene(StartId);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Menu()
    {
         SceneManager.LoadScene(menu);
    }

    public void Defeat()
    {
         SceneManager.LoadScene(defeat);
    }

    public void Victory()
    {
         SceneManager.LoadScene(victory);
    }
}
