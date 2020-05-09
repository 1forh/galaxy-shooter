using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    private float _speed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
    }

    private void OnTriggerEnter(Collider other)
    {
        bool isPlayer = other.tag == "Player";
        bool isLaser = other.tag == "Laser";


        
        if (isPlayer)
        {
            // damage the player
            Destroy(gameObject);
        }


        if (isLaser)
        {
           Destroy(gameObject);
           Destroy(other.gameObject);

        }

    }

    void CalculateMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= -7f)
        {
            transform.position = new Vector3(Random.Range(0.0f, 8.0f), 7f, 0);
        }
    }
}
