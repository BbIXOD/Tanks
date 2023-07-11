using UnityEngine;
using UnityEngine.UI;

public class SensitivityScroll : MonoBehaviour
{
    private Scrollbar _scrollbar;

    private void Awake()
    {
        _scrollbar = GetComponent<Scrollbar>();
        _scrollbar.value = PlayerPrefs.GetFloat(PlayerPrefsKeys.Sensitivity);
    }

    public void ChangeValue()
    {
        PlayerPrefs.SetFloat(PlayerPrefsKeys.Sensitivity, _scrollbar.value);
    }
}
