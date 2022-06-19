using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsOwned : MonoBehaviour
{
    public int weaponsOwned;
    string weaponsOwnedKey = "WeaponsOwnedKey7";
    // Start is called before the first frame update
    void Start()
    {
        weaponsOwned = PlayerPrefs.GetInt(weaponsOwnedKey, 0);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetInt(weaponsOwnedKey, weaponsOwned);
    }
}
