using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienController : MonoBehaviour
{

    List<string> ids;
    Dictionary<string, GameObject> aliens;
    List<Vector3> positions;
    public GameObject alienPrefab;
    public bool allowFriendlyFire = false;
    public float gapBetweenAliens = 2.0f;
    public float verticalGapBetweenAliens = 2.0f;
    public float alienSpeed = 1.0f;

    public bool started = false;
    // Start is called before the first frame update
    void Start()
    {
        aliens = new Dictionary<string, GameObject>();
        positions = new List<Vector3>();
        for (int y = 0; y < 9; y++)
        {
            for (int x = 0; x < 11; x++)
            {
                Vector3 position = new Vector3(x * gapBetweenAliens, -y * verticalGapBetweenAliens, 0);
                positions.Add(position);
                // GameObject alien = Instantiate(alienPrefab, position, Quaternion.Euler(0, 0, 180));
                // alien.GetComponent<TestSocketControl>().controller = this;

            }
        }
    }

    public void SpeedUpAliens()
    {
        alienSpeed = alienSpeed * 1.1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            started = true;
        }
    }

    public void Right(string id)
    {
        if (aliens.ContainsKey(id))
        {
            GameObject alien = aliens[id];
            alien.GetComponent<TestSocketControl>().Right();
        }
    }
    public void Left(string id)
    {
        if (aliens.ContainsKey(id))
        {
            GameObject alien = aliens[id];
            alien.GetComponent<TestSocketControl>().Left();
        }
    }
    public void Fire(string id)
    {
        if (aliens.ContainsKey(id))
        {
            GameObject alien = aliens[id];
            alien.GetComponent<TestSocketControl>().Fire();
        }
    }

    public void Spawn(string id)
    {
        Debug.Log("Spawning: " + id);
        if (aliens.ContainsKey(id))
        {
            return;
        }

        float x = Random.Range(-3.0f, 3.0f);
        float y = Random.Range(-3.0f, 3.0f);
        float z = Random.Range(-3.0f, 3.0f);

        Vector3 position = positions[0];
        positions.Remove(positions[0]);


        GameObject alien = Instantiate(alienPrefab, position, Quaternion.Euler(0, 0, 180));
        alien.GetComponent<TestSocketControl>().startPosition = position;
        alien.GetComponent<TestSocketControl>().controller = this;
        aliens.Add(id, alien);
    }

    public void Destroy(string id)
    {
        Debug.Log("Destroying:" + id);
        if (aliens.ContainsKey(id))
        {
            GameObject alien = aliens[id];
            aliens.Remove(id);
            positions.Insert(0, alien.GetComponent<TestSocketControl>().startPosition);
            Destroy(alien);
        }
    }
}
