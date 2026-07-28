using UnityEngine;
using System.Collections.Generic;
using System.IO;


public class DiggerAgent : MonoBehaviour
{

    public int[,] grid;
    private ReadLevel levelRead;
    public int height;
    public int width;
    private int x_coords;
    private int y_coords;
    private bool dungeonBig = false;


    //parse variables
    private string[] lines;

    //Level gemometry plot
    private int levelRadWidth;
    private int levelRadHeight;
    private int directionChange = 5;
    private int roomSpawn = 5;

    private int dungeonSize;
    private int dungeonFill = 0;
    private float dungeonThreshold = 0.40f;

    private string levelString;
    private string dataToSave;

    private List<Vector2> directions = new List<Vector2>();
    private Vector2 direction = new Vector2();
    private Vector2 diggerLoc = new Vector2();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void runDig()
    {
        States.leveldata = new LevelData();
        States.tileData = new List<string>();
        States.tiles = new List<Tile>();
        string path = (Application.persistentDataPath + "/Save/level.json");
        grid = new int[width,height];
        dungeonSize = height*width;
        // if (File.Exists(path))
        // {
        //     levelString = File.ReadAllText(path);
        // }
        // else
        // {
        //     File.Create(path);
        // }
        //States.leveldata = JsonUtility.FromJson<LevelData>(levelRead);
        setupArray();
        x_coords = (width-1)/2;
        y_coords = (height-1)/2;
        findSize();
        Digger();
        parseData();
        dedicateSpace();
        placeHole();
        wirteData();
        //printData();
        dataToSave = JsonUtility.ToJson(States.leveldata);
        //Debug.Log(levelString);
        //File.WriteAllText(path, dataToSave);
    }

    void setupArray()
    {
        for(int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                grid[i,j] = 0;
            }
        }
    }

    void Digger()
    {

        diggerLoc = new Vector2(x_coords,y_coords);

        grid[(int)diggerLoc.x,(int)diggerLoc.y] = 1;
        dungeonFill += 1;

        directions.Add(Vector2.up); 
        directions.Add(Vector2.left); 
        directions.Add(Vector2.down);
        directions.Add(Vector2.right);

        direction = directions[UnityEngine.Random.Range(0,3)];

        while (!dungeonBig) 
        {
            outOfBoundsCheck();
            diggerLoc += direction;

            int locX = (int)diggerLoc.x;
            int locY = (int)diggerLoc.y;

            if (UnityEngine.Random.Range(0,100) < directionChange)
            {
                direction = directions[UnityEngine.Random.Range(0,4)];
                directionChange = 0;
            }
            else
            {
                directionChange += 1;
            }

            if (UnityEngine.Random.Range(0,250) < roomSpawn)
            {
                int roomX = UnityEngine.Random.Range(1,2);
                int roomY = UnityEngine.Random.Range(1,4);
                //Debug.Log("Room start!");
                for (int i = -roomX; i < roomX; i++)
                {
                    for (int j = -roomY; j < roomY; j++)
                    {
                        int roomBuildX = locX + i;
                        int roomBuildY = locY + j;
                        if (inRange(roomBuildX,0,height) && inRange(roomBuildY,0,width))
                        {
                            //Debug.Log(roomBuildX.ToString() + ", " + roomBuildY.ToString());
                            grid[roomBuildX,roomBuildY] = 2;
                        }
                    }
                }
                //Debug.Log("Room done! " + locX.ToString() + ", " + locY.ToString());
                roomSpawn = 0;
                dungeonFill += (roomX * roomY);

            }
            else
            {
                roomSpawn += 1;
                if (inRange(locX,0,height) && inRange(locY,0,width))
                {
                    grid[locX,locY] = 1;
                }
                dungeonFill += 1;
            }

            if ((dungeonFill / dungeonSize) > dungeonThreshold)
            {
                dungeonBig = true;
            }
        }

    }

        private void parseData()
    {
        int current = 0;
        int enemySelectorMax = Mathf.Min(2, States.stats.depth);
        int enemySelectorMin = Mathf.Max(0, (States.stats.depth - 2));
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Tile curTile = new Tile();
                curTile.type = grid[i,j];
                //Debug.Log(grid[i,j]);
                curTile.posX = j;
                curTile.posY = i;

                if (curTile.type != 0)
                {
                    if (UnityEngine.Random.Range(0,7) == 0)
                    {
                        Enemy enemy = new Enemy();
                        int roll = UnityEngine.Random.Range(enemySelectorMin,enemySelectorMax);
                        enemy.populate(current, roll);
                        string enemyStr = JsonUtility.ToJson(enemy);
                        curTile.ent = enemyStr;
                        //curTile.ent = (int)UnityEngine.Random.Range(1,1);
                    }
                    else
                    {
                        curTile.ent = "NULL";
                    }
                }
                else
                {
                    curTile.ent = "NULL";
                }

                States.tiles.Add(curTile);
                current++;
            }
        }
    }

    private void printData()
    {
        for (int i = 0; i < width; i++) 
        {
            for (int j = 0; j < height; j++)
            {
                Debug.Log(grid[i,j]);
            }
        }
    }

    private void findSize()
    {


        levelRadHeight = (height - 1)/2;
        levelRadWidth = (width - 1)/2;

        States.leveldata.height = height;
        States.leveldata.width = width;
        States.leveldata.radX = levelRadWidth;
        States.leveldata.radY = levelRadHeight;
    }

    private void outOfBoundsCheck()
    {
        if (((int)diggerLoc.x + (int)direction.x) > (width - 2))
        {
            direction = directions[0];
            directionChange = 0;
        }
        else if (((int)diggerLoc.x + (int)direction.x) < 2)
        {
            direction = directions[2];
            directionChange = 0;
        }

        if (((int)diggerLoc.x + (int)direction.x) > (height - 2))
        {
            direction = directions[1];
            directionChange = 0;
        }
        else if (((int)diggerLoc.y + (int)direction.y) < 2)
        {
            direction = directions[3];
            directionChange = 0;
        }
    }
    
    private bool inRange(int target, int low, int high)
    {
        //Debug.Log(low.ToString() + " " + target.ToString() + " " + high.ToString());
        return (low < target) && (target < high);
    }

    private void wirteData()
    {
        foreach (Tile curTile in States.tiles)
        {
            string serial = JsonUtility.ToJson(curTile);
            States.tileData.Add(serial);
        }
        States.leveldata.data = States.tileData;
    }

    private void dedicateSpace()
    {
        int counter = 0;
        int chance = 0;
        foreach (Tile curTile in States.tiles)
        {
            if (curTile.type == 2)
            {
                chance = 10;
            }
            else if (curTile.type == 1)
            {
                chance = 30;
            }
            if (curTile.type != 0)
            {
                if (UnityEngine.Random.Range(0,chance) == 0)
                {
                    Pickup drop = new Pickup();
                    drop.populate(counter,(int)UnityEngine.Random.Range(0,0));
                    drop.applyTo();
                    string dropStr = JsonUtility.ToJson(drop);
                    curTile.obj = dropStr;
                }
                else
                {
                    curTile.obj = "NULL";
                }            
            }
            else
            {
                curTile.obj = "NULL";
            }
            counter ++;  
        }
    }

    private void placeHole()
    {
        int max = States.tiles.Count;
        Tile curTile = States.tiles[0];
        while (curTile.type != 2)
        {
            curTile = States.tiles[UnityEngine.Random.Range(0,max)];
        }
        curTile.obj = "HOLE";
    }
}
