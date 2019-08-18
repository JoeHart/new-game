using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    public float speed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().velocity = Vector3.down * speed;
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.collider.name)
        {
            case "Player":
                Destroy(collision.collider);
                break;
            case "Cube(Clone)":
                Debug.Log("hit alien");
                Destroy(collision.collider.gameObject);
                break;

        }

        Destroy(gameObject);
    }
}
