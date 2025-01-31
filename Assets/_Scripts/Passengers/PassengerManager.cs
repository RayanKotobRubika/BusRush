using UnityEngine;
using Random = UnityEngine.Random;

public class PassengerManager : MonoBehaviour
{
    public static PassengerManager Instance { get; private set; }
    
    [SerializeField] private Passenger[] _passengerPrefabs;
    [SerializeField] private Transform _passengerContainer;
    
    private void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this);
            return;
        } 
        
        Instance = this; 
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.P))
            SpawnPassenger(LaneManager.Instance.Lanes[0] , (PassengerColor)Random.Range(0, 3));
    }
    
    public void SpawnPassenger(Lane lane, PassengerColor color)
    {
        int index = Random.Range(0,lane.PassengerSpawnPoints.Length);

        Passenger passengerPrefab = null;
        
        foreach (Passenger p in _passengerPrefabs)
        {
            if (p.Color != color) continue;

            passengerPrefab = p;
            break;
        }
        
        Instantiate(passengerPrefab, lane.PassengerSpawnPoints[index].position, Quaternion.identity, _passengerContainer);
    }
}
