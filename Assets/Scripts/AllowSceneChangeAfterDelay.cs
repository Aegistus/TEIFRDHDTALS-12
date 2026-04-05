using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllowSceneChangeAfterDelay : MonoBehaviour
{
    [SerializeField] float delay = 5f;
    [SerializeField] GameObject prompt;

    bool canSkip = false;

    private void Start()
    {
        StartCoroutine(Delay());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canSkip)
        {
            GetComponent<SceneChanger>().ChangeScene();
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(delay);
        prompt.SetActive(true);
        canSkip = true;
    }
}
