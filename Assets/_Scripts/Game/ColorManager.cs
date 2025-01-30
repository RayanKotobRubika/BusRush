using System;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    public static ColorManager Instance;
    
    [field:SerializeField] public Color Red { get; private set; }
    [field:SerializeField] public Color Green { get; private set; }
    [field:SerializeField] public Color Blue { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        
        Instance = this;
    }
}
