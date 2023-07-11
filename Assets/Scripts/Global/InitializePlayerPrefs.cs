using System;
using UnityEngine;

public class InitializePlayerPrefs : MonoBehaviour
{
    private void Awake()
    {
        PlayerPrefs.SetFloat(PlayerPrefsKeys.Sensitivity, 0.5f);
    }
}
