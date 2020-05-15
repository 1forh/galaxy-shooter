using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;
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
    [SerializeField]
    private bool _isSpeedActive = false;
    [SerializeField]
    private float _speedModifier = 2.0f;
    [SerializeField]
    private bool _isShieldsActive = false;
    [SerializeField]
    private GameObject _playerShield;
    [SerializeField]
    private int _score = 0;
    private UIManager _uiManager;

    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL");
        }

        if (_uiManager == null)
        {
            Debug.LogError("The UI Manager is NULL");
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

    public void ActivatePowerup(int powerupId)
    {
        switch(powerupId)
        {
            case 0:
                _isTripleShotActive = true;
                break;
            case 1:
                _isSpeedActive = true;
                _speed *= _speedModifier;
                
                break;
            case 2:
                _isShieldsActive = true;
                _playerShield.SetActive(true);
                break;
        }

        StartCoroutine(DeactivatePowerup());
    }

    IEnumerator DeactivatePowerup()
    { 
        yield return new WaitForSeconds(5.0f);

        if (_isSpeedActive == true)
        {
            _speed /= _speedModifier;
            _isSpeedActive = false;
        }

        _isTripleShotActive = false;
       
        _isShieldsActive = false;
        _playerShield.SetActive(false);
    }

    public void Damage()
    {
        if (_isShieldsActive == true)
        {
            // deactivate shields
            this._isShieldsActive = false;
            _playerShield.SetActive(false);
            return;
        }

        _lives--;

        _uiManager.UpdateLives(_lives);

        if (_lives < 1)
        {
            _spawnManager.OnPlayerdeath();
            Destroy(gameObject);
        }
    }

    public void AddToScore(int amount = 10)
    {
        _score += amount;
        _uiManager.UpdateScore(_score);
    }
}
