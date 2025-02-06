using System;
using System.Collections;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    [field:SerializeField] public int Capacity { get; private set; }
    [field:SerializeField] public PassengerColor Color { get; private set; }
    [field:SerializeField] public bool TakesAllColors { get; private set; }
    public int CurrentPassengers { get; private set; }

    [SerializeField] private float _bouncyScaleFactor;
    [SerializeField] private float _bounceDuration;
    private Vector3 _originalScale;

    [SerializeField] private float _movementSpeed;

    [SerializeField] private TextMeshProUGUI _remainingPassengersText;

    private Coroutine _upAndDownCoroutine;
    private bool _startedCoroutine;

    private void Awake()
    {
        _originalScale = transform.localScale;
    }

    private void Start()
    {}

    private void Update()
    {
        if (transform.position == VehicleManager.Instance.VehicleLastPoint.position)
            Destroy(gameObject);

        Updatetext();
    }

    public void AddPassenger()
    {
        CurrentPassengers++;
        StartCoroutine(CoroutineUtils.BouncyScale(transform, _bouncyScaleFactor, _bounceDuration, true));
    }

    public void MoveVehicle(Vector3 targetPos)
    {
        StartCoroutine(CoroutineUtils.EaseInOutMove(transform, targetPos, _movementSpeed));
    }

    private void Updatetext()
    {
        int nb = Capacity - CurrentPassengers;
        if (nb < 0) nb = 0;
        _remainingPassengersText.text = nb.ToString();
    }
}
