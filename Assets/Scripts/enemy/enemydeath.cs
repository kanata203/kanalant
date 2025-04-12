using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemydeath : MonoBehaviour
{
    bool hit = true;
    public bananaman banana;
    public void Onhit()
    {
        if (hit)
        {
            hit = false;
            banana.DestroyEnemy();
            banana.Instance();
            Destroy(this.gameObject);
            
        }

    }
}
