using UnityEngine;
using UnityEngine.UI;

public class ChangeSceneButton : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        GetComponent<Button>().interactable = false;
        Time.timeScale = 1;
        StartCoroutine(SceneHandler.Instance.LoadLevel(sceneName));
    }
}
