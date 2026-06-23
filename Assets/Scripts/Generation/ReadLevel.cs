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
    


    public LevelData level_data = new LevelData();
    private Tile cur_tile = new Tile();
    private List<Tile> tileSet = new List<Tile>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //parseData();
        //printData();
        //findSize();
        //string parsedLevel = JsonUtility.ToJson(level_data);
        level_data = JsonUtility.FromJson<LevelData>(levelDataJson.text);
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
        foreach (string tile_string in level_data.data)
        {
            cur_tile = JsonUtility.FromJson<Tile>(tile_string);
            tileSet.Add(cur_tile);

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
                    Quaternion.identity
                );

                
            }

        }

    }

    private void buildWalls()
    {
        foreach (Tile cur_tile in tileSet)
        {
            if ((cur_tile.tile_pos_y >= 1) && (cur_tile.tile_type != 0))
            {
                foreach (Tile check_tile in tileSet)
                {
                    if ((check_tile.tile_pos_y == cur_tile.tile_pos_y+1 &&
                        check_tile.tile_pos_x == cur_tile.tile_pos_x) && 
                        check_tile.tile_type == 0)
                        {
                            int real_pos_x = check_tile.tile_pos_x - level_data.level_rad_x;
                            int real_pos_y = check_tile.tile_pos_y - level_data.level_rad_y;
                            Instantiate(
                                wallNorth, 
                                new Vector3 
                                (
                                    real_pos_x*1.5f,
                                    0.75f, 
                                    real_pos_y*1.5f - 0.75f
                                ),
                                Quaternion.Euler(90.0f, 180.0f, 0.0f)
                            );

                        }
                }
            }

            if ((cur_tile.tile_pos_y <= level_data.level_height - 2) && (cur_tile.tile_type != 0))
            {
                foreach (Tile check_tile in tileSet)
                {
                    if ((check_tile.tile_pos_y == cur_tile.tile_pos_y-1 &&
                        check_tile.tile_pos_x == cur_tile.tile_pos_x) && 
                        check_tile.tile_type == 0)
                        {
                            int real_pos_x = check_tile.tile_pos_x - level_data.level_rad_x;
                            int real_pos_y = check_tile.tile_pos_y - level_data.level_rad_y;
                            Instantiate(
                                wallSouth, 
                                new Vector3 
                                (
                                    real_pos_x*1.5f,
                                    0.75f, 
                                    real_pos_y*1.5f + 0.75f
                                ),
                                Quaternion.Euler(90.0f, 0.0f, 0.0f)
                            );

                        }
                }
            }

            if ((cur_tile.tile_pos_x >= 1) && (cur_tile.tile_type != 0))
            {
                foreach (Tile check_tile in tileSet)
                {
                    if ((check_tile.tile_pos_y == cur_tile.tile_pos_y &&
                        check_tile.tile_pos_x == cur_tile.tile_pos_x+1) && 
                        check_tile.tile_type == 0)
                        {
                            int real_pos_x = check_tile.tile_pos_x - level_data.level_rad_x;
                            int real_pos_y = check_tile.tile_pos_y - level_data.level_rad_y;
                            Instantiate(
                                wallEast, 
                                new Vector3 
                                (
                                    real_pos_x*1.5f - 0.75f,
                                    0.75f, 
                                    real_pos_y*1.5f
                                ),
                                Quaternion.Euler(90.0f, 270.0f, 0.0f)
                            );

                        }
                }
            }

            if ((cur_tile.tile_pos_x <= level_data.level_width - 2) && (cur_tile.tile_type != 0))
            {
                foreach (Tile check_tile in tileSet)
                {
                    if ((check_tile.tile_pos_y == cur_tile.tile_pos_y &&
                        check_tile.tile_pos_x == cur_tile.tile_pos_x-1) && 
                        check_tile.tile_type == 0)
                        {
                            int real_pos_x = check_tile.tile_pos_x - level_data.level_rad_x;
                            int real_pos_y = check_tile.tile_pos_y - level_data.level_rad_y;
                            Instantiate(
                                wallWest, 
                                new Vector3 
                                (
                                    real_pos_x*1.5f + 0.75f,
                                    0.75f, 
                                    real_pos_y*1.5f
                                ),
                                Quaternion.Euler(90.0f, 90.0f, 0.0f)
                            );

                        }
                }
            }
        }

    }

}
