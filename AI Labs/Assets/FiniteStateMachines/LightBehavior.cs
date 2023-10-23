using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBehavior : MonoBehaviour {

    public enum LightState
    {
        ON,
        OFF
    };

    public LightState state = LightState.OFF;
    public Sprite onSprite;
    public Sprite offSprite;
    public Sprite Sprite;

    // Use this for initialization
    void Start ()
    {
        if (state == LightState.OFF)
        {
            // Init off state
            GetComponent<SpriteRenderer>().sprite = offSprite;
        }
        else
        {
            // Init on state
            GetComponent<SpriteRenderer>().sprite = onSprite;
            StartCoroutine("AutoLightOff");
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        // What state are we in?
        switch(state)
        {
            case LightState.OFF:
                // Process Light State OFF
                GetComponent<SpriteRenderer>().sprite = offSprite;

                // Then check for transition
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    state = LightState.ON;
                    StartCoroutine("AutoLightOff");
                }
                break;

            case LightState.ON:
                // Process Light State ON
                GetComponent<SpriteRenderer>().sprite = onSprite;

                // Then check for transition
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    state = LightState.OFF;
                }
                break;
        }
    }

    IEnumerator AutoLightOff()
    {
        yield return new WaitForSeconds(5.0f);
        state = LightState.OFF;
    }
}

