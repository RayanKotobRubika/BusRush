using System.Collections;
using UnityEngine;

public static class CoroutineUtils
{
    public static IEnumerator BouncyScale(Transform target, float scaleFactor, float duration)
    {
        Vector3 originalScale = target.localScale;
        Vector3 targetScale = originalScale * scaleFactor;
        float halfDuration = duration / 2f;
        
        float elapsed = 0f;
        while (elapsed < halfDuration)
        {
            target.localScale = Vector3.Lerp(originalScale, targetScale, elapsed / halfDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        target.localScale = targetScale;

        elapsed = 0f;
        while (elapsed < halfDuration)
        {
            target.localScale = Vector3.Lerp(targetScale, originalScale, elapsed / halfDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        target.localScale = originalScale;
    }

    public static IEnumerator EaseInOutMove(Transform target, Vector3 targetPosition, float speed)
    {
        Vector3 startPosition = target.position;
        float distance = Vector3.Distance(startPosition, targetPosition);
        float duration = distance / speed;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            t = Mathf.SmoothStep(0, 1, t);
            target.position = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null;
        }

        target.position = targetPosition;
    }
    
    public static IEnumerator EaseInOutMoveUI(RectTransform target, Vector3 targetPosition, float duration)
    {
        Vector3 startPosition = target.anchoredPosition;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            t = Mathf.SmoothStep(0, 1, t);
            target.anchoredPosition = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null;
        }

        target.anchoredPosition = targetPosition;
    }

    public static IEnumerator ScaleToTarget(Transform target, float targetScale, float scaleOverdrive, float scaleCut, float duration)
    {
        Vector3 startingScale = target.localScale;
        float elapsedTime = 0f;

        while (elapsedTime < duration * scaleCut)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            t = Mathf.SmoothStep(0, 1, t);
            target.localScale = Vector3.Lerp(startingScale, Vector3.one * scaleOverdrive, t);
            yield return null;
        }
        
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            t = Mathf.SmoothStep(0, 1, t);
            target.localScale = Vector3.Lerp(startingScale, Vector3.one * targetScale, t);
            yield return null;
        }

        target.localScale = targetScale * Vector3.one;
    }

    public static IEnumerator FadeInCanvaGroup(CanvasGroup target, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            target.alpha = Mathf.Lerp(0, 1, t);
            yield return null;
        }

        target.alpha = 1;
    }
    
    public static IEnumerator FadeOutCanvaGroup(CanvasGroup target, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            target.alpha = Mathf.Lerp(1, 0, t);
            yield return null;
        }

        target.alpha = 0;
    }
}