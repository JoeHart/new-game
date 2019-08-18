using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceInvaderPlayerControls : MonoBehaviour
{
    public float speed = 1.0f;
    private float _currentSpeed = 0.0f;
    public GameObject bulletPrefab;
    public float fireInterval = 2.0f;
    private float _lastFired = 0.0f;
    public GameObject deathText;
    public AudioSource explosion;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        float z = transform.position.z;
        if (Input.GetAxis("Horizontal") > 0)
        {
            GetComponent<Rigidbody>().position = new Vector3(x + speed, y, z);
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            GetComponent<Rigidbody>().position = new Vector3(x - speed, y, z);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }
    }

    void Fire()
    {
        if (Time.time > (_lastFired + fireInterval))
        {
            float x = transform.position.x;
            float y = transform.position.y;
            float z = transform.position.z;
            Vector3 bulletPosition = new Vector3(x, y + 1.0f, z);
            GameObject bullet = Instantiate(bulletPrefab, bulletPosition, Quaternion.Euler(0, 0, 180));
            bullet.GetComponent<BulletLogic>().Go(false);

            _lastFired = Time.time;
        }
    }

    public void Die()
    {
        explosion.Play();
        deathText.SetActive(true);
        Destroy(gameObject);
    }
}
