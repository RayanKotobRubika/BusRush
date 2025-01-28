using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PassengerManager : MonoBehaviour
{
    [SerializeField] private Passenger[] _passengerPrefabs;
    [SerializeField] private Transform _passengerContainer;
    
    public void SpawnPassenger(Lane lane, PassengerColor color)
    {
        int index = Random.Range(0, lane.PassengerSpawnPoints.Length);

        Passenger passengerPrefab = null;
        
        foreach (Passenger p in _passengerPrefabs)
        {
            if (p.AssignedColor != color) continue;

            passengerPrefab = p;
            break;
        }
        
        Passenger passenger = Instantiate(passengerPrefab, lane.PassengerSpawnPoints[index].position, 
            Quaternion.identity, _passengerContainer);
        passenger.Initialize(lane);
    }

    private float _timer = 1f;
    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer > 0) return;
        _timer = 1f;
        SpawnPassenger(LaneManager.Instance.Lanes[Random.Range(0, 3)] , (PassengerColor)Random.Range(0, 3));
    }
}
