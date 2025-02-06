using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VehicleManager : MonoBehaviour
{
    public static VehicleManager Instance { get; private set; }

    [SerializeField] private float _spawnDelay;
    private float _initTimer;

    [HideInInspector] public bool BusIsActive;
    
    [field:SerializeField] public Transform[] VehiclesPositionsPoints { get; private set; }
    [field:SerializeField] public Transform VehicleLastPoint { get; private set; }
    
    [field:SerializeField] public Vehicle[] CurrentVehicles { get; private set; }
    [field:SerializeField] public List<Vehicle> VehicleList { get; private set; }

    [SerializeField] private Transform _vehicleHolder;

    [SerializeField] private TextMeshProUGUI _remainingVehiclesText;

    private int _remainingVehicles;
    
    private void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this);
            return;
        } 
        
        Instance = this;
        
        VehicleList = new List<Vehicle>(LevelManager.Instance.Data.VehicleList);

        _remainingVehicles = VehicleList.Count;

        CurrentVehicles = new Vehicle[VehiclesPositionsPoints.Length];
        BusIsActive = false;
    }

    private void Start()
    {
        _initTimer = _spawnDelay;
        SpawnFirstVehicle();
    }

    private void Update()
    {
        if (CurrentVehicles[0] == null)
        {
            if (!GameManager.Instance.IsGameOver && VehicleList.Count == 0)
                GameManager.Instance.WinGame();
            else
                return;
        }
        
        UpdateText();

        BusIsActive = (CurrentVehicles[0].transform.position - VehiclesPositionsPoints[0].position).sqrMagnitude < 2
                      && CurrentVehicles[0].CurrentPassengers < CurrentVehicles[0].Capacity;
        
        if (CurrentVehicles[^1] == null && VehicleList.Count > 0)
        {
            _initTimer -= Time.deltaTime;

            if (_initTimer <= 0)
            {
                _initTimer = _spawnDelay;
                CurrentVehicles[^1] = SpawnVehicle();
                MoveVehicleToFirstFreeSpot();
            }
        }
        else
            _initTimer = _spawnDelay;

        if (CurrentVehicles[0].CurrentPassengers >= CurrentVehicles[0].Capacity && CurrentVehicles[0] != null)
        {
            MoveAllVehicles();
            _remainingVehicles--;
        }
    }
    
    private void UpdateText()
    {
        _remainingVehiclesText.text = $"Remaining Vehicles : {_remainingVehicles}";
    }

    private Vehicle SpawnVehicle()
    {
        Vehicle vehicle = Instantiate(VehicleList[0], VehiclesPositionsPoints[^1].position, Quaternion.identity, _vehicleHolder);
        VehicleList.RemoveAt(0);
        return vehicle;
    }

    private void SpawnFirstVehicle()
    {
        CurrentVehicles[0] = SpawnVehicle();
        CurrentVehicles[0].MoveVehicle(VehiclesPositionsPoints[0].position);
    }

    private void MoveVehicleToFirstFreeSpot()
    {
        for (int i = 0; i < CurrentVehicles.Length; i++)
        {
            if (CurrentVehicles[i] != null) continue;

            CurrentVehicles[i] = CurrentVehicles[^1];
            CurrentVehicles[i].MoveVehicle(VehiclesPositionsPoints[i].position);
            CurrentVehicles[^1] = null;
            return;
        }
    }

    private void MoveAllVehicles()
    {
        for (int i = 0; i < CurrentVehicles.Length; i++)
        {
            if (CurrentVehicles[i] == null) continue;
            
            if (i == 0)
            {
                CurrentVehicles[i].MoveVehicle(VehicleLastPoint.position);
                CurrentVehicles[i] = null;
            }
            else
            {
                CurrentVehicles[i].MoveVehicle(VehiclesPositionsPoints[i - 1].position);
                CurrentVehicles[i - 1] = CurrentVehicles[i];
                CurrentVehicles[i] = null;
            }
        }

        if (VehicleList.Count > 0)
            CurrentVehicles[^1] = SpawnVehicle();
        
        if (CurrentVehicles[0] != null)
            LevelManager.Instance.ChangeRateAndOdds(CurrentVehicles[0]);
    }
}
