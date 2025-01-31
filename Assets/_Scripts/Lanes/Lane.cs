using UnityEngine;

public class Lane : MonoBehaviour
{
    public Obstacle[] Obstacles { get; private set; }
    public Transform[] PassengerSpawnPoints { get; private set; }
    
    [field:SerializeField] public int Index { get; private set; }

    private void Awake()
    {
        Obstacles = new Obstacle[5];

        Transform[] spawnPoints = GetComponentsInChildren<Transform>();
        
        PassengerSpawnPoints = new Transform[spawnPoints.Length - 1];

        for (int i = 1; i < spawnPoints.Length; i++)
        {
            PassengerSpawnPoints[i-1] = spawnPoints[i];
        }
    }
}
