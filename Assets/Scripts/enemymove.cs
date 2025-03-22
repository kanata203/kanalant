using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using UnityEngine.Timeline;
using System.Threading;

public class enemymove : MonoBehaviour
{
    Ray ray;
   RaycastHit hit;
   
    public float distance = 1;
    public int raycount = 6;
    public float enemyFOV = 180;
    
    public Vector3 player_pos;
    public Vector3 wall_pos;
   
    Rigidbody rb;
    Vector3 enemy_advance;
    public float enemy_speed;
    float time;
    [SerializeField]
    GameObject bullet;
    [SerializeField]
    Transform barrel;
    [SerializeField]
    bool isattack;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        rb = GetComponent<Rigidbody>();
    }

    /*npcにさせること
     * プレイヤーへの攻撃　最優先　プレイヤーを検知したらその方向に向いて攻撃
     * 移動　基本前進
     * 停止　正面の壁との距離が2.5以下なら停止
     * 回転　停止したら回転、プレイヤーを見つけた時以外は四方向に回転、常に右回転
     */
    void Update()
    {
        time += Time.deltaTime;
        enemy_advance = transform.forward;
        /*if (isattack)
        {
            rb.velocity = enemy_advance.normalized * enemy_speed;
        }*/
        
        float minwalldist = 1000f;
        Vector3 vector = transform.position;//npcの座標
        float anglestep = enemyFOV / raycount;
       
        bool hitplayer = false;
        float start_angle = transform.eulerAngles.y - enemyFOV / 2;
        for (int i = 0; i < raycount+1; i++) 
        {
            //float angle = anglestep * i + start_angle;
            float angle = start_angle - i * anglestep;
            Vector3 dic = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad),0f,Mathf.Sin(angle * Mathf.Deg2Rad));
            ray = new Ray(vector, dic);
            Debug.DrawRay(vector, ray.direction * distance, Color.red);
            
            if(Physics.Raycast(ray,out hit,distance))//レイが何かに当たったら
            {
               
                if(hit.collider.tag == "player" && !hitplayer)
                {
                    player_pos = hit.collider.transform.position;
                    hitplayer = true;
                }
                if(hit.collider.tag == "wall")
                {
                    float dist = Vector3.Distance(hit.point, transform.position);
                    if(dist < minwalldist)
                    {
                        minwalldist = dist;
                        wall_pos = hit.point;

                    }

                    Debug.Log(wall_pos);
                }
                
                
            }
            
        }
        
        if(hitplayer == true)
        {
            isattack = true;
            transform.LookAt(player_pos);
            rb.velocity = Vector3.zero;
            if (time > 0.2f) 
            {
                enemyattack();
                time = 0;
            }
            
        }
        else
        {
            isattack = false;
            Debug.Log(isattack);
            rb.velocity = enemy_advance.normalized * enemy_speed;
        }
       float walldist = Vector3.Distance(wall_pos, transform.position);
        Debug.Log(walldist);
       if (walldist < 2.5f)
        {
            
            Vector3 walldir = (wall_pos - transform.position).normalized;
            float angle = Vector3.Angle(transform.forward, walldir);
            if(angle >= -100 || angle <= 100)
            {
                if(time >= 1f)
                {
                    float rota;
                    do {rota = UnityEngine.Random.Range(-125, 125); } while (Mathf.Abs(rota) < 20f);
                    transform.Rotate(new Vector3(0, rota ,0));
                    Debug.Log("tomare");
                    time = 0;
                }
                
                 
            }
        }
    }
    void enemyattack()
    {
        Instantiate(bullet, barrel.position,barrel.rotation);
    }
}
