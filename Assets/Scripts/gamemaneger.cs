using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gamemaneger : MonoBehaviour
{
    [SerializeField]
    public int level;
    static gamemaneger instance;
    [SerializeField]
    Button retrybutton;
    [SerializeField]
    GameObject leveltext;
    TextMeshProUGUI nowlevel;
    // Start is called before the first frame update
    void Start()
    {
        nowlevel = leveltext.GetComponent<TextMeshProUGUI>();
        
        if (instance!= null && instance!= this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += onsceneloaded;
    }

    // Update is called once per frame
    void Update()
    {
        nowlevel.text = "LV " + level;
    }
    public void loadscene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void onsceneloaded(Scene scene,LoadSceneMode mode)
    {
        retrybutton = GameObject.Find("Canvas/gameover/retrybutton").GetComponent<Button>();
        retrybutton.onClick.AddListener(() => loadscene());
        leveltext = GameObject.Find("Canvas/leveltext");
        nowlevel = leveltext.GetComponent<TextMeshProUGUI>();
    }
}
