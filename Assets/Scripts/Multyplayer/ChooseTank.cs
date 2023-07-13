using UnityEngine;
using TMPro;

public class ChooseTank : MonoBehaviour
{
    private TMP_Dropdown _tanks;

    private void Awake()
    {
        _tanks = GetComponent<TMP_Dropdown>();
        SetModel();
    }

    public void SetModel()
    {
        PlayerPrefsKeys.tankModel =  _tanks.options[_tanks.value].text;
    }
}
