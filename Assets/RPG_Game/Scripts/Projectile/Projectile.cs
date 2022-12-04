using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inheritance
{
    [RequireComponent(typeof(Rigidbody))]

    public abstract class Projectile : MonoBehaviour
    {
        protected abstract void Impact(Collision otherCollision);

        [SerializeField] protected float Speed = .25f;
        [SerializeField] protected Rigidbody RB;

        private void OnCollisionEnter(Collision collision)
        {
            Impact(collision);

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
    }
}
