using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyhp : MonoBehaviour
{
    public int hp;
    // Start is called before the first frame update
    void Start()
    {
        hp = 10;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   public void damage()
    {
        hp -= 1;

        if(hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
