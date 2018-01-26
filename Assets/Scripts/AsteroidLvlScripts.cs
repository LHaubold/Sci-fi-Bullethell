using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class AsteroidLvlScripts : MonoBehaviour
{
    [SerializeField]
    List<Transform> Positions;
    [SerializeField]
    GameObject[] Asteroids;
    [SerializeField]
    GameObject[] Lvl;
    GameObject Spawner;
    int spd = 1;
    int currentLvl;

    public int SetCurrentLvl
    {
        set { currentLvl = value; }
    }

    bool spawnLvl = false;

	void Start ()
    {
        Spawner = GameObject.Find("Spawner");
        Positions = new List<Transform>(transform.childCount);
        for(int i = 0; i < transform.childCount; i++)
        {
            Positions.Add(transform.GetChild(i).transform);
        }
        Asteroids = Resources.LoadAll<GameObject>("Prefabs/Asteroids");
        Lvl = Resources.LoadAll<GameObject>("Prefabs/Lvl");

        for(int o = 0; o < Positions.Count; o++)
        {
            int randIndex = Random.Range(0, Positions.Count - 1);
            int randAsteroid = Random.Range(0, Asteroids.Length - 1);
            GameObject go = Instantiate(Asteroids[randAsteroid], Positions[randIndex]) as GameObject;
            go.transform.position = Positions[randIndex].position;
            Positions.RemoveAt(randIndex);
        }
	}
	
	
	void Update ()
    {
        transform.position = new Vector3(transform.position.x - Time.deltaTime * spd, transform.position.y);

        if(spawnLvl == false)
        {
            if(transform.position.x <= -5.5f)
            {
                spawnLvl = true;
                int b = Random.Range(0, Lvl.Length -1);
                //if(b == currentLvl)
                //{
                //    for (int i = 0; b == currentLvl; i++)
                //    {
                //        b = Random.Range(0, Lvl.Length - 1);
                //    }
                //}
                GameObject goLvl = Instantiate(Lvl[b], Spawner.transform ) as GameObject;
                goLvl.name = Lvl[b].name;
                goLvl.transform.position = Spawner.transform.position;
                if(goLvl.GetComponent<AsteroidLvlScripts>() != null)
                {
                    goLvl.GetComponent<AsteroidLvlScripts>().SetCurrentLvl = b;
                }
                else
                {
                    goLvl.GetComponent<LvlScript>().SetCurrentLvl = b;
                }
            }
        }


        if(transform.position.x <= -25)
        {
            Destroy(gameObject);
        }
	}
}
