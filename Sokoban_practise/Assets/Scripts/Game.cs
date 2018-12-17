using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public LevelBuilder LevelBuilder;
    public GameObject NextButton;
    private Player player;
    private bool isReadyForInput;

    public void Play()
    {
        SceneManager.UnloadSceneAsync("Menu");
        Resources.UnloadUnusedAssets();
        Start();
    }
    
    void Start()
    {
        NextButton.SetActive(false) ;
        ResetScene();
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
                if (player.Move(moveInput))
                {
                    ScoreScript.MovementsCount++;
                }
                NextButton.SetActive(IsLevelComplete());
            }
        }
        else
        {
            isReadyForInput = true;
        }
    }

    bool IsLevelComplete()
    {
        var boxes = FindObjectsOfType<Box>();

        return boxes.All(box => box.isOnCross);
    }

    IEnumerator ResetSceneASync()
    {
        if (SceneManager.sceneCount > 1)
        {
            var asyncUnload = SceneManager.UnloadSceneAsync("LevelScene");
            while (!asyncUnload.isDone)
            {
                yield return null;
                Debug.Log("Unloading...");
            }
            Debug.Log("Unload Done");
            Resources.UnloadUnusedAssets();
        }
        
        var asyncLoad = SceneManager.LoadSceneAsync("LevelScene", LoadSceneMode.Additive);
        while (!asyncLoad.isDone)
        {
            yield return null;
            Debug.Log("Loading...");
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName("LevelScene"));
        LevelBuilder.Build();
        player = FindObjectOfType<Player>();
        Debug.Log("Level loaded");
        ScoreScript.MovementsCount = 0;
    }
    
    public void NextLevel()
    {
        NextButton.SetActive(false);
        LevelBuilder.NextLevel();
        StartCoroutine(ResetSceneASync());
    }

    public void ResetScene()
    {
        StartCoroutine(ResetSceneASync());
    }
}
