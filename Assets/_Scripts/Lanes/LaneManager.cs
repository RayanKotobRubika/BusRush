using UnityEngine;

public class LaneManager : MonoBehaviour
{
    public static LaneManager Instance { get; private set; }
    
    [field:SerializeField] public Lane[] Lanes { get; private set; }
    
    [field:SerializeField] public Transform LanesArrivalZ { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this);
            return;
        } 
        
        Instance = this; 
    }
}
