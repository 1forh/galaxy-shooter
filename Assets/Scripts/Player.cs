using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 10f;

    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private float _fireRate = 0.5f;
    private float _nextFire = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
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
        Vector3 startingPosition = new Vector3(0, 0.8f, 0);
        Instantiate(_laserPrefab, transform.position + startingPosition, Quaternion.identity);
    }
}
