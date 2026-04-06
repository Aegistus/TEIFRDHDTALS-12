using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FailsafeSceneManager : MonoBehaviour
{
    public Stack<string> pastScenes = new();

    static FailsafeSceneManager instance;

    private void Start()
    {
        SceneManager.activeSceneChanged += UpdateSceneStack;
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void UpdateSceneStack(Scene current, Scene next)
    {
        pastScenes.Push(current.name);
    }

    public void GoToLastScene()
    {
        if (pastScenes.Count > 0)
        {
            SceneManager.LoadScene(pastScenes.Pop());
        }
        else
        {
            Debug.LogWarning("No scenes to go back to!");
        }
    }

    // only works if there is one possible next scene option
    public void GoToNextScene()
    {
        var nextScene = FindAnyObjectByType<SceneChanger>()?.Scene;
        if (nextScene != null)
        {
            SceneManager.LoadScene(nextScene);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            GoToLastScene();
        }
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            GoToNextScene();
        }
    }
}
