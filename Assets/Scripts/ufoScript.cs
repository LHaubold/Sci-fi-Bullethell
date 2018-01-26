using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class ufoScript : MonoBehaviour
{
    GameObject Player;
    GameObject Projectiles;

    Vector2 direction;

    float angle;
    float distancecheck;
    float timer = 0;
    float shootingIntervall = 3f;
    float maxY;
    float minY;
    float move = 0.5f;
    bool movefinish = false;

    bool shooting = false;

    int HP;

    void Start()
    {
        Projectiles = GameObject.Find("Projectiles");
        Player = GameObject.Find("ship");
        HP = Random.Range(1, 3);
        move = Random.Range(0, 1000) / 1000f;
        if(move >= 0.5f)
        {
            movefinish = true;
        }
        if((transform.position.y - 1) < -4.4f)
        {
            minY = -4.4f;
        }
        else
        {
            minY = transform.position.y;
        }
        if((transform.position.y + 1) > 4.4f)
        {
            maxY = 4.4f;
        }
        else
        {
            maxY = transform.position.y + 1;
        }
    }

    void Update()
    {
        if(Player != null)
        {
            LookAtTarget();
            if(Player.transform.position.x < gameObject.transform.position.x)
            {
                shoot();
            }
            hover();
        }

    }

    void hover()
    {
        if (movefinish == false)
        {
            move += Time.deltaTime * 0.8f;
        }
        else
        {
            move -= Time.deltaTime * 0.8f;
        }
        if (move <= 0)
        {
            movefinish = false;
        }
        if(move >= 1)
        {
            movefinish = true;
        }

        transform.position = Vector2.Lerp(new Vector2(transform.position.x, minY), new Vector2(transform.position.x, maxY), move);
    }

    void shoot()
    {
        if (transform.position.x <= 9 && transform.position.x >= -9)
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

            if (timer >= shootingIntervall)
            {
                timer = 0;
                GameObject go = Instantiate(Resources.Load("Prefabs/enemyProjectile2") as GameObject);
                go.transform.position = gameObject.transform.GetChild(0).transform.position;
                go.transform.parent = Projectiles.transform;
                go.GetComponent<EnemyProjectile>().setType = 1;
                go.GetComponent<EnemyProjectile>().setTarget = transform.GetChild(1).gameObject;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            GameObject Explosion = Instantiate(Resources.Load("Prefabs/explosion") as GameObject);
            Explosion.transform.parent = transform.parent.transform;
            //Explosion.transform.position = transform.position;
            col.gameObject.GetComponent<ship>().LooseLive();
            Destroy(gameObject);
        }
        if (col.gameObject.tag == "Projectile")
        {
            Destroy(col.gameObject);
            HP--;
            if (HP <= 0)
            {
                GameObject Explosion = Instantiate(Resources.Load("Prefabs/explosion") as GameObject);
                Explosion.transform.parent = transform.parent;
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
