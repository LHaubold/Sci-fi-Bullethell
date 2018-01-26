using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class AsteroidScripts : MonoBehaviour
{
    int rotateDirection;
    int rotateSpd;
    int HP;

    GameObject Projectiles;
	
	void Start ()
    {
        Projectiles = GameObject.Find("Projectiles");

        HP = Random.Range(1, 3);
        rotateDirection = Random.Range(0, 1000);
        if(rotateDirection < 500)
        {
            rotateDirection = -1;
        }
        else
        {
            rotateDirection = 1;
        }
        rotateSpd = Random.Range(20, 100);
	}


    void Update()
    {
        transform.Rotate(0, 0, Time.deltaTime * rotateDirection * rotateSpd);
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Projectile")
        {
            Destroy(col.gameObject);
            HP--;
            if(HP <= 0)
            {
                GameObject Explosion = Instantiate(Resources.Load("Prefabs/explosion") as GameObject);
                Explosion.transform.parent = transform.parent.transform;
                Explosion.transform.position = transform.position;
                staticValues.Score += 5;
                dropStuff();
                Destroy(gameObject);
            }         
        }
        if(col.gameObject.tag == "Player")
        {
            GameObject Explosion = Instantiate(Resources.Load("Prefabs/explosion") as GameObject);
            Explosion.transform.parent = transform.parent.transform;
            Explosion.transform.position = transform.position;
            col.gameObject.GetComponent<ship>().LooseLive();
            Destroy(gameObject);
        }
    }

    void dropStuff()
    {
        int randomDrop = Random.Range(0, 1000);
        if (randomDrop <= 50)
        {
            GameObject go = Instantiate(Resources.Load<GameObject>("Prefabs/Health"));
            go.transform.parent = transform.parent.transform;
            go.transform.position = transform.position;
        }
        else if (randomDrop > 80 && randomDrop <= 100)
        {
            GameObject go = Instantiate(Resources.Load<GameObject>("Prefabs/Powerup"));
            go.transform.parent = transform.parent.transform;
            go.GetComponent<Pickups>().setType = 1;
            go.transform.position = transform.position;
        }
    }
}
