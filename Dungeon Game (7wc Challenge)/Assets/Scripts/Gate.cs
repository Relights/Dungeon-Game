using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{

    public GameObject dungeonGDetector;
    public GameObject lobbyGDetector;



    public GameObject toDungeon;
    public GameObject toLobby;


    public GameObject message;

    public GameObject player;

    public bool isInLobby = true;

    void Update()
    {
        Vector3 direction = player.transform.position - lobbyGDetector.transform.position;
        Vector3 dir = player.transform.position - dungeonGDetector.transform.position;
        if (direction.magnitude <= 3)
        {
            message.GetComponent<DisplayMessage>().displayMessage("[F] Enter Dungeon");
            message.GetComponent<DisplayMessage>().isDisplayingMsg = true;
        }
        else if(isInLobby == true && (dir.magnitude <= 15 && dir.magnitude >= 3))
        {
            message.GetComponent<DisplayMessage>().isDisplayingMsg = false;
        }
        if(dir.magnitude <= 3)
        {
            message.GetComponent<DisplayMessage>().displayMessage("[F] Enter Lobby");
            message.GetComponent<DisplayMessage>().isDisplayingMsg = true;
        }
        else if (isInLobby == false && (dir.magnitude <= 15 && dir.magnitude >= 3))
        {
            message.GetComponent<DisplayMessage>().isDisplayingMsg = false;
        }



            if (Input.GetKeyDown(KeyCode.F))
        {
            if(isInLobby == true)
            {

                if (direction.magnitude <= 3)
                {
                    
                    player.GetComponent<CharacterController>().enabled = false;
                    player.transform.position = toDungeon.transform.position;
                    player.GetComponent<CharacterController>().enabled = true;
                    isInLobby = false;
                }
            }
            else
            {

                if(dir.magnitude <= 3)
                {
                    
                    player.GetComponent<CharacterController>().enabled = false;
                    player.transform.position = toLobby.transform.position;
                    player.GetComponent<CharacterController>().enabled = true;
                    isInLobby = true;
                }
            }

        }

    }


}
