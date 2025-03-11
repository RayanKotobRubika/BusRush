using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;

public class ObstacleUI : MonoBehaviour
{
    [field:SerializeField] public Obstacle RelatedObstacle { get; private set; }
    [field:SerializeField] public Sprite ObstacleSprite { get; private set; }
    [field:SerializeField] public TextMeshProUGUI CostText { get; private set; }
    public Image ObstacleImage { get; private set; }

    private void Awake()
    {
        ObstacleImage = GetComponentInChildren<Image>();
        ObstacleImage.sprite = ObstacleSprite;
    }

    private void OnEnable()
    {
        StartCoroutine(CoroutineUtils.BouncyScale(transform, 1.15f, 0.15f, true));
    }

    private void Start()
    {
        CostText.text = $"{RelatedObstacle.CatBellCost}";
    }
}
