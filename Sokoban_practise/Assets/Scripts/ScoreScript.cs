using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public static int MovementsCount = 0;
    private Text movements;
    
    void Start()
    {
        movements =  FindObjectOfType<Text>();
    }

    private void Update()
    {
        movements.text = "Steps: " + MovementsCount;
    }
}
