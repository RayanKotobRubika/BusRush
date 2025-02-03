using System;
using UnityEngine;

public class CatGroup : Obstacle
{
    [SerializeField] private int _passengerNumber;
    
    private void Start()
    {
        for (int i = 0; i < _passengerNumber; i++)
        {
            PassengerManager.Instance.SpawnPassenger(LaneManager.Instance.Lanes[0], Color, transform.position);
        }
    }
}
