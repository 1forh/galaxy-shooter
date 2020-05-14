using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 10f;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = 1f;
    private float _nextFire = 0.0f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
    [SerializeField]
    private bool _isTripleShotActive = false;


    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();

        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown("space") && Time.time > _nextFire)
        {
            FireLaser();
        }
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        float x = horizontalInput * _speed * Time.deltaTime;
        float y = verticalInput * _speed * Time.deltaTime;
        Vector3 position = new Vector3(x, y, 0);
        transform.Translate(position);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        if (transform.position.x >= 11f)
        {
            transform.position = new Vector3(-11f, transform.position.y, transform.position.z);
        }
        else if (transform.position.x <= -11f)
        {
            transform.position = new Vector3(11f, transform.position.y, transform.position.z);
        }
    }

    void FireLaser()
    {
        _nextFire = Time.time + _fireRate;
        Vector3 startingPosition = new Vector3(0, 1.05f, 0);
        

        if (_isTripleShotActive == true)
        {
            Instantiate(_tripleShotPrefab, transform.position + startingPosition, Quaternion.identity);
        }
        else {
            Instantiate(_laserPrefab, transform.position + startingPosition, Quaternion.identity);
        }
    }

    public void ActivatePowerup(string key)
    {
        if (key == "TripleShot")
        {
            _isTripleShotActive = true;
        }
    }

    public void DeactivatePowerup()
    {
        _isTripleShotActive = false;
    }

    public void Damage()
    {
        _lives--;

        if (_lives < 1)
        {
            _spawnManager.OnPlayerdeath();
            Destroy(gameObject);
        }
    }
}
