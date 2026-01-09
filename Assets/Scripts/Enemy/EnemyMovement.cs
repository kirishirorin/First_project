using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using Player;
using UnityEngine;
using Zenject;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _freezeTime;
    [SerializeField] private Animator _animator;
    private Vector3 _direction;
    private PlayerMovement _playerMovement;
    private WaitForSeconds _checkTime = new WaitForSeconds(3f);
    private WaitForSeconds _freeze;
    private Coroutine _distanceToHide;
    private float _initialSpeed;

    public float MoveSpeed
    {
        get => _moveSpeed;
        set => _moveSpeed = value;
    }


    private void Start()
    {
        _initialSpeed =  _moveSpeed;
        _freeze = new WaitForSeconds(_freezeTime);
    }


    private void OnEnable()
    {
        _distanceToHide = StartCoroutine(CheckDistanceToHide());
    }

    private void OnDisable()
    {
        if (_distanceToHide != null)
        {
            StopCoroutine(_distanceToHide);
        }
    }

    private void Update()
    {
        Move();
    }


    public void FreezeEnemy(float percent)
    {
        if (gameObject.activeSelf)
        {
            StartCoroutine(StartFreeze(percent));
        }
    }

    private void Move()
    {
        _direction = (_playerMovement.transform.position - transform.position).normalized;
        transform.position += _direction * (_moveSpeed * Time.deltaTime);
        _animator.SetFloat("Horizontal", _direction.x);
        _animator.SetFloat("Vertical", _direction.y);
    }

    private IEnumerator CheckDistanceToHide()
    {
        while (true)
        {
            float distance = Vector3.Distance(transform.position, _playerMovement.transform.position);
            if (distance > 20f)
            {
                gameObject.SetActive(false);
            }

            yield return _checkTime;
        }
    }


    private IEnumerator StartFreeze(float percent)
    {
        _moveSpeed *= percent;
        yield return _freeze;
        _moveSpeed = _initialSpeed;
    }
    
    [Inject] private void Construct(PlayerMovement playerMovement) => _playerMovement = playerMovement;
}
