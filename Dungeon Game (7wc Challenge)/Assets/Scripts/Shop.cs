using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    //Variables
    AudioSource audioPlayer;

    public AudioClip purchase;

    public GameObject shopOwner;
    public GameObject player;
    public GameObject equippedSword;

    public Camera shopCamera;
    public Camera mainCamera;
    public Camera weaponCam;

    public GameObject message;
    public GameObject shopPanel;
    public GameObject title;
    public GameObject damageValue;
    public GameObject swordDisplay;
    public GameObject priceValue;
    public Mesh[] meshes;

    public GameObject notice;
    public GameObject inventory;

    string[] weapons = { "Enhanced Cutter", "Dagger", "Iron Sword", "Katana", "Rapier", "Legendary Sword" };
    int[] prices = { 50, 100, 250, 500, 1000, 3000 };
    int[] damage = {25, 50, 75, 125, 175, 250};
    public int upgradableWeapon = 0;

    public GameObject inv;

    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Display weapon that is upgradable.
        if (inventory.GetComponent<WeaponsOwned>().weaponsOwned < weapons.Length - 1)
        {
            upgradableWeapon = inventory.GetComponent<WeaponsOwned>().weaponsOwned;
        }
        else
        {
            upgradableWeapon = weapons.Length - 1;
        }
        //Set shop UIs.
        if (inventory.GetComponent<WeaponsOwned>().weaponsOwned < weapons.Length)
        {
            title.GetComponent<UnityEngine.UI.Text>().text = weapons[upgradableWeapon];
            damageValue.GetComponent<UnityEngine.UI.Text>().text = damage[upgradableWeapon].ToString();
            priceValue.GetComponent<UnityEngine.UI.Text>().text = prices[upgradableWeapon].ToString();
            swordDisplay.GetComponent<MeshFilter>().sharedMesh = meshes[upgradableWeapon];
        }
        //Check if player reaches the max weapon tier. 
        else
        {
            title.GetComponent<UnityEngine.UI.Text>().text = "Coming soon!";
            priceValue.GetComponent<UnityEngine.UI.Text>().text = "0";
            damageValue.GetComponent<UnityEngine.UI.Text>().text = "0";
            swordDisplay.SetActive(false);
        }
        //Open shop if player is within its range.
        if (Vector3.Distance(player.transform.position, shopOwner.transform.position) < 2)
        {
            if (!shopPanel.active)
            {
                message.GetComponent<DisplayMessage>().displayMessage("[F] Open shop");
                message.GetComponent<DisplayMessage>().isDisplayingMsg = true;
            }
            else
            {
                message.GetComponent<DisplayMessage>().isDisplayingMsg = false;
            }
            
            if (Input.GetKeyDown(KeyCode.F) && !inv.active)
            {
                if (shopPanel.active)
                {
                    shopPanel.SetActive(false);
                    Cursor.lockState = CursorLockMode.Locked;
                    player.GetComponent<PlayerMovement>().enabled = true;
                    mainCamera.GetComponent<Camera>().enabled = true;
                    shopCamera.GetComponent<Camera>().enabled = false;
                    weaponCam.GetComponent<Camera>().enabled = true;
                    equippedSword.SetActive(true);
                    swordDisplay.SetActive(false);
                }
                else
                {
                    
                    shopPanel.SetActive(true);
                    Cursor.lockState = CursorLockMode.None;
                    player.GetComponent<PlayerMovement>().enabled = false;
                    mainCamera.GetComponent<Camera>().enabled = false;
                    shopCamera.GetComponent<Camera>().enabled = true;
                    weaponCam.GetComponent<Camera>().enabled = false;
                    equippedSword.SetActive(false);
                    swordDisplay.SetActive(true);
                    

                }
                
            }
        }
        else
        {
            message.GetComponent<DisplayMessage>().displayMessage("");
        }
    }
    //Upgrade weapon.
    public void upgrade()
    {

        if (player.GetComponent<Player>().coins >= prices[upgradableWeapon] && inventory.GetComponent<WeaponsOwned>().weaponsOwned < weapons.Length)
        {
            audioPlayer.PlayOneShot(purchase);
            notice.SetActive(true);
            player.GetComponent<Player>().coins -= prices[upgradableWeapon];
            inventory.GetComponent<WeaponsOwned>().weaponsOwned++;
        }

    }
}
