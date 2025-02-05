using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [field:SerializeField] public Camera MainCamera { get; private set; }
    
    [field:SerializeField] public GameObject WinScreen { get; private set; }
    [field:SerializeField] public GameObject LoseScreen { get; private set; }

    [SerializeField] private float _bouncyScale;
    [SerializeField] private float _bounceDuration;
    
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
    
    public void BounceButton(Transform buttonTransform)
    {
        StopAllCoroutines();
        StartCoroutine(CoroutineUtils.BouncyScale(buttonTransform, _bouncyScale, _bounceDuration));
    }

    public void WinGame()
    {
        int currentLevel = PlayerPrefs.GetInt("CurrentLevel");
        PlayerPrefs.SetInt("CurrentLevel", currentLevel + 1);
        
        IsGameOver = true;

        ShowWinScreen();
    }

    public void LoseGame()
    {
        IsGameOver = true;

        ShowLoseScreen();
    }

    private void ShowWinScreen()
    {
        WinScreen.SetActive(true);
        
        Debug.Log("LEVEL COMPLETED !");
    }

    private void ShowLoseScreen()
    {
        LoseScreen.SetActive(true);
        
        Debug.Log("LEVEL FAILED !");
    }
}
