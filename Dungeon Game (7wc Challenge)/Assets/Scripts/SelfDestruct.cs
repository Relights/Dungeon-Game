using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    // Start is called before the first frame update
    public float secondsBeforeDestroy;
    void Update()
    {
        Destroy(gameObject, secondsBeforeDestroy);  
    }

}
