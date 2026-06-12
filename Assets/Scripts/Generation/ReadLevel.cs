using UnityEngine;

public class ReadLevel : MonoBehaviour
{

    public TextAsset levelData;
    private string[] lines;
    private string[][] level_data;
    private string[] cur_line;
    private string line;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (levelData != null)
        {
            lines = (levelData.text.Split("/"));
        }

//        Debug.Log(lines[0]);

        for (int i = 0; i <= lines.Length; i++)
        {
            line = lines[i];
            Debug.Log(line);
            cur_line = line.Split(",");
            for (int j = 0; j <= cur_line.Length; j++)
            {
                Debug.Log(cur_line[j]);
                level_data[i][j] = cur_line[j];
            }
        }

        //Debug.Log(level_data[0]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
