using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class PlayerTurn : TurnGameState
{
    [SerializeField] Text _playerTurnTextUI = null;
    [SerializeField] Text _laserTextUI = null;

    [SerializeField] Text _AttackDamageTextUI = null;
    [SerializeField] Text _HealDamageTextUI = null;
    [SerializeField] Text _LaserDamageTextUI = null;

    [SerializeField] Image _AttackDamageImage = null;
    [SerializeField] Image _HealDamageImage = null;
    [SerializeField] Image _LaserDamageImage = null;

    int _playerTurnCount = 0;

    [SerializeField] Collider _playerPiece;
    [SerializeField] Collider _AIPiece;

    private int _laserCountdown = 0;

    public GameObject healObject01;
    public GameObject healObject02;


    public override void Enter()
    {
        Debug.Log("Player Turn: Enter");
        _playerTurnTextUI.gameObject.SetActive(true);

        _playerTurnCount++;
        _playerTurnTextUI.text = "PlayerTurn: " + _playerTurnCount.ToString();

        _laserCountdown += 1;

        // hook into events
        StateMachine.Input.PressedConfirm += OnPressedConfirm;
        StateMachine.Input.PressedAttack += OnPressedAttack;
        //StateMachine.Input.PressedLaser += OnPressedLaser;
        StateMachine.Input.PressedHeal += OnPressedHeal;

        if (_laserCountdown >= 3)
        {
            StateMachine.Input.PressedLaser += OnPressedLaser;
        }

        _laserTextUI.GetComponent<Text>().text = "Laser: " + _laserCountdown;
    }

    public override void Exit()
    {
        _playerTurnTextUI.gameObject.SetActive(false);
        Debug.Log("Player Turn: Exit");
        // unhook from events
        StateMachine.Input.PressedConfirm -= OnPressedConfirm;
        StateMachine.Input.PressedAttack -= OnPressedAttack;
        StateMachine.Input.PressedLaser -= OnPressedLaser;
        StateMachine.Input.PressedHeal -= OnPressedHeal;

        _AttackDamageTextUI.gameObject.SetActive(false);
        _HealDamageTextUI.gameObject.SetActive(false);
        _LaserDamageTextUI.gameObject.SetActive(false);
        _AttackDamageImage.gameObject.SetActive(false);
        _HealDamageImage.gameObject.SetActive(false);
        _LaserDamageImage.gameObject.SetActive(false);

        healObject01.SetActive(false);
        healObject02.SetActive(false);

        Debug.Log("Player Turn; Exit");
    }

    void OnPressedConfirm()
    {
        Debug.Log("Attempt to enter Enemy State!");
        // change the enemy turn state
        StateMachine.ChangeState<AITurn>();
    }

    void OnPressedAttack()
    {
        Debug.Log("Player attacks the AI!");
        
        IDamageable damage = _AIPiece.GetComponent<IDamageable>();
        if (damage != null)
        {
            damage.TakeAttackDamage(20);
            _AttackDamageTextUI.gameObject.SetActive(true);
            _AttackDamageImage.gameObject.SetActive(true);
        }
        StateMachine.ChangeState<AITurn>();
    }

    void OnPressedLaser()
    {
        Debug.Log("Player shoots laser!");
        
        IDamageable number = _AIPiece.GetComponent<IDamageable>();
        if (number != null)
        {
            number.TakeLaserDamage(30);
            _laserCountdown -= 3;
            _LaserDamageTextUI.gameObject.SetActive(true);
            _LaserDamageImage.gameObject.SetActive(true);
            _laserTextUI.GetComponent<Text>().text = "Laser: " + _laserCountdown;
        }
        StateMachine.ChangeState<AITurn>();
    }

    void OnPressedHeal()
    {
        Debug.Log("Player recovers!");
        healObject01.SetActive(true);
        healObject02.SetActive(true);
        IDamageable recover = _playerPiece.GetComponent<IDamageable>();
        if (recover != null)
        {
            _HealDamageTextUI.gameObject.SetActive(true);
            _HealDamageImage.gameObject.SetActive(true);
            recover.Heal(10);
        }
        StateMachine.ChangeState<AITurn>();
    }
}
