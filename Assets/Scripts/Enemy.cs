using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    private float _speed = 2.0f;
    private Player _player;
    private Animator _anim;

    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _anim = GetComponent<Animator>();

        if (_player == null)
        {
            Debug.LogError("_player is NULL");
        }

        if (_anim == null)
        {
            Debug.LogError("_anim is NULL");
        }
    }

    void Update()
    {
        CalculateMovement();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        bool isPlayer = other.tag == "Player";
        bool isLaser = other.tag == "Laser";
       
        if (isPlayer)
        {
            if (_player != null)
            {
                _player.Damage();
            }

            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            Destroy(gameObject, 2.8f);
        }


        if (isLaser)
        {

            if (_player != null)
            {
                _player.AddToScore(Random.Range(7, 11));
            }
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            Destroy(gameObject, 2.8f);
            Destroy(other.gameObject, 2.8f);
            
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
