using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TurnGameSM))]

public class TurnGameState : State
{
    protected TurnGameSM StateMachine { get; private set; }

    void Awake()
    {
        StateMachine = GetComponent<TurnGameSM>();
    }
}
