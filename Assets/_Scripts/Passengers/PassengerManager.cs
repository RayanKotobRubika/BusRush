using UnityEngine;
using Random = UnityEngine.Random;

public class PassengerManager : MonoBehaviour
{
    public static PassengerManager Instance { get; private set; }
    
    [SerializeField] private Passenger[] _passengerPrefabs;
    [field:SerializeField] public Transform PassengerContainer { get; private set; }
    
    private void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this);
            return;
        } 
        
        Instance = this; 
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
        
        Passenger passenger = Instantiate(passengerPrefab, lane.PassengerSpawnPoints[index].position, Quaternion.identity, PassengerContainer);
        StartCoroutine(CoroutineUtils.ScaleToTarget(passenger.transform, 1f, 1.1f, 0.9f, 0.2f));
    }
    
    public void SpawnPassenger(Lane lane, PassengerColor color, Vector3 position)
    {
        Passenger passengerPrefab = null;
        
        foreach (Passenger p in _passengerPrefabs)
        {
            if (p.Color != color) continue;

            passengerPrefab = p;
            break;
        }
        
        Passenger passenger = Instantiate(passengerPrefab, position, Quaternion.identity, PassengerContainer);
        StartCoroutine(CoroutineUtils.ScaleToTarget(passenger.transform, 1f, 1.1f, 0.9f, 0.2f));
    }
    
    public void SpawnPassenger(Lane lane, PassengerColor color, int position)
    {
        Passenger passengerPrefab = null;
        
        foreach (Passenger p in _passengerPrefabs)
        {
            if (p.Color != color) continue;

            passengerPrefab = p;
            break;
        }
        
        Passenger passenger = Instantiate(passengerPrefab, lane.PassengerSpawnPoints[position].position, Quaternion.identity, PassengerContainer);
        StartCoroutine(CoroutineUtils.ScaleToTarget(passenger.transform, 1f, 1.1f, 0.9f, 0.2f));
    }
}
