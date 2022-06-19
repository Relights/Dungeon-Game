using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphicsSettings : MonoBehaviour
{
    //Variables
    public int currentQualityIndex;
    string currentQualityIndexKey = "CurrentQualityIndexKey";
    void Start()
    {
        GetComponent<Dropdown>().value = PlayerPrefs.GetInt(currentQualityIndexKey, 1);
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt(currentQualityIndexKey, 1));
    }
    public void SetQuality(int qualityIndex)
    {
        currentQualityIndex = qualityIndex;
        QualitySettings.SetQualityLevel(currentQualityIndex);
        PlayerPrefs.SetInt(currentQualityIndexKey, currentQualityIndex);
    }
}
