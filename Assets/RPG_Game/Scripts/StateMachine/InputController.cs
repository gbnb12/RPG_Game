using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputController : MonoBehaviour
{
    public event Action PressedConfirm = delegate { };
    public event Action PressedCancel = delegate { };
    public event Action PressedAttack = delegate { };
    public event Action PressedDefend = delegate { };
    public event Action PressedLaser = delegate { };
    public event Action PressedHeal = delegate { };

    

    private void Update()
    {
        DetectConfirm();
        DetectCancel();
        DetectAttack();
        DetectDefend();
        DetectLaser();
        DetectHeal();
    }

    private void DetectCancel()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            PressedCancel?.Invoke();
        }
    }

    private void DetectConfirm()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PressedConfirm?.Invoke();
        }
    }

    private void DetectAttack()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            PressedAttack?.Invoke();

        }
    }

    private void DetectDefend()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            PressedDefend?.Invoke();
        }
    }

    private void DetectLaser()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            PressedLaser?.Invoke();

        }
    }

    private void DetectHeal()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            
            PressedHeal?.Invoke();

        }
    }
}
