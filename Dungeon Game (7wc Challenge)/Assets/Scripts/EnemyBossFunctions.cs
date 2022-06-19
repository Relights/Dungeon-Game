using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyBossFunctions : MonoBehaviour
{
    AudioSource audioPlayer;

    public AudioClip walking;
    public AudioClip attacking;

    public Animator anim;

    public GameObject playerOb;

    public GameObject weapon;

    public Canvas can;

    public Canvas playerCan;

    public float health;

    public float maxHealth;

    public float giveXp;

    public int loot;

    public int sightRange;

    public float speed;

    public int attackRange;

    NavMeshAgent agent;

    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
        setRigidbodyState(true);
        setColliderState(false);
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Physics.IgnoreCollision(GetComponent<Collider>(), playerOb.GetComponent<Collider>(), true);
        can.transform.Find("DamageIndicator").GetComponent<UnityEngine.UI.Text>().text = gameObject.name + ": " + health + " / " + maxHealth;
        can.transform.Find("Slider").GetComponent<UnityEngine.UI.Slider>().value = health/maxHealth;
        //Check if player is within range
        if (Vector3.Distance(playerOb.transform.position, this.transform.position) < sightRange && health > 0)
        {
            //Look at player
            Vector3 direction = playerOb.transform.position - this.transform.position;
            direction.y = 0;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
            //Chase player
            if (direction.magnitude > attackRange)
            {
                //Pathfinding
                agent.SetDestination(playerOb.transform.position);
                //Running animations
                anim.SetBool("Walking", true);
                anim.SetBool("Attacking", false);
            }
            else
            {
                anim.SetBool("Walking", false);
                anim.SetBool("Attacking", true);
            }
        }
        else
        {
            anim.SetBool("Walking", false);
            anim.SetBool("Attacking", false);
        }
        if(health <= 0)
        {
            die();
        }
        //Performance wise, if the player is too far from the enemy, the enemy will despawn.
        if(Vector3.Distance(playerOb.transform.position, this.transform.position) > 600)
        {
            Destroy(gameObject);
        }
        
    }
    //Die function
    private bool killRewardsShown = false; 
    void die()
    {
        Destroy(weapon);
        Destroy(gameObject, 5);
        Destroy(anim);
        setRigidbodyState(false);
        setColliderState(true);
        Destroy(GetComponent<Collider>());
        Destroy(GetComponent<Rigidbody>());
        playerOb.GetComponent<Player>().xp += giveXp;
        playerOb.GetComponent<Player>().coins += loot;
        can.enabled = false;       
        if(killRewardsShown == false)
        {
            displayKillRewards();
        }

    }

    public void activateDamage()
    {
        weapon.GetComponent<Collider>().enabled = true;
    }
    public void deactivateDamage()
    {
        weapon.GetComponent<Collider>().enabled = false;
    }

    //Display kill rewards.
    public GameObject killRewards;
    void displayKillRewards()
    {
        GameObject cloneRewardsUI = Instantiate(killRewards, transform.position , transform.rotation) as GameObject;
        cloneRewardsUI.transform.SetParent(playerCan.transform, false);
        cloneRewardsUI.transform.Find("CoinReward").GetComponent<UnityEngine.UI.Text>().text = "+ " + loot;
        cloneRewardsUI.transform.Find("XpReward").GetComponent<UnityEngine.UI.Text>().text = "+ " + giveXp + " xp";
        killRewardsShown = true;
    }

    void setRigidbodyState(bool state)
    {
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();

        foreach(Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = state;
        }
        GetComponent<Rigidbody>().isKinematic = !state;
    }
    void setColliderState(bool state)
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();

        foreach(Collider collider in colliders)
        {
            collider.enabled = state;
        }
        GetComponent<Collider>().enabled = !state;
    }
    public void playWalkingSound()
    {
        audioPlayer.PlayOneShot(walking);
    }
    public void playAttackingSound()
    {
        audioPlayer.PlayOneShot(attacking);
    }
}
