using UnityEngine;
using System.Collections.Generic;

public class ReadLevel : MonoBehaviour
{

    //Raw data file
    public TextAsset levelDataTxt;
    public TextAsset levelDataJson;

    public GameObject floor;

    public GameObject wallNorth;
    public GameObject wallEast;
    public GameObject wallSouth;
    public GameObject wallWest;
    


    public LevelData levelData = new LevelData();
    private Tile curTile = new Tile();
    private Tile checkTile;
    //private List<Tile> tileSet = new List<Tile>();
    private Tile[,] tileSet = new Tile[1,1];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //parseData();
        //printData();
        //findSize();
        //string parsedLevel = JsonUtility.ToJson(levelDataTxt);
        levelData = JsonUtility.FromJson<LevelData>(levelDataJson.text);
        tileSet = new Tile[levelData.width,levelData.height];
        readTile();
        buildWalls();
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
                Instantiate(
                    floor, 
                    new Vector3 
                    (
                        real_posX*1.5f,
                        0.0f, 
                        real_posY*1.5f
                    ), 
                    Quaternion.identity
                );

                
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
                    Instantiate(
                        wallNorth, 
                        new Vector3 
                        (
                            real_posX*1.5f,
                            0.75f, 
                            real_posY*1.5f + 0.75f
                        ),
                        Quaternion.Euler(90.0f, 180.0f, 0.0f)
                    );

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
                    Instantiate(
                        wallEast, 
                        new Vector3 
                        (
                            real_posX*1.5f + 0.75f,
                            0.75f, 
                            real_posY*1.5f
                        ),
                        Quaternion.Euler(90.0f, 270.0f, 0.0f)
                    );


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
                    Instantiate(
                        wallSouth, 
                        new Vector3 
                        (
                            real_posX*1.5f,
                            0.75f, 
                            real_posY*1.5f - 0.75f
                        ),
                        Quaternion.Euler(90.0f, 0.0f, 0.0f)
                    );

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
                    Instantiate(
                        wallWest, 
                        new Vector3 
                        (
                            real_posX*1.5f - 0.75f,
                            0.75f, 
                            real_posY*1.5f
                        ),
                        Quaternion.Euler(90.0f, 90.0f, 0.0f)
                    );

                }

            }
        }

    }

    private bool inRange(int target, int low, int high)
    {
        //Debug.Log(low.ToString() + " " + target.ToString() + " " + high.ToString());
        return (low < target) && (target < high);
    }

}
