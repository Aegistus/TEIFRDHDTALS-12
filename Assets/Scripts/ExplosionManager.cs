using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionManager : MonoBehaviour
{
    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        StartCoroutine(ExplodeSomething());
    }

    IEnumerator ExplodeSomething()
    {
        while (true)
        {
            int randChild = Random.Range(0, transform.childCount);
            transform.GetChild(randChild).gameObject.SetActive(false);
            transform.GetChild(randChild).gameObject.SetActive(true);
            yield return new WaitForSeconds(1f);
        }
    }
}
