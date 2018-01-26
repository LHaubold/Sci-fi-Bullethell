using UnityEngine;
using System.Collections;


//manages player movement, shooting, getting hit, hp, respawning, invincibility after death
public class ship : MonoBehaviour
{
    Animator anim;

    GameObject Projectiles;

    bool invincible = true; //returns true while player is invincible
    bool coroutineRunning = false; //returns true while player is shooting
    bool spaceup = false; //returns true when the player stops shooting

    //player spd
    [SerializeField]
    float spd = 5f; 
    float spdX;
    float spdY;

    //restricts moveable area
    float maxX = 8.0f; 
    float maxY = 3.9f;

    //invincibility timer
    float timer = 0; 
    float invincibleTimer = 0;
    float invincibleDuration = 2;


    Rigidbody2D rigid;

    void Start ()
    {
        anim = transform.GetComponent<Animator>();
        Projectiles = GameObject.Find("Projectiles");
        rigid = GetComponent<Rigidbody2D>();
	}
	
	
	void Update ()
    {
        Move();

        //initiate shooting
        if (Input.GetKey("space"))
        {
            if(coroutineRunning == false)
            {
                coroutineRunning = true;
                StartCoroutine("ShootingCoroutine"); 
            }
        }
        //stop shooting
        if (Input.GetKeyUp("space"))
        {
            spaceup = true;
        }

        //invincibility timer
        if(invincible == true)
        {
            invincibleTimer += Time.deltaTime;
            if(invincibleTimer >= invincibleDuration)
            {
                invincible = false;
                invincibleTimer = 0;
            }
        }
    }

    //coroutine for continuous shooting
    IEnumerator ShootingCoroutine()
    {
        while (coroutineRunning)
        {
            GameObject go = Instantiate(Resources.Load("Prefabs/StandardProjectile") as GameObject);
            go.transform.position = gameObject.transform.GetChild(0).transform.position;
            go.transform.parent = Projectiles.transform;
            //add aditional projectiles if powerup is active
            if (staticValues.Pickup == 1)
            {
                GameObject go2 = Instantiate(Resources.Load("Prefabs/StandardProjectile") as GameObject);
                go2.transform.position = gameObject.transform.GetChild(2).transform.position;
                go2.transform.parent = Projectiles.transform;
                GameObject go3 = Instantiate(Resources.Load("Prefabs/StandardProjectile") as GameObject);
                go3.transform.position = gameObject.transform.GetChild(3).transform.position;
                go3.transform.parent = Projectiles.transform;
            }
            //end shooting when spacebar is no longer pressed
            if (spaceup)
            {
                spaceup = false;
                coroutineRunning = false;
                yield break;
            }
            yield return new WaitForSeconds(0.3f);
        }

    }

    //manages incoming dmg
    public void GetHit(GameObject go, int dmg)
    {
        Destroy(go);
        if(invincible == false)
        {
            staticValues.HP -= dmg;
        }
        if(staticValues.HP <= 0)
        {
            LooseLive();
        }
    }

    //respawns player after death if he has remaining lifes
    public void LooseLive()
    {
        if(invincible == false)
        {
            staticValues.lives--;
            staticValues.HP = 100;
            staticValues.Pickup = 0;
            GameObject go = Instantiate(Resources.Load("Prefabs/explosion") as GameObject);
            go.transform.position = transform.position;
            go.transform.parent = Projectiles.transform;
            invincible = true;
            transform.position = new Vector3(-6, 0, 0);

        }

        if (staticValues.lives < 0)
        {
            Destroy(gameObject);
        }
    }

    //manages player movement 
    void Move()
    {
        //animationstuff
        if (Input.GetKey("w") || Input.GetKey("up"))
        {
            anim.SetBool("up", true);
        }
        else
        {
            anim.SetBool("up", false);
        }

        if (Input.GetKey("s") || Input.GetKey("down"))
        {
            anim.SetBool("down", true);
        }
        else
        {
            anim.SetBool("down", false);
        }
        //restricts movement in x direction
        if(transform.position.x > (maxX * -1) && transform.position.x < maxX && Input.GetAxis("Horizontal")  != 0)
        {
            spdX = spd;
        }
        else if((transform.position.x < (maxX * -1) && Input.GetAxis("Horizontal") < -0.1f) || (transform.position.x > maxX && Input.GetAxis("Horizontal") > 0.1f))
        {
            spdX = 0;
        }
        else if(transform.position.x < (maxX * -1) && Input.GetAxis("Horizontal") > 0.1f)
        {
            spdX = spd;
        }
        else if(transform.position.x > maxX && Input.GetAxis("Horizontal") < -0.1f)
        {
            spdX = spd;
        }
        //restricts movement in y direction
        if (transform.position.y > (maxY * -1) && transform.position.y < maxY && Input.GetAxis("Vertical") != 0)
        {
            spdY = spd;
        }
        else if ((transform.position.y < (maxY * -1) && Input.GetAxis("Vertical") < -0.1f) || (transform.position.y > maxY && Input.GetAxis("Vertical") > 0.1f))
        {
            spdY = 0;
        }
        else if (transform.position.y < (maxY * -1) && Input.GetAxis("Vertical") > 0.1f)
        {
            spdY = spd;
        }
        else if (transform.position.y > maxY && Input.GetAxis("Vertical") < -0.1f)
        {
            spdY = spd;
        }
        //move
        transform.position = new Vector3(transform.position.x + (Input.GetAxis("Horizontal") * Time.deltaTime * spdX), transform.position.y + (Input.GetAxis("Vertical")) * Time.deltaTime * spdY);               
    }
}
