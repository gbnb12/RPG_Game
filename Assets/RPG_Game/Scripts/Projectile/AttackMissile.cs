using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inheritance;

namespace indexer
{ 
    public class AttackMissile : Projectile
    {



        protected override void Impact(Collision collision)
        {
            gameObject.SetActive(false);
        }
    }
}
