using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndIronLungGameplay : MonoBehaviour
{
    [SerializeField] AudioSource mooSound;
    [SerializeField] AudioSource ironBarSound;

    public void EndGame()
    {
        StartCoroutine(EndCoroutine());
    }

    IEnumerator EndCoroutine()
    {
        yield return new WaitForSeconds(3);
        mooSound.Play();
        FindAnyObjectByType<FadeUI>().FadeOut();
        yield return new WaitForSeconds(2);
        ironBarSound.Play();
        yield return new WaitForSeconds(3);
        GetComponent<SceneChanger>().ChangeScene();
    }
}
