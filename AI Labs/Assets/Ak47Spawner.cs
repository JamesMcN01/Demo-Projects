using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ak47Spawner : MonoBehaviour
{
 //   public GameObject ak47;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

 public   void Spawn(GameObject ak47)
    {
        Debug.Log(ak47 + "Was spawned");
        Instantiate(ak47,transform.position + new Vector3(0, -5, 0), transform.rotation);
    }
}
