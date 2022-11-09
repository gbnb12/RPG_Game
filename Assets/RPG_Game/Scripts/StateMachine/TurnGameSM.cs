using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnGameSM : StateMachine
{
    [SerializeField] InputController _input;
    public InputController Input => _input;
    void Start()
    {
        // set starting State here
        ChangeState<Setup>();
    }
}
