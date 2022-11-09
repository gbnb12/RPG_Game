using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setup : TurnGameState
{
    bool _activated = false;

    public override void Enter()
    {
        Debug.Log("Setup: Enter");

        _activated = false;
    }

    public override void Tick()
    {
        if (_activated == false)
        {
            _activated = true;
            StateMachine.ChangeState<PlayerTurn>();
        }
    }

    public override void Exit()
    {
        _activated = false;
        Debug.Log("Setup: Exit");
    }
}
