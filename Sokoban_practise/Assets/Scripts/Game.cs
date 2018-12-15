using UnityEngine;

public class Game : MonoBehaviour
{
    public Player player;
    private bool isReadyForInput;

    void Update()
    {
        var moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        
        moveInput.Normalize();
        if (moveInput.sqrMagnitude > 0.5)
        {
            if (isReadyForInput)
            {
                isReadyForInput = false;
                player.Move(moveInput);
            }
        }
        else
        {
            isReadyForInput = true;
        }
    }
}
