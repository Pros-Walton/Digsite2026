using UnityEngine;
using System.Collections.Generic;


public class DiggerAgent : MonoBehaviour
{

    public int[,] grid;
    private ReadLevel level_read;
    public int height;
    public int width;
    private int x_coords;
    private int y_coords;
    private bool dungeon_big = false;


    //parse variables
    private string[] lines;
    private string[] cur_line;
    private string line;

    //Level data parsing
    private List<int> line_types = new List<int>();
    private List<List<int>> full_level = new List<List<int>>();

    //Level dimentions
    private int levelWidth = 0;
    private int levelHeight = 0;

    //Level gemometry plot
    private int levelRadWidth;
    private int levelRadHeight;

    private LevelData level_data = new LevelData();
    private List<string> tile_data = new List<string>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        grid = new int[width,height];
        level_read = this.GetComponent<ReadLevel>();
        setupArray();
        x_coords = (width-1)/2;
        y_coords = (height-1)/2;
    }

    // Update is called once per frame
    void Update()
    {
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
        int directionChange = 5;
        int roomSpawn = 5;

        Vector2 digger_loc = new Vector2(x_coords,y_coords);

        grid[(int)digger_loc.x,(int)digger_loc.y] = 1;

        List<Vector2> directions = new List<Vector2>();

        directions.Add(Vector2.up); 
        directions.Add(Vector2.left); 
        directions.Add(Vector2.down);
        directions.Add(Vector2.right);


        

        Vector2 direction = directions[UnityEngine.Random.Range(0,3)];

        while (!dungeon_big) 
        {
            digger_loc += direction;
            if (UnityEngine.Random.Range(0,100) < directionChange)
            {
                direction = directions[UnityEngine.Random.Range(0,3)];
                directionChange = 0;
            }
            else
            {
                directionChange += 5;
            }

            if (UnityEngine.Random.Range(0,100) < roomSpawn)
            {
                int room_x = UnityEngine.Random.Range(2,4);
                int room_y = UnityEngine.Random.Range(3,7);
                for (int i = -room_x; i < room_x; i++)
                {
                    for (int j = -room_y; j < room_y; j++)
                    {
                        grid[
                            (int)digger_loc.x + i,
                            (int)digger_loc.y + j 
                        ] = 2;
                    }
                }
                roomSpawn = 0;

            }
            else
            {
                roomSpawn += 5;
                grid[
                    (int)digger_loc.x,
                    (int)digger_loc.y
                    ] = 1;
            }
        }

    }

        private void parseData()
    {
        if (level_read.levelData != null)
        {
            lines = (level_read.levelData.text.Split("/"));
        }

//        Debug.Log(lines[0]);

        for (int i = 0; i < lines.Length; i++)
        {
            line = lines[i];
            cur_line = line.Split(",");
            for (int j = 0; j < cur_line.Length; j++)
            {
                Tile cur_tile = new Tile();
                cur_tile.tile_type = int.Parse(cur_line[j]);
                cur_tile.tile_pos_x = j;
                cur_tile.tile_pos_y = i;
                line_types.Add((int.Parse(cur_line[j])));
                string tile_serial = JsonUtility.ToJson(cur_tile);
                tile_data.Add(tile_serial);
               //level_data[i][j] = cur_line[j];
            }
            full_level.Add(line_types);
            line_types = new List<int>();
        }
        level_read.level_data.data = tile_data;
    }

    private void printData()
    {
        foreach (List<int> line_now in full_level)
        {
            foreach (int tile in line_now) 
            {
                Debug.Log(tile);
            }
        }

    }

    private void findSize()
    {
        levelHeight = full_level.Count;
        foreach (List<int> line_row in full_level)
        {
            levelWidth = Mathf.Max(levelWidth, line_row.Count);
        }

        levelRadHeight = (levelHeight - 1)/2;
        levelRadWidth = (levelWidth - 1)/2;

        level_read.level_data.level_height = levelHeight;
        level_read.level_data.level_width = levelWidth;
        level_read.level_data.level_rad_x = levelRadWidth;
        level_read.level_data.level_rad_y = levelRadHeight;
    }
}
