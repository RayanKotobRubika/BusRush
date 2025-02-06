using UnityEngine;

public class ChangeSceneButton : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        Time.timeScale = 1;
        StartCoroutine(SceneHandler.Instance.LoadLevel(sceneName));
    }
}
