using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
[System.Serializable]
public class cell
{
    public List<bool> bools = new List<bool>()
    {
        false,//â∫
        false,//ç∂
        false,//è„
        false,//âE
        
    };
    public Vector2 pos;
    public cell(Vector2 pos)
    {
        this.pos = pos;
    }
    

}
[System.Serializable]
public class Celllist
{
    public List<cell> celllist = new List<cell>();
    /*public Celllist(List<cell> celllist)
    {
        this.celllist = celllist;
    }*/
}
public class mapmaker : MonoBehaviour
{
    [SerializeField]
    GameObject FloorPrefab;
    [SerializeField]
    GameObject yokoKabePrefab;
    [SerializeField]
    GameObject tateKabePrefab;
    [SerializeField]
    Vector2 floorsize;
    [SerializeField]
    float makerate;
    [SerializeField]
    List<List<cell>> cells = new List<List<cell>>();
    [SerializeField]
    List<Celllist> celllists = new List<Celllist>();
    // Start is called before the first frame update
    void Start()
    {
        celllists.Clear();
        makemap(floorsize);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            resetscene();
        }
    }
    public void makemap(Vector2 floorsize)
    {
        
        for (int i = 0; i < floorsize.x; i++)
        {
            cells.Add(new List<cell>());
            celllists.Add(new Celllist());
            for(int j = 0; j < floorsize.y; j++)
            {
                GameObject floor = Instantiate(FloorPrefab,transform);
                floor.transform.position = new Vector3(i * 5, 0, j * 5);
                cells[i].Add(new cell(new Vector2(i,j)));
                celllists[i].celllist.Add(new cell(new Vector2(i,j)));
            }
        }
        
        for (int i = 0; i < floorsize.x ; i++)
        {
            
            for (int j = 0; j < floorsize.y+1; j++)
            {
                float rnd = Random.value;
                if (j == 0 || j == floorsize.y)
                {
                    cells[i][0].bools[0] = true;
                    cells[i][(int)floorsize.y - 1].bools[2] = true;
                    celllists[i].celllist[0].bools[0] = true;
                    celllists[i].celllist[(int)floorsize.y - 1].bools[2] = true;
                    GameObject yokokabe = Instantiate(yokoKabePrefab, transform);
                    yokokabe.transform.position = new Vector3(i * 5, 0, j * 5) + new Vector3(0, 2.5f, -2.5f);
                   
                }
                else if(makerate > rnd)
                {
                    GameObject yokokabe = Instantiate(yokoKabePrefab, transform);
                    yokokabe.transform.position = new Vector3(i * 5, 0, j * 5) + new Vector3(0, 2.5f, -2.5f);
                    cells[i][j - 1].bools[2] = true;
                    cells[i][j].bools[0] = true;
                    celllists[i].celllist[j - 1].bools[2] = true;
                    celllists[i].celllist[j].bools[0] = true;
                    /*if (reachable(new Vector2(i, j - 1)) && reachable(new Vector2(i, j)))
                    {
                        GameObject yokokabe = Instantiate(yokoKabePrefab, transform);
                        yokokabe.transform.position = new Vector3(i * 5, 0, j * 5) + new Vector3(0, 2.5f, -2.5f);
                    }
                    else
                    {
                        cells[i][j - 1].bools[2] = false;
                        cells[i][j].bools[0] = false;
                        celllists[i].celllist[j - 1].bools[2] = false;
                        celllists[i].celllist[j].bools[0] = false;
                    }*/
                    
                }
                
            }

        }
        for (int i = 0; i < floorsize.x+1; i++)
        {
            for (int j = 0; j < floorsize.y; j++)
            {
                float rnd = Random.value;
                if(i == 0 || i == floorsize.x )//äOë§ÇÃògÇÃècï«
                {
                    cells[0][j].bools[1] = true;
                    cells[(int)floorsize.x - 1][j].bools[3] = true;
                    celllists[0].celllist[j].bools[1] = true;
                    celllists[(int)floorsize.x - 1].celllist[j].bools[3] = true;
                    GameObject tatekabe = Instantiate(tateKabePrefab, transform);
                    tatekabe.transform.position = new Vector3(i * 5, 0, j * 5) + new Vector3(-2.5f, 2.5f, 0);
                }
                else if(makerate > rnd)//ÉâÉìÉ_ÉÄÇ≈ÇΩÇƒÇÈÇ©Ç«Ç§Ç©
                {
                    cells[i-1][j].bools[3] = true; 
                    cells[i][j].bools[1] = true;
                    celllists[i-1].celllist[j].bools[3] = true;
                    celllists[i].celllist[j].bools[1] = true;
                    
                    if (reachable(new Vector2(i - 1, j))&&reachable(new Vector2(i,j)))
                    {
                        GameObject tatekabe = Instantiate(tateKabePrefab, transform);
                        tatekabe.transform.position = new Vector3(i * 5, 0, j * 5) + new Vector3(-2.5f, 2.5f, 0);
                    }
                    else
                    {
                        cells[i - 1][j].bools[3] = false;
                        cells[i][j].bools[1] = false;
                        celllists[i - 1].celllist[j].bools[3] = false;
                        celllists[i].celllist[j].bools[1] = false;
                    }
                    
                }
               
                    
                

            }

        }
    }
   
    public void resetscene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    List<Vector2>passlist = new List<Vector2>();
    public bool reachable(Vector2 pos)
    {
        
        if(pos == Vector2.zero)
        {
            passlist = new List<Vector2>();
            return true;
        }
        int []dx = { 0, -1, 0, 1 }; 
        int[] dy = { -1, 0, 1, 0 };
        for (int i = 0;i < 4;i++) 
        {
            if(pos.x + dx[i] == -1 || pos.y + dy[i] == -1 || pos.x + dx[i] == floorsize.x || pos.y + dy[i] == floorsize.y)
            {
                continue;
            }

            if (!cells[(int)pos.x][(int)pos.y].bools[i] && !passlist.Exists(x => x == pos + new Vector2(dx[i], dy[i])))
            {
                passlist.Add(pos);
                return reachable(pos + new Vector2(dx[i],dy[i]));
            }
        }
        passlist = new List<Vector2>();
        return false;
    }
}
