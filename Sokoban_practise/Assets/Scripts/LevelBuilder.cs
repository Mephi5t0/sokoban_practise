using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelElement
{
    public string Character;
    public GameObject Prefab;
}

public class LevelBuilder : MonoBehaviour
{
    public int CurrentLevelNumber;
    public List<LevelElement> LevelElements;
    private Level currentLevel;

    GameObject GetPrefab(char sym)
    {
        var levelElement = LevelElements.Find(x => x.Character == sym.ToString());
        
        return levelElement?.Prefab;
    }

    public void NextLevel()
    {
        CurrentLevelNumber++;
        if (CurrentLevelNumber >= GetComponent<Levels>().levels.Count)
        {
            CurrentLevelNumber = 0;
        }
    }

    public void Build()
    {
        currentLevel = GetComponent<Levels>().levels[CurrentLevelNumber];

        var startX = -currentLevel.Width / 2;
        var x = startX;
        var y = -currentLevel.Height / 2;
        
        foreach (var row in currentLevel.rows)
        {
            foreach (var sym in row)
            {
                Debug.Log(sym);
                var prefab = GetPrefab(sym);
                if (prefab != null)
                {
                    Debug.Log(prefab.name);
                    Instantiate(prefab, new Vector3(x, y, 0), Quaternion.identity);
                }

                x++;
            }

            y++;
            x = startX;
        }
    }
}
