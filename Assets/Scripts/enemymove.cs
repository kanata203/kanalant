using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class enemymove : MonoBehaviour
{
    Ray ray;
   RaycastHit hit;
    Vector3 direction;
    public float distance = 10;
    public int raycount = 6;
    public float enemyFOV = 180;
    public float wall_distance;
    public Vector3 player_pos;
    public Vector3 wall_pos;
    float enemy_foward = 30;
    Rigidbody rb;
    Vector3 enemy_advance;
    public float enemy_speed;
    // Start is called before the first frame update
    void Start()
    {
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
        enemy_advance = transform.forward;
        rb.velocity = new Vector3(enemy_advance.x * enemy_speed, rb.velocity.y, enemy_advance.z * enemy_speed);
        enemyFOV %= 360;
        Vector3 vector = transform.position;//npcの座標
        float anglestep = enemyFOV / raycount;
       
        bool hitplayer = false;
        float start_angle = 90 - enemyFOV / 2;
        for (int i = 0; i < raycount+1; i++) 
        {
            //float angle = anglestep * i + start_angle;
            float angle = i * (360 / raycount);
            Vector3 dic = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad),0f,Mathf.Sin(angle * Mathf.Deg2Rad));
            ray = new Ray(vector, dic);
            Debug.DrawRay(vector, ray.direction * distance, Color.red);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit,distance))//レイが何かに当たったら
            {
               
                if(hit.collider.tag == "player" && !hitplayer)
                {
                    player_pos = hit.collider.transform.position;
                    hitplayer = true;
                }
                if(hit.collider.tag == "wall" && !hitplayer)
                {
                    wall_pos = hit.collider.transform.position;
                   
                    Debug.Log(wall_pos);
                }
                
                
            }
            
        }
        //正面の壁との距離を測る
        Vector3 walldic = new Vector3(0, 0, 1);//90度の向き
        float dist = Vector3.Distance(wall_pos , transform.position);
        Debug.Log(dist);
       if (dist < distance)
        {
            
            direction = (wall_pos - transform.position).normalized;
            float angle = Vector3.Angle(enemy_advance, direction);
            if(angle <= enemy_foward)
            {
                transform.Rotate(new Vector3(0, +9, 0));
                Debug.Log("tomare");
                 
            }
        }
    }
}
