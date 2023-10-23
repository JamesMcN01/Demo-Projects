using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreCounter : MonoBehaviour



{
    // used to acces the text object
    public Text scoreText;
    // used to store the score total
    public float scoreTotal;
    // used to declare the amount of score that is increased per seconed
    public float scoreIncreasePerSecond;

    // Start is called before the first frame update
    void Start()
    {
        // sets the total score to 0
        scoreTotal = 0f;
        
    }

    // Update is called once per frame
    void Update()
    {
        // converts the text to the current score
        scoreText.text = "Score: " + (int)scoreTotal;
        // used to increase the score by the set amount per second
        scoreTotal += scoreIncreasePerSecond * Time.deltaTime;
    }

   public void PLayerHit(int pointReduce)
    {
        Debug.Log("The Score has been reduced by " + pointReduce);
        scoreTotal = scoreTotal - pointReduce;
    }
}
