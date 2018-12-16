using UnityEngine;

public class Game : MonoBehaviour
{
    public LevelBuilder LevelBuilder;
    private Player player;
    private bool isReadyForInput;

    void Start()
    {
        LevelBuilder.Build();
        player = FindObjectOfType<Player>();
    }
    
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
