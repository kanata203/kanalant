using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bananaman : MonoBehaviour
{
    public float min_range;
    public float max_range;
    public float angle;
    public GameObject enemy;
    public GameObject enemyDeath;
    public Transform tyuusin;
    Transform enemy_pos;
    public GameObject nowenemey;
    public int killnumber;
    // Start is called before the first frame update
    void Start()
    {
        //Instance();
        killnumber = 0;
    }

    // Update is called once per frame
    
    public void Instance()
    {
        GameObject obj = Instantiate(enemy, tyuusin.position, Quaternion.Euler(0, Random.Range(-angle / 2, angle / 2), 0));
        obj.transform.Translate(obj.transform.forward * Random.Range(min_range, max_range));
        print("ok");
        enemy_pos = obj.transform;
        nowenemey = obj;
        
    }
    public void DestroyEnemy()
    {
        GameObject obj = Instantiate(enemyDeath, enemy_pos.position, enemy_pos.rotation);
        killnumber += 1;
      
    }
}
