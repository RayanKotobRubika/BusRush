using System;
using UnityEngine;

public class Lane : MonoBehaviour
{
    private Obstacle[] _obstacles;
    private Transform[] _passengerSpawnPoints;
    
    [field:SerializeField] public int Index { get; private set; }

    private void Awake()
    {
        int obstaclesPerLane = LaneManager.Instance.ObstaclesPerLane;
        _obstacles = new Obstacle[obstaclesPerLane];
        
        _passengerSpawnPoints = GetComponentsInChildren<Transform>();
    }
    
    
}
