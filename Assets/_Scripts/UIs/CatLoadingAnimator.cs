using UnityEngine;

public class CatLoadingAnimator : MonoBehaviour
{
    public float OriginalScale;
    public float ScaleOverdrive;
    public float Duration;
    
    public void ScaleUp()
    {
        StartCoroutine(CoroutineUtils.ScaleToTarget(transform, OriginalScale, ScaleOverdrive, 0.9f, Duration));
    }
    
    public void ScaleDown()
    {
        StartCoroutine(CoroutineUtils.ScaleToTarget(transform, 0.0001f, ScaleOverdrive, 0.1f, Duration));
    }
}
