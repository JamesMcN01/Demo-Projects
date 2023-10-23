using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChestBehaviour : MonoBehaviour
{
    // gold value
    public int gold;
    public knightManager knight;
    int keys;
    // chest value
    public int chestValue;
    // referes to the text that counts how much gold is stored
    public Text goldText;
    // access the chest animator
    private Animator chestAnimator;
    // Start is called before the first frame update
    void Start()
    {
        chestAnimator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // converts chest value into the gold display
       
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Knight" )
        {
            chestAnimator.SetTrigger("Open");
        }
       
    }


        public void ChestOpen()
    {
          
            gold += chestValue;
          StartCoroutine(Despawn(5f));
         
    }
    IEnumerator Despawn(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
    //    Destroy(gameObject);
    }
}
