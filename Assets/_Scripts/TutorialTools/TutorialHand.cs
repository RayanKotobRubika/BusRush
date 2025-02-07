using System.Collections;
using UnityEngine;

public class TutorialHand : MonoBehaviour
{
    private RectTransform _rectTransfrom;
    [SerializeField] private Vector2 _startPos;
    [SerializeField] private Vector2 _endPos;
    [SerializeField] private float _moveDuration;
    [SerializeField] private float _freezeDuration;
    
    void Start()
    {
        _rectTransfrom = GetComponent<RectTransform>();
        StartCoroutine(MoveLoop(_rectTransfrom, _startPos, _endPos, _moveDuration, _freezeDuration));
    }

    private IEnumerator MoveLoop(RectTransform target, Vector2 startPosition, Vector2 endPosition, float moveDuration, float freezeDuration)
    {
        while (true)
        {
            target.anchoredPosition = startPosition;

            float elapsedTime = 0f;
            while (elapsedTime < moveDuration)
            {
                elapsedTime += Time.deltaTime;
                float t = Mathf.SmoothStep(0, 1, elapsedTime / moveDuration);
                target.anchoredPosition = Vector2.Lerp(startPosition, endPosition, t);
                yield return null;
            }

            target.anchoredPosition = endPosition;
            yield return new WaitForSeconds(freezeDuration);
        }
    }
}
