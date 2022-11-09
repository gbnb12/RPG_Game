using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable 
{
    public void TakeAttackDamage(int amount);

    public void TakeLaserDamage(int number);

    public void Kill();

    public void Heal(int recover);
}
