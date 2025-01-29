using System;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public bool IsPlaced { get; private set; }
    [field:SerializeField] public Vector2 Dimensions { get; private set; }

    private void Awake()
    {
        IsPlaced = false;
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }
    
    
}
