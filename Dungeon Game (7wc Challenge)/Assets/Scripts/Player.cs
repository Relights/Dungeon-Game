using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //Variables
    public Canvas can;

    public LayerMask safeZone;

    public Transform groundCheck;

    public GameObject stats;
    public GameObject notice;

    public GameObject gate;


    //Store player data
    public float curMana;
    string manaKey = "ManaKey";
    public float mana;
    string healthKey = "HealthKey";
    public float health;
    string maxHealthKey = "MaxHealthKey";
    public float maxHealth;
    string coinKey = "CoinKey";
    public int coins;
    string xpKey = "XpKey";
    public float xp;
    string xpRequiredKey = "xpRequiredKey";
    public float xpRequired;
    string levelKey = "LevelKey";
    public int level;
    string healthRegenKey = "HealthRegenKey";
    public float healthRegen;

    void Start()
    {
        //Get player data
        mana = PlayerPrefs.GetFloat(manaKey, 100);
        healthRegen = PlayerPrefs.GetFloat(healthRegenKey, 1);
        health = (float) PlayerPrefs.GetInt(healthKey, 100);
        maxHealth = (float) PlayerPrefs.GetInt(maxHealthKey, 100);
        coins =  PlayerPrefs.GetInt(coinKey, 0);
        xp = (float) PlayerPrefs.GetInt(xpKey, 0);
        xpRequired = (float) PlayerPrefs.GetInt(xpRequiredKey, 10);
        level = PlayerPrefs.GetInt(levelKey, 1);
        health = maxHealth;
        curMana = mana;
    }

    // Update is called once per frame
    void Update()
    {
        //Regen Health and mana per second.
        regenHealth();
        regenMana();

        //Player stat uis
        can.transform.Find("Panel").Find("HealthIndicator").GetComponent<UnityEngine.UI.Text>().text = health.ToString("0") + " / " + maxHealth;
        can.transform.Find("Panel").Find("HealthBar").GetComponent<UnityEngine.UI.Slider>().value = health / maxHealth;
        can.transform.Find("Panel").Find("XpBar").GetComponent<UnityEngine.UI.Slider>().value = xp / xpRequired;
        can.transform.Find("Panel").Find("LevelIndicator").GetComponent<UnityEngine.UI.Text>().text = "Level " + level;
        can.transform.Find("Panel").Find("XpIndicator").GetComponent<UnityEngine.UI.Text>().text = xp + "/" + xpRequired.ToString("0"); ;
        can.transform.Find("Gold").Find("GoldIndicator").GetComponent<UnityEngine.UI.Text>().text = coins + "";
        can.transform.Find("Panel").Find("ManaBar").GetComponent<UnityEngine.UI.Slider>().value = curMana / mana;
        can.transform.Find("Panel").Find("ManaIndicator").GetComponent<UnityEngine.UI.Text>().text = curMana.ToString("0") + "/" + mana;
        //Store player data per frame.
        PlayerPrefs.SetFloat(manaKey, mana);
        PlayerPrefs.SetFloat(healthRegenKey, healthRegen);
        PlayerPrefs.SetInt(healthKey, (int)health);
        PlayerPrefs.SetInt(maxHealthKey, (int)maxHealth);
        PlayerPrefs.SetInt(coinKey, coins);
        PlayerPrefs.SetInt(xpKey, (int)xp);
        PlayerPrefs.SetInt(xpRequiredKey, (int)xpRequired);
        PlayerPrefs.SetInt(levelKey, level);
        //Level up.
        if (xp >= xpRequired)
        {
            levelUp();
        }

        if(health <= 0)
        {
            die();
        }
    }

    void regenHealth()
    {
        if(health < maxHealth)
        {
            health += healthRegen * Time.deltaTime;
        }
    }

    void regenMana()
    {
        if(curMana < mana)
        {
            curMana += 5 * Time.deltaTime;
        }
    }

    void takeDamage() {
        health -= 10;
    }

    void die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void levelUp()
    {
        mana += 10;
        notice.SetActive(true);
        xp = 0;
        level++;
        xpRequired = level * 10 * 1.25f;
        stats.GetComponent<UpgradePlayerStats>().skillPointValue += 1;
    }
        
}
