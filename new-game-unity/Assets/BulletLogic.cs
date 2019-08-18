using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    public float speed = 1.0f;

    public void Go(bool goDown)
    {
        if (goDown == true)
        {
            GetComponent<Rigidbody>().velocity = Vector3.down * speed;
        }
        else
        {
            GetComponent<Rigidbody>().velocity = Vector3.up * speed;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.collider.name)
        {
            case "Player":
                collision.collider.GetComponent<SpaceInvaderPlayerControls>().Die();
                break;
            case "Cube(Clone)":
                Debug.Log("hit alien");
                Destroy(collision.collider.gameObject);
                break;

        }

        Destroy(gameObject);
    }
}
