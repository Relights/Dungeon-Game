using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMonsters : MonoBehaviour
{
    //Variables
    public GameObject goblin;
    public GameObject skeleton;
    public GameObject armouredSkeleton;

    public GameObject skeletonBoss;
    public GameObject dragon;

    public Transform player;
    public Camera camera;

    public float timer;
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1)
        {
            //Spawn goblins
            if (transform.Find("GoblinSpawner").transform.childCount < 15 && Vector3.Distance(player.position, this.transform.position) < 20)
            {
                int randomX = Random.Range(0, 20);
                int randomZ = Random.Range(0, 14);
                GameObject spawnedGoblin = Instantiate(goblin, new Vector3(randomX, 0, randomZ), Quaternion.identity);
                spawnedGoblin.name = "Goblin";
                spawnedGoblin.GetComponent<EnemyFunctions>().enabled = true;
                spawnedGoblin.transform.parent = transform.Find("GoblinSpawner").transform;
            }
            //Spawn skeletons
            if (transform.Find("SkeletonSpawner").transform.childCount < 10 && Vector3.Distance(player.position, transform.Find("SkeletonSpawner").transform.position) < 70)
            {
                int randomX_S = Random.Range(23, -4);
                int randomZ_S = Random.Range(36, 94);
                GameObject spawnedSkeleton = Instantiate(skeleton, new Vector3(randomX_S, 0, randomZ_S), Quaternion.identity);
                spawnedSkeleton.name = "Skeleton";
                spawnedSkeleton.GetComponent<EnemyFunctions>().enabled = true;
                spawnedSkeleton.transform.parent = transform.Find("SkeletonSpawner").transform;
            }
            //Spawn armoured skeletons
            if (transform.Find("Skeleton2Spawner").transform.childCount < 30 && Vector3.Distance(player.position, transform.Find("Skeleton2Spawner").transform.position) < 200)
            {
                int randomX_S2 = Random.Range(153, 214);
                int randomZ_S2 = Random.Range(7, 140);
                GameObject spawnedSkeleton2 = Instantiate(armouredSkeleton, new Vector3(randomX_S2, -2, randomZ_S2), Quaternion.identity);
                spawnedSkeleton2.name = "Armoured Skeleton";
                spawnedSkeleton2.GetComponent<EnemyFunctions>().enabled = true;
                spawnedSkeleton2.transform.parent = transform.Find("Skeleton2Spawner").transform;
            }
            //Spawn skeleton bosses
            if (transform.Find("SkeletonBossSpawner").transform.childCount < 3 && Vector3.Distance(player.position, transform.Find("SkeletonBossSpawner").transform.position) < 200)
            {
                int randomZ_SB = Random.Range(50, 89);
                GameObject spawnedSkeletonBoss = Instantiate(skeletonBoss, new Vector3(70, -2, randomZ_SB), Quaternion.identity);
                spawnedSkeletonBoss.name = "Skeleton Boss";
                spawnedSkeletonBoss.GetComponent<EnemyBossFunctions>().enabled = true;
                spawnedSkeletonBoss.transform.parent = transform.Find("SkeletonBossSpawner").transform;
            }
            //Spawn dragon
            if (transform.Find("DragonBossSpawner").transform.childCount < 1 && Vector3.Distance(player.position, transform.Find("DragonBossSpawner").transform.position) < 200)
            {
                GameObject spawnedSkeletonBoss = Instantiate(dragon, new Vector3(287, -12.77f, 72), Quaternion.identity);
                spawnedSkeletonBoss.name = "Dragon";
                spawnedSkeletonBoss.GetComponent<DragonBossFunctions>().enabled = true;
                spawnedSkeletonBoss.transform.parent = transform.Find("DragonBossSpawner").transform;
            }

        }
    }
}
