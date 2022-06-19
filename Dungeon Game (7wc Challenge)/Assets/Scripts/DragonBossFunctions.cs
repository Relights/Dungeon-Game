using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class DragonBossFunctions : MonoBehaviour
{
    //Variables
    public AudioSource audioPlayer;

    public AudioClip chomp;

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

    public GameObject fireBall;
    public GameObject fireBallStartingLocation;

    private float timer = 1.5f;

    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else{
            timer = 1.5f;
        }
        
        Physics.IgnoreCollision(GetComponent<Collider>(), playerOb.GetComponent<Collider>(), true);
        can.transform.Find("DamageIndicator").GetComponent<UnityEngine.UI.Text>().text = gameObject.name + ": " + health.ToString("0") + " / " + maxHealth;
        can.transform.Find("Slider").GetComponent<UnityEngine.UI.Slider>().value = health/maxHealth;
        //Check if player is within the range
        if (Vector3.Distance(playerOb.transform.position, this.transform.position) < sightRange && health > 0)
        {
            bool shootingFireBalls = false;
            //Look at player
            Vector3 direction = playerOb.transform.position - this.transform.position;
            direction.y = 0;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 1);
            //Shoot fireball
            if(direction.magnitude >= 13 && direction.magnitude <= 40 && timer <= 0)
            {
                anim.SetBool("ShootFireBall", true);
                shootingFireBalls = true;
            }
            else
            {
                shootingFireBalls = false;
                anim.SetBool("ShootFireBall", false);
            }
            //Regen health every second.
            if(health < maxHealth)
            {
                health += 5 * Time.deltaTime;
            }
            



            //Chase player
            if (direction.magnitude > attackRange)
            {
                if(shootingFireBalls == false)
                {
                    agent.SetDestination(playerOb.transform.position);
                    anim.SetBool("Walking", true);
                    anim.SetBool("Attacking", false);
                }

            }
            else if(direction.magnitude < attackRange)
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
        //Despawn dragon if player is too far.
        if(Vector3.Distance(playerOb.transform.position, this.transform.position) > 600)
        {
            Destroy(gameObject);
        }
        
    }
    private bool killRewardsShown = false; 
    void die()
    {
        Destroy(gameObject, 20);
        anim.SetTrigger("Die");
        Destroy(anim, 5);
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

    public void shootFireBall()
    {
        Instantiate(fireBall, fireBallStartingLocation.transform.position, transform.rotation);
    }

    public void playChompingSound()
    {
        audioPlayer.PlayOneShot(chomp);
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
}
