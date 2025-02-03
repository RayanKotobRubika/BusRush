using System.Collections;
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

    private void Awake()
    {
        _originalScale = transform.localScale;
    }

    private void Update()
    {
        if (transform.position == VehicleManager.Instance.VehicleLastPoint.position)
            Destroy(gameObject);

        Updatetext();
    }

    public void AddPassenger()
    {
        CurrentPassengers++;
        StartCoroutine(BouncyScale());
    }

    public void MoveVehicle(Vector3 targetPos)
    {
        StartCoroutine(EaseInOutMove(targetPos, _movementSpeed));
    }

    private void Updatetext()
    {
        int nb = Capacity - CurrentPassengers;
        if (nb < 0) nb = 0;
        _remainingPassengersText.text = nb.ToString();
    }
    
    private IEnumerator BouncyScale()
    {
        Vector3 targetScale = new Vector3(_bouncyScaleFactor, _bouncyScaleFactor, _bouncyScaleFactor);
        float halfDuration = _bounceDuration / 2f;
        
        float elapsed = 0f;
        while (elapsed < halfDuration)
        {
            transform.localScale = Vector3.Lerp(_originalScale, targetScale, elapsed / halfDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localScale = targetScale;

        elapsed = 0f;
        while (elapsed < halfDuration)
        {
            transform.localScale = Vector3.Lerp(targetScale, _originalScale, elapsed / halfDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localScale = _originalScale;
    }
    
    private IEnumerator EaseInOutMove(Vector3 targetPosition, float speed)
    {
        Vector3 startPosition = transform.position;
        float distance = Vector3.Distance(startPosition, targetPosition);
        float duration = distance / speed;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);

            t = Mathf.SmoothStep(0, 1, t);

            transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null;
        }

        transform.position = targetPosition;
    }
}
