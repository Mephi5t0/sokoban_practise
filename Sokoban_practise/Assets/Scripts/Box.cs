using System;
using System.Linq;
using UnityEngine;

public class Box : MonoBehaviour
{
    public bool isOnCross;
    private static readonly double TOLERANCE = 0.0001;

    public bool Move(Vector2 direction)
    {
        if (IsBlocked(transform.position, direction))
        {
            return false;
        }

        transform.Translate(direction);
        TestIfOnCross();
        
        return true;
    }

    private bool IsBlocked(Vector3 position, Vector2 direction)
    {
        var newPosition = new Vector2(position.x, position.y) + direction;
        var walls = GameObject.FindGameObjectsWithTag("Wall");

        if (walls.Any(wall => Math.Abs(wall.transform.position.x - newPosition.x) < TOLERANCE 
                              && Math.Abs(wall.transform.position.y - newPosition.y) < TOLERANCE))
        {
            return true;
        }

        var boxes = GameObject.FindGameObjectsWithTag("Box");
        if (boxes.Any(box => Math.Abs(box.transform.position.x - newPosition.x) < TOLERANCE 
                             && Math.Abs(box.transform.position.y - newPosition.y) < TOLERANCE))
        {
            return true;
        }

        return false;
    }

    private void TestIfOnCross()
    {
        var crosses = GameObject.FindGameObjectsWithTag("Cross");

        if (crosses.Any(cross => Math.Abs(transform.position.x - cross.transform.position.x) < TOLERANCE
                                 && Math.Abs(transform.position.y - cross.transform.position.y) < TOLERANCE))
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            isOnCross = true;
            return;
        }
        GetComponent<SpriteRenderer>().color = Color.white;
        isOnCross = false;
    }
}
