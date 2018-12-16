using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level
{
    public List<string> rows = new List<string>();

    public int Height => rows.Count;

    public int Width
    {
        get
        {
            var maxLength = 0;
            foreach (var row in rows)
            {
                if (row.Length > maxLength)
                {
                    maxLength = row.Length;
                }
            }

            return maxLength;
        }
    }

}

public class Levels : MonoBehaviour
{
    public string fileName;
    public List<Level> levels;

    void Awake()
    {
        var textAsset = (TextAsset) Resources.Load(fileName);
        if (!textAsset)
        {
            Debug.Log("Levels: " + fileName + ".txt is not found");
            return;
        }

        Debug.Log("Levels imported");

        var completeText = textAsset.text;

        var lines = completeText.Split(new[] {"\n"}, System.StringSplitOptions.None);
        levels.Add(new Level());

        foreach (var line in lines)
        {
            if (line.StartsWith(";"))
            {
                Debug.Log("New level added");
                levels.Add(new Level());
                continue;
            }
            levels[levels.Count - 1].rows.Add(line);
        }
    }
}
