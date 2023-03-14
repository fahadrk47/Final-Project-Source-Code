using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_manager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] Enemy_1;
    public GameObject Enemy_2;
    public GameObject player;
    public int[] wave_Enemies;
    public int[] locations;

    public static bool add_wave;
    public int wave_number = 0;
    public GameObject Enemies;
    private int j;
    public int level_enemies;
    public static int Enemies_destroy;

    void Start()
    {
        add_wave = false;

    }

    // Update is called once per frame
    void Update()
    {

        //float temp = Enemies.transform.childCount;
        if (add_wave == true & Enemies.transform.childCount == 0)
        {
            for (int m = 0; m < locations.Length; m++)
            {
                locations[m] = (3 * m) - 3;
            }
            if (Enemies_destroy == level_enemies)
            {
                spawn_enemy(Enemy_2, new Vector3(0, -0.63f, player.transform.position.z + 15));
            }
            else
            {
                for (int i = 0; i < wave_Enemies[wave_number]; i++)
                {
                    if (Enemy_1[0].name == "boss_1" || Enemy_1[0].name == "boss_2")
                    {
                        spawn_enemy(Enemy_1[0], new Vector3(0, 0, player.transform.position.z + 10));
                    }
                    else
                    {
                        do
                        {
                            j = Random.Range(0, locations.Length);
                        }
                        while (locations[j] == 10);



                        spawn_enemy(Enemy_1[Random.Range(0, 3)], new Vector3(locations[j], 0, player.transform.position.z + 6 + Random.Range(0, 3)));
                        locations[j] = 10;
                    }


                }

            }
            //print("init");

            wave_number++;
            add_wave = false;
        }

    }
    void spawn_enemy(GameObject enemy_, Vector3 pos_)
    {

        var temp = Instantiate(enemy_, pos_, Enemies.transform.rotation);
        temp.transform.parent = Enemies.transform;
    }
    IEnumerator delayCoroutine()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(Random.Range(1, 3));

    }
}
