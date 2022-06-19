using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public GameObject player;
    public int damage;
    public bool doDamage = false;


    void OnTriggerEnter(Collider col)
    {
        Debug.Log("Hit!");
        if (col.gameObject.tag == "Player")
        {
            if (doDamage)
            {
                player.GetComponent<Player>().health -= damage;
            }
        }
    }

    public void dealDamage()
    {
        doDamage = true;
        
    }
    public void disableDamage()
    {
        doDamage = false;
    }
}
