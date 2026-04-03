using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BottlecapsUI : MonoBehaviour
{
    [SerializeField] TMP_Text text;

    private void Start()
    {
        var player = FindObjectOfType<PlayerInventory>();
        player.OnBottlecapChange += UpdateBottlecaps;
        text.text = "" + player.bottlecaps;
    }

    private void UpdateBottlecaps(int bottlecapCount)
    {
        text.text = "" + bottlecapCount;
    }
}
