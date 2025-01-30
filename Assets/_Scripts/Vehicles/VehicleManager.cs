using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class VehicleManager : MonoBehaviour
{
    public static VehicleManager Instance { get; private set; }
    
    [SerializeField] private TextMeshProUGUI _displayCapacityText;
    [SerializeField] private Image _capacityTextBackground;
    private bool _colorUpdated;

    [SerializeField] private float _spawnDelay;
    private float _initTimer;

    [HideInInspector] public bool BusIsActive;
    
    [field:SerializeField] public Transform[] VehiclesPositionsPoints { get; private set; }
    [field:SerializeField] public Transform VehicleLastPoint { get; private set; }
    
    [field:SerializeField] public Vehicle[] CurrentVehicles { get; private set; }
    [field:SerializeField] public List<Vehicle> VehicleList { get; private set; }

    [SerializeField] private Transform _vehicleHolder;
    
    private void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this);
            return;
        } 
        
        Instance = this;

        _colorUpdated = false;

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
        UpdateColor();

        BusIsActive = CurrentVehicles[0].transform.position == VehiclesPositionsPoints[0].position
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
            MoveAllVehicles();
    }
    
    private void UpdateText()
    {
        _displayCapacityText.text = $"{CurrentVehicles[0].CurrentPassengers} / {CurrentVehicles[0].Capacity}";
    }

    private void UpdateColor()
    {
        _capacityTextBackground.color = CurrentVehicles[0].GetComponentInChildren<MeshRenderer>().material.color;
        _colorUpdated = true;
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
        _colorUpdated = false;
        
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
    }
}
