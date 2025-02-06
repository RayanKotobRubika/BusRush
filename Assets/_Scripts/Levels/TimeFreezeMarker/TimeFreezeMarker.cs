using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class TimeFreezeMarker : MonoBehaviour
{
    public bool IsActive = false;
    public int PassengersToActivate = 0;
    public float CellSize;
    public Vector3 Position;
    public GameObject SpringTrapPreview;

    private GameObject _activeSpringTrapPreview;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out Passenger passenger)) return;

        ActivatePreview();
        
        Passenger[] passengers = PassengerManager.Instance.PassengerContainer.gameObject.GetComponentsInChildren<Passenger>();

        foreach (Passenger p in passengers)
        {
            p.MovementSpeed = 0;
        }
    }

    public void DeactivateTimeFreeze()
    {
        Passenger[] passengers = PassengerManager.Instance.PassengerContainer.gameObject.GetComponentsInChildren<Passenger>();

        foreach (Passenger p in passengers)
        {
            p.MovementSpeed = LevelManager.Instance.Data.PassengersMovementSpeed;
        }

        Destroy(gameObject);
    }

    public GameObject ActivatePreview()
    {
        return Instantiate(SpringTrapPreview, Position - transform.position, Quaternion.identity, transform);
    }
}
