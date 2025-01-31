using System;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public static ObstacleManager Instance { get; private set; }

    [field:SerializeField] public int GridWidth { get; private set; }
    [field:SerializeField] public int GridHeight { get; private set; }
    public Obstacle[,] ObstacleGrid { get; private set; }
    [field:SerializeField] public float GridCellSize { get; private set; }

    [SerializeField] private GridTile _gridTile;

    [field:SerializeField] public float ObstacleHeight { get; private set; }
    [field:SerializeField] public Transform ObstaclesParent { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;

        ObstacleGrid = new Obstacle[GridWidth, GridHeight];
    }

    private void Start()
    {
        for (int i = 0; i < GridWidth; i++)
        {
            for (int j = 0; j < GridHeight; j++)
            {
                Vector3 pos = new(((float)(-GridWidth + 1) / 2 + i)*GridCellSize, 0, ((float)(-GridHeight + 1) / 2 + j)*GridCellSize);
                GridTile tile = Instantiate(_gridTile, pos, Quaternion.identity, transform);
                tile.Coordinates = new Vector2(i, j);
            }
        }
    }
}
