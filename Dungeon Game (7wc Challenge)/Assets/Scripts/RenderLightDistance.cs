using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderLightDistance : MonoBehaviour
{
    public GameObject player;

    void Update()
    {
        //Used to optimize the game performance by disabling lights that are outside of player's range.
        if (Vector3.Distance(player.transform.position, this.transform.position) < 40)
        {
            GetComponent<Light>().enabled = true;
        }
        else
        {
            GetComponent<Light>().enabled = false;
        }
    }
}
