using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inheritance
{
    [RequireComponent(typeof(Rigidbody))]

    public abstract class Projectile : MonoBehaviour
    {
        protected abstract void Impact(Collision collision);

        [SerializeField] protected float Speed = .25f;
        [SerializeField] protected Rigidbody RB;

        [SerializeField] protected AudioClip _hitSound;
        [SerializeField] protected ParticleSystem _hitParticle;

        private void OnCollisionEnter(Collision collision)
        {
            Impact(collision);
            Feedback();
        }

        private void Awake()
        {
            if(RB == null)
            {
                RB = GetComponent<Rigidbody>();
            }
        }

        private void FixedUpdate()
        {
            Move();
        }

        protected virtual void Move()
        {
            Vector3 moveOffset = transform.forward * Speed;
            RB.MovePosition(RB.position + moveOffset);
        }

        private void Feedback()
        {
            if (_hitParticle != null)
            {
                _hitParticle = Instantiate(_hitParticle,
                    transform.position, Quaternion.identity);
            }

            if (_hitSound != null)
            {
                AudioHelper.PlayClip2D(_hitSound, 1f);
            }
        }
    }
}
