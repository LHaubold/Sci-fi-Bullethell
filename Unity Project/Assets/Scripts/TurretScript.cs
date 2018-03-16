using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class TurretScript : MonoBehaviour
{
    GameObject Player;
    GameObject Projectiles;

    Vector2 direction;

    float angle;
    float distancecheck;
    float timer = 0;
    float shootingIntervall = 1.5f;

    bool shooting = false;

    int HP;

    void Start ()
    {
        Projectiles = GameObject.Find("Projectiles");
        Player = GameObject.Find("ship");
        HP = Random.Range(1, 3);
	}
	
	void Update ()
    {
        if(Player != null)
        {
            if(Player.transform.position.x < gameObject.transform.position.x)
            {
                shoot();
            }
            LookAtTarget();            
        }
	}

    void shoot()
    {
        if(transform.position.x <= 9 && transform.position.x >= -9)
        {
            shooting = true;
        }
        else
        {
            shooting = false;
        }

        if (shooting)
        {
            timer += Time.deltaTime;

            if(timer >= shootingIntervall)
            {
                timer = 0;
                switch(transform.parent.transform.parent.transform.parent.name)
                {
                    case "BlueLvl":
                        GameObject go = Instantiate(Resources.Load("Prefabs/blueBullet") as GameObject);
                        go.transform.position = gameObject.transform.GetChild(0).transform.position;
                        go.transform.parent = Projectiles.transform;
                        go.GetComponent<EnemyProjectile>().setType = 0;
                        go.GetComponent<EnemyProjectile>().setTarget = transform.GetChild(1).gameObject;
                        break;
                    case "RedLvl":
                        GameObject go2 = Instantiate(Resources.Load("Prefabs/redBullet") as GameObject);
                        go2.transform.position = gameObject.transform.GetChild(0).transform.position;
                        go2.transform.parent = Projectiles.transform;
                        go2.GetComponent<EnemyProjectile>().setType = 0;
                        go2.GetComponent<EnemyProjectile>().setTarget = transform.GetChild(1).gameObject;
                        break;
                    case "GreenLvl":
                        GameObject go3 = Instantiate(Resources.Load("Prefabs/greenBullet") as GameObject);
                        go3.transform.position = gameObject.transform.GetChild(0).transform.position;
                        go3.transform.parent = Projectiles.transform;
                        go3.GetComponent<EnemyProjectile>().setType = 0;
                        go3.GetComponent<EnemyProjectile>().setTarget = transform.GetChild(1).gameObject;
                        break;
                    case "YellowLvl":
                        GameObject go4 = Instantiate(Resources.Load("Prefabs/yellowBullet") as GameObject);
                        go4.transform.position = gameObject.transform.GetChild(0).transform.position;
                        go4.transform.parent = Projectiles.transform;
                        go4.GetComponent<EnemyProjectile>().setType = 0;
                        go4.GetComponent<EnemyProjectile>().setTarget = transform.GetChild(1).gameObject;
                        break;
                }


            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            GameObject Explosion = Instantiate(Resources.Load("Prefabs/explosion") as GameObject);
            Explosion.transform.parent = transform.parent.transform;
            Explosion.transform.position = transform.position;
            col.gameObject.GetComponent<ship>().LooseLive();
            Destroy(gameObject);            
        }
        if(col.gameObject.tag == "Projectile")
        {
            Destroy(col.gameObject);
            HP--;
            if(HP <= 0)
            {
                GameObject Explosion = Instantiate(Resources.Load("Prefabs/explosion") as GameObject);
                Explosion.transform.parent = transform.parent.transform;
                Explosion.transform.position = transform.position;
                staticValues.Score += 20;
                dropStuff();
                Destroy(gameObject);
            }
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
        else if (randomDrop > 90 && randomDrop <= 100)
        {
            GameObject go = Instantiate(Resources.Load<GameObject>("Prefabs/Powerup"));
            go.transform.parent = transform.parent.transform;
            go.GetComponent<Pickups>().setType = 1;
            go.transform.position = transform.position;
        }
    }

    void LookAtTarget()
    {
        direction = Player.transform.position - transform.position;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }
}
