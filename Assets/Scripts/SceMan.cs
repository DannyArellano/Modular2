using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceMan : MonoBehaviour
{
    public int SceneInt;
    private SceneTransition sceneTransition;

    private void Start()
    {
        sceneTransition = FindObjectOfType<SceneTransition>();
    }

    public void ChangeScene()
    {
        if (sceneTransition != null)
        {
            sceneTransition.ChangeScene(SceneInt);
        }
        else
        {
            Debug.LogError("SceneTransition script not found in the scene.");
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
