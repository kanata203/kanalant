using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyhp : MonoBehaviour
{
    public int hp;
    [SerializeField]
    enemymanager em;
    // Start is called before the first frame update
    void Start()
    {
        hp = 10;
        em = GameObject.FindWithTag("gamemaneger").GetComponent<enemymanager>();
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
            em.enemyspown();
            em.enemycount++;
            Destroy(this.gameObject);
        }
    }
}
