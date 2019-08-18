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
    public float fireInterval = 2.0f;
    private float _lastFired = 0.0f;
    private bool _lastTouchedLeft = false;
    private bool _firstTouch = true;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        float z = transform.position.z;
        if (Input.GetKeyDown(KeyCode.LeftArrow) || _leftPress == true)
        {
            _rigidbody.velocity = new Vector3(-1f, 0, 0);
            _leftPress = false;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || _rightPress == true)
        {
            _rigidbody.velocity = new Vector3(1f, 0, 0);
            _rightPress = false;
        }

        if (_firePress == true)
        {
            if (Time.time > (_lastFired + fireInterval))
            {
                Vector3 bulletPosition = new Vector3(x, y - 0.3f, z);
                Instantiate(bulletPrefab, bulletPosition, Quaternion.Euler(0, 0, 0));
                _lastFired = Time.time;
            }

            _firePress = false;
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
                }
                break;
            case "RightWall":
                if (_firstTouch || _lastTouchedLeft)
                {

                    Advance();
                    _firstTouch = false;
                    _lastTouchedLeft = false;

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
        transform.position = newPosition;
        _rigidbody.isKinematic = false;
    }
}
