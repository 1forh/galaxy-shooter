﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{

    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int _powerupId;

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        bool isPlayer = other.tag == "Player";

        if (isPlayer)
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                player.ActivatePowerup(_powerupId);
            }

            Destroy(gameObject);
        }
    }

    void CalculateMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= -7f)
        {
            Destroy(gameObject);
        }
    }
}
