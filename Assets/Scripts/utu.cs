using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class utu : MonoBehaviour
{
    //public GameObject enemy;
    
    [SerializeField]
    Transform camera;
    [SerializeField]
    Transform barrel;
    public GameObject sikaku;
    //public GameObject jikan;
    public GameObject bullet;
    public AudioSource audioSource;
    public AudioClip fireSE;
    public float cooltime = 0.1f;
        float time; //射撃用
    public float timer;//制限時間用
    public bool isStart;
    public TextMesh sutato;
    public bananaman bananaman;
    public TextMesh killtext;
    public float fov;

    public TextMesh modeSelectText;
    public enum MODE
    {
        TIMEATTACK,
        HIGHSCOREATTACK,

    }
    MODE mode = MODE.TIMEATTACK;

    public float limittime = 30;
    public int maxEnemyNumber = 50;
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        fov = cam.fieldOfView;
        timer = 0;
        string n = "時間:" + timer.ToString("N0") + "秒";
        //jikan.GetComponent<TextMesh>().text = n;
        time = Time.time;
        isStart = false;
        //action.Stop();
        GetComponent<bananaman>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(1))
        {
            cam.fieldOfView = fov / 1.25f;
        }
        if (Input.GetMouseButtonUp(1))
        {
            cam.fieldOfView = fov;
        }
        if(isStart == true)
        {
            Counter();
            
        }
        Shot();


       


    }

    void Shot()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);
        RaycastHit hitinfo;
        //action.Play();

        if (Input.GetMouseButtonDown(0)&&cooltime<Time.time - time)
        {
            time = Time.time;
            audioSource.PlayOneShot(fireSE);
            Instantiate(bullet, barrel.position, camera.rotation);

            if(Physics.Raycast(ray.origin, ray.direction * 100, out hitinfo))
            {
                if(hitinfo.collider.tag == "enemy")
                {

                    hitinfo.collider.GetComponent<enemyhp>().damage();
                    Debug.Log(hitinfo.collider.GetComponent<enemyhp>().hp);
                    if (hitinfo.collider.GetComponent<enemydeath>() != null)
                    {
                        hitinfo.collider.GetComponent<enemydeath>().banana = bananaman;
                        Debug.Log(bananaman.killnumber);
                        hitinfo.collider.GetComponent<enemydeath>().Onhit();
                    }
                    
                    


                    /*Debug.Log(hitinfo.collider.name);
                    Destroy(hitinfo.collider.gameObject);
                    GetComponent<bananaman>().DestroyEnemy();
                    GetComponent<bananaman>().Instance();
                    */
                }
                if (hitinfo.collider.tag == "button")
                {
                   
                    
                    if(hitinfo.collider.name == "high score button")
                    {
                        mode = MODE.HIGHSCOREATTACK;
                        timer = limittime;
                        string n = "時間:" + timer.ToString("N0") + "秒";
                        //jikan.GetComponent<TextMesh>().text = n;
                        //Debug.Log(mode);
                        killtext.text = "0";

                    }
                    else if (hitinfo.collider.name == "time attack button")
                    {
                        mode = MODE.TIMEATTACK;
                        timer = 0;
                        string n = "時間:" + timer.ToString("N0") + "秒";
                        //jikan.GetComponent<TextMesh>().text = n;
                        //Debug.Log(mode);
                        killtext.text = maxEnemyNumber.ToString();

                    }
                    else
                    {
                        if (isStart)
                        {
                            isStart = false;
                            sutato.text = "スタート";
                            modeSelectText.gameObject.SetActive(true);
                            Destroy(bananaman.nowenemey);
                            bananaman.killnumber = 0;

                            switch (mode)
                            {
                                case MODE.HIGHSCOREATTACK:
                                    timer = limittime;
                                    
                                    break;
                                case MODE TIMEATTACK:
                                    timer = 0;
                                    break;
                            }
                            string n = "時間:" + timer.ToString("N0") + "秒";
                            //jikan.GetComponent<TextMesh>().text = n;

                        }
                        else
                        {
                            modeSelectText.gameObject.SetActive(false);
                            isStart = true;
                            sutato.text = "リセット";
                            bananaman.Instance();
                        }
                    }


                }
            }
        }

    }
    void Counter()
    {
        //stopWatch -= Time.deltaTime;
       
        //string n = "時間:" + timer.ToString("N0") + "秒";
        //jikan.GetComponent<TextMesh>().text = n;
        switch (mode)
        {
            case MODE.HIGHSCOREATTACK:
                timer -= Time.deltaTime;
                
                killtext.text = bananaman.killnumber.ToString("N0");

                if (timer <= 0)
                {
                    isStart = false;
                    sutato.text = "スタート";
                    modeSelectText.gameObject.SetActive(true);
                    Destroy(bananaman.nowenemey);
                    bananaman.killnumber = 0;
                    timer = limittime;
                }
                break;
            case MODE.TIMEATTACK:
                timer += Time.deltaTime;
               
                int nokori = maxEnemyNumber - bananaman.killnumber;
                killtext.text = nokori.ToString("N0");
                if(nokori == 0)
                {
                    isStart = false;
                    sutato.text = "スタート";
                    modeSelectText.gameObject.SetActive(true);
                    Destroy(bananaman.nowenemey);
                    bananaman.killnumber = 0;
                    timer = 0;
                }
                break;

        }
        if (isStart)
        {
            string n = "時間:" + timer.ToString("N0") + "秒";
            //jikan.GetComponent<TextMesh>().text = n;

        }
    }
   
}
