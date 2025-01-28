using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Lane : MonoBehaviour
{
    public Obstacle[] Obstacles { get; private set; }
    public Transform[] PassengerSpawnPoints { get; private set; }
    
    [field:SerializeField] public int Index { get; private set; }

    private void Awake()
    {
        Obstacles = new Obstacle[5];
        
        PassengerSpawnPoints = new Transform[3];
        
        Transform[] spawnPoints = GetComponentsInChildren<Transform>();

        for (int i = 1; i < spawnPoints.Length; i++)
        {
            PassengerSpawnPoints[i-1] = spawnPoints[i];
        }
    }

    
    
}
