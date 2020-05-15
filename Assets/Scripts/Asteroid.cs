using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotationSpeed = 3.0f;
    [SerializeField]
    private GameObject _explosionPrefab;
    private SpawnManager _spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();

        if (_spawnManager == null)
        {
            Debug.LogError("Spawn Manager is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * _rotationSpeed);
    }

    // check for laser collission of type trigger
    // instantiate exposion at position of asteroid
    // destroy explosion after 3 seconds

    private void OnTriggerEnter2D(Collider2D other)
    {
        bool isLaser = other.tag == "Laser";

        if (isLaser)
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject, 0.25f);
            Destroy(other.gameObject);
            _spawnManager.StartSpawning();
        }
    }

    IEnumerator DestroyExplosion()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(_explosionPrefab);
    }
}
