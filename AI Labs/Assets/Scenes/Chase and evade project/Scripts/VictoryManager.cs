using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class VictoryManager : MonoBehaviour
{
    public int victorySceneID;
    public int endSceneID;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Victory()
    {
        SceneManager.LoadScene(victorySceneID);
    }

    public void Defeat()
    {

    }
}
