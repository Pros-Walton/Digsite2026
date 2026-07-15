using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class ReadLevel : MonoBehaviour
{

    public GameObject floor;

    public GameObject wallNorth;
    public GameObject wallEast;
    public GameObject wallSouth;
    public GameObject wallWest;

    public GameObject floors;
    public GameObject norths;
    public GameObject easts;
    public GameObject souths;
    public GameObject wests;
    public GameObject items;
    public GameObject entites;
    
    private ObjectLists entityListComp;
    private GameObject[] entityList;
    private GameObject[] objList;

    public LevelData levelData = new LevelData();
    private Tile curTile = new Tile();
    private Tile checkTile;
    //private List<Tile> tileSet = new List<Tile>();
    private Tile[,] tileSet = new Tile[1,1];

    public GameObject playerTarget;

    public DiggerAgent agent;

    public bool load;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!SceneStates.LoadData)
        {
            agent = this.GetComponent<DiggerAgent>();
            agent.runDig();
        }
        //Debug.Log(path);

        readFiles();
        tileSet = new Tile[levelData.width,levelData.height];
        readTile();
        buildWalls();
        entityListComp = GetComponentInParent<ObjectLists>();
        entityList = entityListComp.entities;
        objList = entityListComp.items;
        spawnEnemies();
        spawnObjects();
        //Debug.Log(parsedLevel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void readTile()
    {
        foreach (string string_level in levelData.data)
        {
            curTile = JsonUtility.FromJson<Tile>(string_level);
            tileSet[curTile.posX,curTile.posY] = curTile;

            int real_posX = curTile.posX - levelData.radX;
            int real_posY = curTile.posY - levelData.radY;


            if (curTile.type != 0)
            {
                GameObject floorInstance = Instantiate(
                    floor, 
                    new Vector3 
                    (
                        real_posX*1.5f,
                        0.0f, 
                        real_posY*1.5f
                    ), 
                    Quaternion.identity
                );

                floorInstance.transform.parent = floors.transform;

                
            }

        }

    }

    private void buildWalls()
    {
        foreach (Tile curTile in tileSet)
        {
            if ((inRange(curTile.posY, 0, (levelData.height))) && (curTile.type != 0))
            {
                if (curTile.posY < levelData.height-1)
                {
                    checkTile = tileSet[curTile.posX,curTile.posY + 1];
                }
                else
                {
                    checkTile = new Tile();
                }
                if (checkTile.type == 0)
                {
                    int real_posX = curTile.posX - levelData.radX;
                    int real_posY = curTile.posY - levelData.radY;
                    GameObject northInstance = Instantiate(
                        wallNorth, 
                        new Vector3 
                        (
                            real_posX*1.5f,
                            0.75f, 
                            real_posY*1.5f + 0.75f
                        ),
                        Quaternion.Euler(90.0f, 180.0f, 0.0f)
                    );
                    northInstance.transform.parent = norths.transform;

                }
            }

            if ((inRange(curTile.posX, 0, (levelData.width))) && (curTile.type != 0))
            {
                if (curTile.posX < levelData.width-1)
                {
                    checkTile = tileSet[(curTile.posX + 1),curTile.posY];
                }
                else
                {
                    checkTile = new Tile();
                }
                if (checkTile.type == 0)
                {
                    int real_posX = curTile.posX - levelData.radX;
                    int real_posY = curTile.posY - levelData.radY;
                    GameObject eastInstance = Instantiate(
                        wallEast, 
                        new Vector3 
                        (
                            real_posX*1.5f + 0.75f,
                            0.75f, 
                            real_posY*1.5f
                        ),
                        Quaternion.Euler(90.0f, 270.0f, 0.0f)
                    );
                    eastInstance.transform.parent = easts.transform;


                }
            }

            if ((inRange(curTile.posY, 0, (levelData.height))) && (curTile.type != 0))
            {
                if (curTile.posY > 0)
                {
                    checkTile = tileSet[curTile.posX,curTile.posY - 1];
                }
                else
                {
                    checkTile = new Tile();
                } 
                if (checkTile.type == 0)
                {
                    int real_posX = curTile.posX - levelData.radX;
                    int real_posY = curTile.posY - levelData.radY;
                    GameObject southInstance = Instantiate(
                        wallSouth, 
                        new Vector3 
                        (
                            real_posX*1.5f,
                            0.75f, 
                            real_posY*1.5f - 0.75f
                        ),
                        Quaternion.Euler(90.0f, 0.0f, 0.0f)
                    );
                    southInstance.transform.parent = souths.transform;

                }
            }


            if (inRange(curTile.posX, 0, (levelData.width)) && (curTile.type != 0))
            {
                if (curTile.posX > 0)
                {
                    checkTile = tileSet[curTile.posX -1 ,curTile.posY];
                }
                else
                {
                    checkTile = new Tile();
                } 
                checkTile = tileSet[curTile.posX - 1,curTile.posY];
                if (checkTile.type == 0)
                {
                    int real_posX = curTile.posX - levelData.radX;
                    int real_posY = curTile.posY - levelData.radY;
                    GameObject westInstance = Instantiate(
                        wallWest, 
                        new Vector3 
                        (
                            real_posX*1.5f - 0.75f,
                            0.75f, 
                            real_posY*1.5f
                        ),
                        Quaternion.Euler(90.0f, 90.0f, 0.0f)
                    );
                    westInstance.transform.parent = wests.transform;

                }

            }
        }

    }

    private bool inRange(int target, int low, int high)
    {
        //Debug.Log(low.ToString() + " " + target.ToString() + " " + high.ToString());
        return (low < target) && (target < high);
    }

    private void spawnEnemies()
    {
        foreach (Tile curTile in tileSet)
        {
            if (curTile.ent != 0)
            {
                int real_posX = curTile.posX - levelData.radX;
                int real_posY = curTile.posY - levelData.radY;
                GameObject enemy = Instantiate(
                    entityList[curTile.ent - 1], 
                    new Vector3 
                    (
                        real_posX * 1.5f,
                        0.4f, 
                        real_posY * 1.5f
                    ), 
                    Quaternion.identity
                );
                enemy.transform.parent = entites.transform;

            }
        }
    }

    private void spawnObjects()
    {
        foreach (Tile curTile in tileSet)
        {
            if (curTile.obj != 0)
            {
                int real_posX = curTile.posX - levelData.radX;
                int real_posY = curTile.posY - levelData.radY;
                GameObject itemInstance = Instantiate(
                    objList[curTile.obj - 1], 
                    new Vector3 
                    (
                        real_posX * 1.5f,
                        0.15f, 
                        real_posY * 1.5f
                    ), 
                    Quaternion.identity
                );
                itemInstance.transform.parent = items.transform;

            }
        }
    }

    private void readFiles()
    {        
        string levPath = Application.persistentDataPath + "/level.json";

        if (File.Exists(levPath))
        {
            levelData = JsonUtility.FromJson<LevelData>(File.ReadAllText(levPath));
        }
        else
        {
            File.Create(levPath);
            File.WriteAllText(levPath,"");
            levelData = JsonUtility.FromJson<LevelData>(File.ReadAllText(levPath));
        }
    }

}
