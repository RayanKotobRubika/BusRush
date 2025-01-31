using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [field:SerializeField] public Camera MainCamera { get; private set; }
    
    public bool IsGameOver { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this);
            return;
        } 
        
        Instance = this;

        IsGameOver = false;
    }

    public void WinGame()
    {
        IsGameOver = true;
        Debug.Log("LEVEL COMPLETED !");
    }

    public void LoseGame()
    {
        IsGameOver = true;
        Debug.Log("LEVEL FAILED !");
    }
}
