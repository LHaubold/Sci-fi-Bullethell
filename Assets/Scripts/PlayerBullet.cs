using UnityEngine;
using System.Collections;

public class PlayerBullet : MonoBehaviour
{


	void Start ()
    {
	
	}
	
	
	void Update ()
    {
        transform.position = new Vector3(transform.position.x + Time.deltaTime * 5, transform.position.y);
        if(transform.position.x >= 9.3f)
        {
            Destroy(gameObject);
        }
	}
}
