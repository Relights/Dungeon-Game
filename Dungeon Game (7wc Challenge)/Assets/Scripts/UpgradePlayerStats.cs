using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePlayerStats : MonoBehaviour
{
    public GameObject healthStat;
    public GameObject healthRegenStat;
    public GameObject movementSpeedStat;
    public GameObject skillPoints;
    public GameObject addButtons;

    public int skillPointValue;
    string skillPointValueKey = "skillPointValueKey";

    public GameObject player;
    
    void Start()
    {
        skillPointValue = PlayerPrefs.GetInt(skillPointValueKey);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetInt(skillPointValueKey, skillPointValue);
        if(skillPointValue > 0)
        {
            addButtons.SetActive(true);
        }
        else
        {
            addButtons.SetActive(false);
        }
        skillPoints.GetComponent<UnityEngine.UI.Text>().text = "Skill points: " + skillPointValue.ToString();
        healthStat.GetComponent<UnityEngine.UI.Text>().text = "Health: " + player.GetComponent<Player>().maxHealth.ToString();
        movementSpeedStat.GetComponent<UnityEngine.UI.Text>().text = "Movement speed: " + player.GetComponent<PlayerMovement>().speed.ToString("#.#") + "(Max: 5)";
        healthRegenStat.GetComponent<UnityEngine.UI.Text>().text = "Health Regen: " + player.GetComponent<Player>().healthRegen.ToString("#.#") + "/s " + "(Max: 20)";

    }
    public void addHealth()
    {
        if(skillPointValue > 0)
        {
            skillPointValue--;
            player.GetComponent<Player>().maxHealth += 10;
        }
    }
    public void addMS()
    {
        if (skillPointValue > 0 && player.GetComponent<PlayerMovement>().speed < 4.8f)
        {
            skillPointValue--;
            player.GetComponent<PlayerMovement>().speed += .2f;
            player.GetComponent<PlayerMovement>().currentSpeed += .2f;
        }
    }
    public void addHealthRegen()
    {
        if (skillPointValue > 0 && player.GetComponent<Player>().healthRegen < 20)
        {
            skillPointValue--;
            player.GetComponent<Player>().healthRegen += .2f;
        }
    }
}
