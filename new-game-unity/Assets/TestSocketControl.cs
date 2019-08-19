using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSocketControl : MonoBehaviour
{
    private bool _leftPress = false;
    private bool _rightPress = false;
    private bool _firePress = false;
    public Vector3 startPosition;
    private Rigidbody _rigidbody;
    public GameObject bulletPrefab;
    public GameObject bulletPlayerPrefab;
    public float fireInterval = 2.0f;
    private float _lastFired = 0.0f;
    private bool _lastTouchedLeft = false;
    private bool _firstTouch = true;
    public AlienController controller;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.started)
        {


            float x = transform.position.x;
            float y = transform.position.y;
            float z = transform.position.z;
            if (_leftPress == true)
            {
                _rigidbody.velocity = new Vector3(-controller.alienSpeed, 0, 0);
                _leftPress = false;
            }
            if (_rightPress == true)
            {
                _rigidbody.velocity = new Vector3(controller.alienSpeed, 0, 0);
                _rightPress = false;
            }

            if (_firePress == true || Input.GetButtonDown("Fire1"))
            {
                if (Time.time > (_lastFired + fireInterval))
                {
                    Vector3 bulletPosition = new Vector3(x, y - 0.4f, z);
                    if (controller.allowFriendlyFire)
                    {
                        GameObject bullet = Instantiate(bulletPlayerPrefab, bulletPosition, Quaternion.Euler(0, 0, 0));
                        bullet.GetComponent<BulletLogic>().Go(true);

                    }
                    else
                    {
                        GameObject bullet = Instantiate(bulletPrefab, bulletPosition, Quaternion.Euler(0, 0, 0));
                        bullet.GetComponent<BulletLogic>().Go(true);

                    }
                    _lastFired = Time.time;
                }

                _firePress = false;
            }
        }
    }

    public void Right()
    {
        _rightPress = true;
    }

    public void Left()
    {
        _leftPress = true;
    }

    public void Fire()
    {
        _firePress = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.collider.name)
        {
            case "LeftWall":
                if (_firstTouch || !_lastTouchedLeft)
                {
                    Advance();
                    _firstTouch = false;
                    _lastTouchedLeft = true;
                    _rigidbody.velocity = new Vector3(controller.alienSpeed, 0, 0);

                }
                break;
            case "RightWall":
                if (_firstTouch || _lastTouchedLeft)
                {

                    Advance();
                    _firstTouch = false;
                    _lastTouchedLeft = false;
                    _rigidbody.velocity = new Vector3(-controller.alienSpeed, 0, 0);


                }
                break;

        }

    }

    void Advance()
    {
        _rigidbody.isKinematic = true;
        float x = transform.position.x;
        float y = transform.position.y - 1.2f;
        float z = transform.position.z;
        Vector3 newPosition = new Vector3(x, y, z);
        controller.SpeedUpAliens();
        transform.position = newPosition;
        _rigidbody.isKinematic = false;
    }
}
