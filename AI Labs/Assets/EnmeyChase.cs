using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnmeyChase : MonoBehaviour
{
    public GameObject target;

    private Animator enemyAnimator;

    private SpriteRenderer enemySprite;

    public int speed;
    //links player manager
    public OsamaManager player;
    // will be used to store how many tents destroyed
    public int HomeBase;
    // Start is called before the first frame update
    void Start()
    {
      enemyAnimator = gameObject.GetComponent<Animator>();

      enemySprite = gameObject.GetComponent<SpriteRenderer>();
        // allows homeBase access to the amount of tents destroyed
      HomeBase = player.tentsDestroyed;
    }

    // Update is called once per frame
    void Update()
    {
        // increases player speed if tents are being destroyed
        if(HomeBase > 2 && HomeBase< 6)
        {
            speed =5;
        }
        else if(HomeBase >= 6 && HomeBase<7)
        {
            speed = 6;
        }
        else if(HomeBase>=7  && HomeBase <9)
        {
            speed =7;
        }
         float speedDelta = speed * Time.deltaTime;

        Vector3 newPosition = EnemyMoveTowards(transform.position, target.transform.position, speedDelta);

        transform.position = newPosition;
    }

    Vector3 EnemyMoveTowards(Vector3 predatorPosition,Vector3 target,  float maxDistanceDelta)
    {
        Vector3 rangeToClose = target - predatorPosition;

        if(rangeToClose.x < 0)
        {
            enemySprite.flipX = true;
        }
        else{
            enemySprite.flipX = false;
        }
        Vector3 normRangeToClose = rangeToClose.normalized;

        Vector3 newPosition = predatorPosition + normRangeToClose * maxDistanceDelta;
        
        return newPosition;
    }
}
