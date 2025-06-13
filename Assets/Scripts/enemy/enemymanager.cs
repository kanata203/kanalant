using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class enemymanager : MonoBehaviour
{
    [SerializeField]
    GameObject enemy;
    public int enemycount;
    [SerializeField]
    int clearcount;
    gamemaneger gm;
    static gamemaneger instance;
    // Start is called before the first frame update
    void Start()
    {
        enemyspown();
        if (instance == null)
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            
            
        }
        enemycount = 0;
        gm = GameObject.FindWithTag("gamemaneger").GetComponent<gamemaneger>();
        clearcount = gm.level;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemycount > clearcount)
        {
            gm.level++;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    public void enemyspown()
    {
        float x = Random.Range(-2.5f,47.5f);
        float z = Random.Range(-2.5f, 47.5f);
        Instantiate(enemy, new Vector3(x, 1.0f, z), Quaternion.identity);
       
    }
    void OnSceneLoaded(Scene scene,LoadSceneMode mode)
    {
        
        enemycount = 0;
        clearcount = gm.level;
        for (int i = 0; i < gm.level+1; i++)
        {
            enemyspown();
        }
    }
}
