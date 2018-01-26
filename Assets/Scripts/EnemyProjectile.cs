using UnityEngine;
using System.Collections;

public class EnemyProjectile : MonoBehaviour
{
    Vector2 direction;

    float angle;
    float distancecheck;

    float spd;
    GameObject target;
    Vector2 targetPos;

    public GameObject setTarget
    {
        set { target = value; }
    }
    [SerializeField]
    int dmg = 10;
    int type = 0;

    public int setType
    {
        set { type = value; }
    }

	void Start ()
    {
        targetPos = target.transform.position - transform.position;
        LookAtTarget();
        switch (type)
        {
            case 0:
                dmg = 10;
                spd = 0.8f;
                break;
            case 1:
                dmg = 20;
                spd = 0.4f;
                break;
        }
    }
	
	void Update ()
    {
        transform.Translate(targetPos * Time.deltaTime * spd, Space.World);

        if (transform.position.x <= -10 || transform.position.x >= 10 || transform.position.y <= -6 || transform.position.y >= 6)
        {
            Destroy(gameObject);
        }
        
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<ship>().GetHit(gameObject, dmg);
        }
    }

    void LookAtTarget()
    {
        direction = target.transform.position - transform.position;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
