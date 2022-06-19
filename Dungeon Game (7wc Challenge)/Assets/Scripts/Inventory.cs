using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject sword;
    public GameObject shopPanel;
    public GameObject damageDescription;

    public GameObject notice;

    public Button next;
    public GameObject inventoryUI;
    public Mesh[] swordMeshes;

    public int selectedWeapon = 0;
    string selectedWeaponKey = "selectedWeaponKey";
    string[] weapons = { "Cutter", "Enhanced Cutter", "Dagger", "Iron Sword", "Katana", "Rapier", "Legendary Sword" };
    int[] damage = {10, 25, 50, 75, 125, 175, 250};

    void Start()
    {
       selectedWeapon = PlayerPrefs.GetInt(selectedWeaponKey, 0);
    }


    void Update()
    {
        //Set UIs
        inventoryUI.transform.Find("CurrentWeaponIndicator").GetComponent<UnityEngine.UI.Text>().text = weapons[selectedWeapon].ToString();
        damageDescription.GetComponent<UnityEngine.UI.Text>().text = "Damage: " + damage[selectedWeapon];
        
        //Press e to open/close inventory
        if (Input.GetKeyDown(KeyCode.E) && !shopPanel.active)
        {
            openCloseInventory();
        }
        //Set Damage
        sword.GetComponent<Sword>().swordDamage = damage[selectedWeapon];
        //Set sword mesh
        sword.GetComponent<MeshFilter>().sharedMesh = swordMeshes[selectedWeapon];

    }

    public void nextWeapon()
    {
        if(selectedWeapon < weapons.Length-1 && selectedWeapon < GetComponent<WeaponsOwned>().weaponsOwned)
        {
            selectedWeapon++;
            PlayerPrefs.SetInt(selectedWeaponKey, selectedWeapon);
        }
        
    }
    public void previousWeapon()
    {
        if(selectedWeapon > 0)
        {
            selectedWeapon--;
            PlayerPrefs.SetInt(selectedWeaponKey, selectedWeapon);
            
        }
        
    }
    void openCloseInventory()
    {
        if(inventoryUI.active)
        {
            inventoryUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            notice.SetActive(false);
            inventoryUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
