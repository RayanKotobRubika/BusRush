using System;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    public static ColorManager Instance;
    
    [field:SerializeField] public Color Red { get; private set; }
    [field:SerializeField] public Color Green { get; private set; }
    [field:SerializeField] public Color Blue { get; private set; }
    [field:SerializeField] public Color Default { get; private set; }
    
    public Dictionary<PassengerColor, Color> Colors { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        
        Instance = this;

        Colors = new Dictionary<PassengerColor, Color>
        {
            { PassengerColor.Red, Red },
            { PassengerColor.Green, Green },
            { PassengerColor.Blue, Blue },
            { PassengerColor.Default, Default}
        };
    }
}
