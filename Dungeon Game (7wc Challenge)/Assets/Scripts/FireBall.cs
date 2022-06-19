using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public GameObject exp;
    public float radius;

    void Update()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * 3000f);
    }


    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag != "DragonBoss")
        {
            GameObject expClone = Instantiate(exp, transform.position, transform.rotation);
            Destroy(expClone, 3);
            damage();
            Destroy(gameObject);
        }

    }

    void damage()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach(Collider col in colliders)
        {
            Player player = col.GetComponent<Player>();
            if(player != null)
            {
                player.GetComponent<Player>().health -= 10;
            }
        }
    }

}
