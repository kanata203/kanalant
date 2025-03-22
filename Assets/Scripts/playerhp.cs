using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerhp : MonoBehaviour
{
    [SerializeField]
    int hp;
    public int maxhp;
    [SerializeField]
    GameObject gameover;
    // Start is called before the first frame update
    void Start()
    {
        hp = maxhp;
        gameover.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            die();
        }
    }
    public void takedamege(int damage)
    {
        hp -= damage;
    }
    void die() 
    {
        gameover.SetActive(true);
        this.gameObject.GetComponent<ugoku>().enabled = false;
        this.gameObject.GetComponent<muki>().enabled = false;
        this.gameObject.GetComponent<utu>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
