using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private Animator _animator;
        private Vector3 _movement;
        public Vector3 Movement => _movement;

        void Update()
        {
            Move();
        }

        public void Move()
        {
            _movement = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
            //_movement = new Vector3(Joystick.Horizontal, Joystick.Vertical, 0);
            
            if (_movement.x > 0)
            {
                _animator.SetBool("Left", false);
                _animator.SetBool("Right", true);
            }
            else
            {
                _animator.SetBool("Right", false);
                _animator.SetBool("Left", true);
            }

            if (_movement.y > 0)
            {
                _animator.SetBool("Down", false);
                _animator.SetBool("Up", true);
            }
            else
            {
                _animator.SetBool("Up", false);
                _animator.SetBool("Down", true);
            }

            transform.position += _movement.normalized * (_moveSpeed * Time.deltaTime);
            _animator.SetFloat("Horizontal", _movement.x);
            _animator.SetFloat("Vertical", _movement.y);
            _animator.SetFloat("Speed", _movement.sqrMagnitude);
        }
    }
}
