using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "BusFrenzy/LevelData")]
public class LevelData : ScriptableObject
{
    [Header("Capacities")]
    [field:SerializeField] public int MaxRedCapacity { get; private set; }
    [field:SerializeField] public int MaxGreenCapacity { get; private set; }
    [field:SerializeField] public int MaxBlueCapacity { get; private set; }
    
    [Header("Vehicles")]
    [field:SerializeField] public List<Vehicle> VehicleList { get; private set; }
    
    [Header("Passengers")]
    [field:SerializeField] public float PassengersMovementSpeed { get; private set; }
    [field:SerializeField] public List<float> PassengersSpawnRatePerBus { get; private set; }
    [field:SerializeField] public List<Vector3> PassengersColorRatesPerBus { get; private set; }
}
