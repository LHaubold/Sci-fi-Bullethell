using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LvlScript : MonoBehaviour
{
    [SerializeField]
    GameObject[] Lvl;
    GameObject Spawner;
    int spd = 1;
    int currentLvl;
    [SerializeField]
    List<GameObject> TurretPositions;
    [SerializeField]
    List<GameObject> ufoPositions;

    public int SetCurrentLvl
    {
        set { currentLvl = value; }
    }

    bool spawnLvl = false;

    void Start()
    {
        Spawner = GameObject.Find("Spawner");
        Lvl = Resources.LoadAll<GameObject>("Prefabs/Lvl");
        for(int i = 0; i <= transform.GetChild(0).transform.childCount - 1; i++)
        {
            TurretPositions.Add(gameObject.transform.GetChild(0).transform.GetChild(i).gameObject);
        }
        for(int k = 0; k <= TurretPositions.Count -1; k++)
        {
            GameObject go = Instantiate(Resources.Load<GameObject>("Prefabs/Turret"));
            go.transform.position = TurretPositions[k].transform.position;
            go.transform.parent = TurretPositions[k].transform;
        }
        for (int n = 0; n <= transform.GetChild(1).transform.childCount - 1; n++)
        {
            ufoPositions.Add(gameObject.transform.GetChild(1).transform.GetChild(n).gameObject);
        }
        for (int m = 0; m <= ufoPositions.Count - 1; m++)
        {
            GameObject go2 = Instantiate(Resources.Load<GameObject>("Prefabs/Ufo"));
            go2.transform.position = ufoPositions[m].transform.position;
            go2.transform.parent = ufoPositions[m].transform;
        }
    }


    void Update()
    {
        transform.position = new Vector3(transform.position.x - Time.deltaTime * spd, transform.position.y);

        if (spawnLvl == false)
        {
            if (transform.position.x <= -5.5f)
            {
                spawnLvl = true;
                int b = Random.Range(0, Lvl.Length * 10);
                b = b % Lvl.Length;
                //if (b == currentLvl)
                //{
                //    for (int i = 0; b == currentLvl; i++)
                //    {
                //        b = Random.Range(0, Lvl.Length - 1);
                //    }
                //}
                GameObject goLvl = Instantiate(Lvl[b], Spawner.transform) as GameObject;
                goLvl.name = Lvl[b].name;
                goLvl.transform.position = Spawner.transform.position;
                if (goLvl.GetComponent<AsteroidLvlScripts>() != null)
                {
                    goLvl.GetComponent<AsteroidLvlScripts>().SetCurrentLvl = b;
                }
                else
                {
                    goLvl.GetComponent<LvlScript>().SetCurrentLvl = b;
                }
            }
        }


        if (transform.position.x <= -25)
        {
            Destroy(gameObject);
        }
    }
}
