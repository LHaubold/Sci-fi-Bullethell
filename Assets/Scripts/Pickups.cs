using UnityEngine;
using System.Collections;

public class Pickups : MonoBehaviour
{
    int rotateDirection;
    int rotateSpd;
    int type = 0;

    public int setType
    {
        set { type = value; }
    }

    void Start()
    {
        rotateDirection = Random.Range(0, 1000);
        if (rotateDirection < 500)
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
        if(col.gameObject.tag == "Player")
        {
            switch (type)
            {
                case 0:
                    staticValues.HP += 25;
                    if(staticValues.HP >= 100)
                    {
                        staticValues.HP = 100;
                    }
                    Destroy(gameObject);
                        break;
                case 1:
                    if (staticValues.Pickup != 1)
                    {
                        staticValues.Pickup = 1;
                        Destroy(gameObject);
                    }
                    break;
            }
        }
    }
}
