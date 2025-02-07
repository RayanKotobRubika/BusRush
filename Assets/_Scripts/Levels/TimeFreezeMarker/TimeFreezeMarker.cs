using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class TimeFreezeMarker : MonoBehaviour
{
    public bool IsBroke = false;
    public float TimeToActivate;
    public Vector3 Position;
    public GameObject SpringTrapPreview;
    public GameObject ArrowSprite;
    public GameObject HandSprite;
    public GameObject HandSprite2;
    public int Type;

    private GameObject _activeSpringTrapPreview;

    private void Update()
    {
        if (Type == 0)
        {
            Ray ray = new Ray(Position, Vector3.down);
            
            if (!Physics.Raycast(ray, out RaycastHit hitInfo, 10f)) return;
            
            if (hitInfo.transform.GetComponent<Obstacle>() != null) DeactivateTimeFreeze();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
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
        
        if (_activeSpringTrapPreview != null) Destroy(_activeSpringTrapPreview);

        foreach (Passenger p in passengers)
        {
            p.MovementSpeed = LevelManager.Instance.Data.PassengersMovementSpeed;
        }

        Destroy(gameObject);
    }

    private void ActivatePreview()
    {
        if (Type == 0)
        {
            HandSprite.SetActive(true);
            ArrowSprite.SetActive(true);
            _activeSpringTrapPreview = Instantiate(SpringTrapPreview, Position - transform.position, Quaternion.identity, transform);
        }
        else
        {
            HandSprite2.SetActive(true);
        }
    }
}
