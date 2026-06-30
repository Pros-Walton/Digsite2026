using UnityEngine;
using System.Collections.Generic;
using System.IO;


public class DiggerAgent : MonoBehaviour
{

    public int[,] grid;
    private ReadLevel levelRead;
    public TextAsset levelDataJson;
    public int height;
    public int width;
    private int x_coords;
    private int y_coords;
    private bool dungeonBig = false;


    //parse variables
    private string[] lines;
    // private string[] curLine;
    // private string line;

    //Level data parsing
    // private List<int> lineTypes = new List<int>();
    // private List<List<int>> full_level = new List<List<int>>();

    //Level gemometry plot
    private int levelRadWidth;
    private int levelRadHeight;
    private int directionChange = 5;
    private int roomSpawn = 5;

    private LevelData levelData = new LevelData();
    private List<string> tileData = new List<string>();

    private int dungeonSize;
    private int dungeonFill = 0;
    private float dungeonThreshold = 0.40f;

    private string dataToSave;

    private List<Vector2> directions = new List<Vector2>();
    private Vector2 direction = new Vector2();
    private Vector2 diggerLoc = new Vector2();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        grid = new int[width,height];
        dungeonSize = height*width;
        levelRead = this.GetComponent<ReadLevel>();
        levelData = JsonUtility.FromJson<LevelData>(levelDataJson.text);
        setupArray();
        x_coords = (width-1)/2;
        y_coords = (height-1)/2;
        findSize();
        Digger();
        parseData();
        //printData();
        dataToSave = JsonUtility.ToJson(levelData);
        string path = (Application.dataPath + "/Scenes/SampleScene/level.json");
        File.WriteAllText(path, dataToSave);
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
            //Debug.Log(((int)diggerLoc.x).ToString() + ", " + ((int)diggerLoc.y).ToString() + "  |  " +
            //(((int)diggerLoc.x) + ((int)direction.x)).ToString() + ", " + (((int)diggerLoc.y) + ((int)direction.y)).ToString());
            diggerLoc += direction;

            int locX = (int)diggerLoc.x;
            int locY = (int)diggerLoc.y;

            if (UnityEngine.Random.Range(0,100) < directionChange)
            {
                direction = directions[UnityEngine.Random.Range(0,3)];
                directionChange = 0;
            }
            else
            {
                directionChange += 1;
            }

            if (UnityEngine.Random.Range(0,250) < roomSpawn)
            {
                int roomX = UnityEngine.Random.Range(1,2);
                int roomY = UnityEngine.Random.Range(2,4);
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
        // if (levelRead.levelData != null)
        // {
        //     lines = (levelRead.levelData.text.Split("/"));
        // }

//        Debug.Log(lines[0]);

        for (int i = 0; i < width; i++)
        {
            // line = lines[i];
            // curLine = line.Split(",");
            for (int j = 0; j < height; j++)
            {
                Tile curTile = new Tile();
                curTile.type = grid[i,j];
                //Debug.Log(grid[i,j]);
                curTile.posX = j;
                curTile.posY = i;

                if (curTile.type == 2)
                {
                    if (UnityEngine.Random.Range(0,5) == 0)
                    {
                        curTile.ent = 1;
                    }
                }

                // lineTypes.Add((int.Parse(curLine[j])));
                string serial = JsonUtility.ToJson(curTile);
                //Debug.Log(serial);
                tileData.Add(serial);
                //level_data[i][j] = curLine[j];
            }
            // full_level.Add(lineTypes);
            // lineTypes = new List<int>();
        }
        levelData.data = tileData;
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
        // levelHeight = full_level.Count;
        // foreach (List<int> line_row in full_level)
        // {
        //     levelWidth = Mathf.Max(levelWidth, line_row.Count);
        // }

        levelRadHeight = (height - 1)/2;
        levelRadWidth = (width - 1)/2;

        levelData.height = height;
        levelData.width = width;
        levelData.radX = levelRadWidth;
        levelData.radY = levelRadHeight;
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
}
