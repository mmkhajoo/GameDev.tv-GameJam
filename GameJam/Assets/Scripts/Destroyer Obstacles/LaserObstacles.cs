using DefaultNamespace;
using Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserObstacles : MonoBehaviour, IDestroyerObstacles
{

    [SerializeField]
    private GameObject _spriteGameObject;

    [Header("Sucker Stats")]
    [SerializeField]
    private float _activeTime;
    [SerializeField]
    private float _deactiveTime;

    private bool _isActive;
    private Collider2D _collider;
    private float _tmpTime;

    public bool IsActive => _isActive;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!IsActive)
            return;

        if (collision.transform.CompareTag("Transitionable"))
        {
            collision.GetComponent<ObjectController>().Destroy(true);
        }

        else if (collision.transform.CompareTag("Player"))
        {
            collision.GetComponent<Player>().Die();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!IsActive)
            return;

        if (collision.transform.CompareTag("Transitionable"))
        {
            collision.GetComponent<ObjectController>().Destroy(true);
        }

        else if (collision.transform.CompareTag("Player"))
        {
            collision.GetComponent<Player>().Die();
        }
    }

    public void Initialize()
    {
        _collider = GetComponent<Collider2D>();
        _isActive = true;
        _tmpTime = 0f;
    }

    public void Active()
    {
        _isActive = true;
        _spriteGameObject.SetActive(true);
    }

    public void Deactive()
    {
        _isActive = false;
        _spriteGameObject.SetActive(false);
    }

    public void Execute()
    {

    }

    private void Update()
    {
        if (IsActive)
        {
            _tmpTime += Time.deltaTime;
            if (_tmpTime > _activeTime)
            {
                _tmpTime = 0f;
                Deactive();
            }
        }
        else
        {
            _tmpTime += Time.deltaTime;
            if (_tmpTime > _deactiveTime)
            {
                _tmpTime = 0f;
                Active();
            }
        }
    }
}
