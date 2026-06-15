using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class LevelData
{
    public int level_width = 0;
    public int level_height = 0;
    public int level_rad_x = 0;
    public int level_rad_y = 0;

    public List<string> data = new List<string>();
}
