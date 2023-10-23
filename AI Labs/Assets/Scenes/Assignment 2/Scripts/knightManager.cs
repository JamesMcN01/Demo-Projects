using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class knightManager : MonoBehaviour
{
    public ChestBehaviour chest;

    public int gold;

    public CircleCollider2D rightAttack;

    public CircleCollider2D leftAttack;

    public float speed;

    public Text keyText;

    public Text goldText;

    private Rigidbody2D rb;

    private Animator animator;

    private SpriteRenderer sprite;

    

    public int Keys =0;
    // Start is called before the first frame update
    void Start()
    {

        rb = gameObject.GetComponent<Rigidbody2D>();

        animator = gameObject.GetComponent<Animator>();

        sprite = gameObject.GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        gold = chest.gold;
         Debug.Log("Gold: " + gold);
          Vector3 input = new Vector3(0.0f,0.0f,0.0f);

            if(gold == 500)
            {
                SceneManager.LoadScene("VictoryWaypoint");
            }
        // Move the player based on cursor key inputs
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            input += Vector3.left;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            input += Vector3.right;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            input += Vector3.up;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            input += Vector3.down;
        }

        Vector3 velocity = input.normalized * speed * Time.deltaTime;

        // Could replace the above with the following code
        //Vector3 velocity = Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

        transform.position += velocity;

        if(velocity != Vector3.zero)
        {
            animator.SetBool("Run", true);

            if (velocity.x < 0)
            {
                sprite.flipX = true;
            }
            else{
                sprite.flipX = false;
            }
        }
        else{
            animator.SetBool("Run" , false);
        }

        keyText.text = " X " + Keys;

        goldText.text = " X " + gold.ToString();
    }

    void OnFire()
    {
        animator.SetTrigger("Attack");

        if(sprite.flipX == true)
        {
            leftAttack.enabled = true;
             rightAttack.enabled = false;

        }
        else
        {
            rightAttack.enabled = true;
            leftAttack.enabled = false;
        }

        StartCoroutine(AttackStop(1f));
    }

  public  void OnCollisionEnter2D (Collision2D collision)
    {

        if (collision.collider.tag == "Key")
        {
            Keys ++;

        }
        if (collision.collider.tag == "Chest" && Keys > 0)
        {
            Keys --;
             chest.ChestOpen();
              Debug.Log("Gold: " + gold);
        }
    }

    IEnumerator AttackStop(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        rightAttack.enabled = false;
         leftAttack.enabled = false;
    }
  
}
