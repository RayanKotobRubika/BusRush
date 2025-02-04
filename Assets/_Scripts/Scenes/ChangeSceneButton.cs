using UnityEngine;

public class ChangeSceneButton : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        StartCoroutine(SceneHandler.Instance.LoadLevel(sceneName));
    }
}
