using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Sword : MonoBehaviour
{
    //Variables
    public GameObject groundCheck;
    public LayerMask groundMask;

    public AudioClip walk;
    public AudioClip woosh;
    public AudioClip hit;
    public AudioClip ability1;
    public AudioClip ability2;

    public Animator anim;

    public ParticleSystem particle;

    public float swordDamage;
    public float currentSwordDamage;

    public TrailRenderer trail;

    public bool usedAttack1;

    public GameObject inv;
    public GameObject shopPanel;

    public GameObject ability1Image;
    public float ability1Cd = 2.5f;
    bool coolDown1 = false;

    public GameObject ability2Image;
    public float ability2Cd = 2.5f;
    bool coolDown2 = false;

    public GameObject player;

    AudioSource audioPlayer;

    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Ability cooldowns
        if (coolDown1)
        {        
            ability1Image.GetComponent<UnityEngine.UI.Image>().fillAmount -= 1 / ability1Cd * Time.deltaTime;
            if(ability1Image.GetComponent<UnityEngine.UI.Image>().fillAmount <= 0)
            {
                coolDown1 = false;
                ability1Image.GetComponent<UnityEngine.UI.Image>().fillAmount = 0;
            }
        }
        if (coolDown2)
        {
            ability2Image.GetComponent<UnityEngine.UI.Image>().fillAmount -= 1 / ability2Cd * Time.deltaTime;
            if (ability2Image.GetComponent<UnityEngine.UI.Image>().fillAmount <= 0)
            {
                coolDown2 = false;
                ability2Image.GetComponent<UnityEngine.UI.Image>().fillAmount = 0;
            }
        }

        //Sword animations 
        if (!inv.active && !shopPanel.active)
        {
            if (Input.GetMouseButtonDown(0))
            {
                anim.speed = 1;
                anim.SetBool("Attack1", true);
                usedAttack1 = true;
            }
            else if (Input.GetKeyDown(KeyCode.Z) && player.GetComponent<Player>().curMana >= 30 && coolDown1 == false && ability2Image.GetComponent<UnityEngine.UI.Image>().fillAmount <= .6f )
            {
                coolDown1 = true;
                ability1Image.GetComponent<UnityEngine.UI.Image>().fillAmount = 1;
                anim.SetBool("Ability1", true);
                player.GetComponent<Player>().curMana -= 30;
                anim.speed = 1;
            }
            else if (Input.GetKeyDown(KeyCode.X) && player.GetComponent<Player>().curMana >= 50 && coolDown2 == false && ability1Image.GetComponent<UnityEngine.UI.Image>().fillAmount <= .6f)
            {
                coolDown2 = true;
                ability2Image.GetComponent<UnityEngine.UI.Image>().fillAmount = 1;
                anim.SetBool("Ability2", true);
                player.GetComponent<Player>().curMana -= 50;
                anim.speed = 1;
            }
            else
            {
                anim.SetBool("Ability2", false);
                anim.SetBool("Ability1", false);
                anim.SetBool("Attack1", false);
                anim.SetBool("Attack2", false);
            }


            //Walking Animation
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S))
            {       
                anim.SetBool("Walking", true);
            }
            else if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                anim.speed = 2;
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                anim.speed = 1;
            }
            else if(!Input.anyKey)
            {
                anim.SetBool("Walking", false);
            }

            
        }
    }
    //Check if player hits an enemy.
    void OnTriggerEnter(Collider col)
    {
        AudioSource hittingAudio = GetComponent<AudioSource>();
        if (col.gameObject.tag == "Enemy")
        {
            hittingAudio.PlayOneShot(hit);
            col.gameObject.GetComponent<EnemyFunctions>().health -= currentSwordDamage;
            Instantiate(particle, transform.position, Quaternion.identity);
            GetComponent<Collider>().enabled = false;
        }
        else if (col.gameObject.tag == "EnemyBoss")
        {
            hittingAudio.PlayOneShot(hit);
            col.gameObject.GetComponent<EnemyBossFunctions>().health -= currentSwordDamage;
            Instantiate(particle, transform.position, Quaternion.identity);
            GetComponent<Collider>().enabled = false;
        }
        else if (col.gameObject.tag == "DragonBoss")
        {
            hittingAudio.PlayOneShot(hit);
            col.gameObject.GetComponent<DragonBossFunctions>().health -= currentSwordDamage;
            Instantiate(particle, transform.position, Quaternion.identity);
            GetComponent<Collider>().enabled = false;
        }
    }

    public void activateDamage()
    {
        currentSwordDamage = swordDamage;
        GetComponent<Collider>().enabled = true;
        trail.enabled = true;
        if (usedAttack1)
        {
            usedAttack1 = false;
        }
    }

    public void deactivateDamage()
    {
        GetComponent<Collider>().enabled = false;
        trail.enabled = false;
        currentSwordDamage = swordDamage;
    }

    public void playWalkingSound()
    {
        audioPlayer.PlayOneShot(walk);
    }
    public void playSlashingSound()
    {
        audioPlayer.PlayOneShot(woosh);
    }

    public void playAbility1Sound()
    {
        audioPlayer.PlayOneShot(ability1);
    }

    public void playAbility2Sound()
    {
        audioPlayer.PlayOneShot(ability2);
    }
}
