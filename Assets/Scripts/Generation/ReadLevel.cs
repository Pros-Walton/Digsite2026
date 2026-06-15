using UnityEngine;
using System.Collections.Generic;

public class ReadLevel : MonoBehaviour
{

    //Raw data file
    public TextAsset levelData;
    public TextAsset levelDataJson;

    public GameObject floor;

    public GameObject wallNorth;
    public GameObject wallEast;
    public GameObject wallSouth;
    public GameObject wallWest;
    

    //parse variables
    // private string[] lines;
    // private string[] cur_line;
    // private string line;

    // //Level data parsing
    // private List<int> line_types = new List<int>();
    // private List<List<int>> full_level = new List<List<int>>();

    // //Level dimentions
    // private int levelWidth = 0;
    // private int levelHeight = 0;

    // //Level gemometry plot
    // private int levelRadWidth;
    // private int levelRadHeight;

    // private LevelData level_data = new LevelData();
    // private List<string> tile_data = new List<string>();

    private LevelData level_data = new LevelData();
    private Tile cur_tile = new Tile();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //parseData();
        //printData();
        //findSize();
        //string parsedLevel = JsonUtility.ToJson(level_data);
        level_data = JsonUtility.FromJson<LevelData>(levelDataJson.text);
        readTile();
        //Debug.Log(parsedLevel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void readTile()
    {
        foreach (string tile_string in level_data.data)
        {
            cur_tile = JsonUtility.FromJson<Tile>(tile_string);

            int real_pos_x = cur_tile.tile_pos_x - level_data.level_rad_x;
            int real_pos_y = cur_tile.tile_pos_y - level_data.level_rad_y;


            if (cur_tile.tile_type != 0)
            {
                Instantiate(
                    floor, 
                    new Vector3 
                    (
                        real_pos_x*1.5f,
                        0.0f, 
                        real_pos_y*1.5f
                    ), 
                    Quaternion.identity);
            }
        }
    }

//     private void parseData()
//     {
//         if (levelData != null)
//         {
//             lines = (levelData.text.Split("/"));
//         }

// //        Debug.Log(lines[0]);

//         for (int i = 0; i < lines.Length; i++)
//         {
//             line = lines[i];
//             cur_line = line.Split(",");
//             for (int j = 0; j < cur_line.Length; j++)
//             {
//                 Tile cur_tile = new Tile();
//                 cur_tile.tile_type = int.Parse(cur_line[j]);
//                 cur_tile.tile_pos_x = j;
//                 cur_tile.tile_pos_y = i;
//                 line_types.Add((int.Parse(cur_line[j])));
//                 string tile_serial = JsonUtility.ToJson(cur_tile);
//                 tile_data.Add(tile_serial);
//                //level_data[i][j] = cur_line[j];
//             }
//             full_level.Add(line_types);
//             line_types = new List<int>();
//         }
//         level_data.level_data = tile_data;
//     }

//     private void printData()
//     {
//         foreach (List<int> line_now in full_level)
//         {
//             foreach (int tile in line_now) 
//             {
//                 Debug.Log(tile);
//             }
//         }

//     }

//     private void findSize()
//     {
//         levelHeight = full_level.Count;
//         foreach (List<int> line_row in full_level)
//         {
//             levelWidth = Mathf.Max(levelWidth, line_row.Count);
//         }

//         levelRadHeight = (levelHeight - 1)/2;
//         levelRadWidth = (levelWidth - 1)/2;

//         level_data.level_height = levelHeight;
//         level_data.level_width = levelWidth;
//         level_data.level_rad_x = levelRadWidth;
//         level_data.level_rad_y = levelRadHeight;
//     }
}
