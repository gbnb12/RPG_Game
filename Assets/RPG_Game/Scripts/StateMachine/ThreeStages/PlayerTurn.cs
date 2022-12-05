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

    public GameObject attack;

    public GameObject laser;

    public Transform firePosition;

    [SerializeField] protected AudioClip _shootAttackSound;
    [SerializeField] protected AudioClip _shootLaserSound;
    [SerializeField] protected AudioClip _healSound;


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
        StateMachine.Input.PressedHeal += OnPressedHeal;

        StateMachine.Input.PressedHeal += ObjectsHeal;
        StateMachine.Input.PressedAttack += ObjectsAttack;
        

        //ObjectsHeal();

        if (_laserCountdown >= 3)
        {
            StateMachine.Input.PressedLaser += OnPressedLaser;
            StateMachine.Input.PressedLaser += ObjectsLaser;
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

        StateMachine.Input.PressedHeal -= ObjectsHeal;

        _AttackDamageTextUI.gameObject.SetActive(false);
        _HealDamageTextUI.gameObject.SetActive(false);
        _LaserDamageTextUI.gameObject.SetActive(false);

        _AttackDamageImage.gameObject.SetActive(false);
        _HealDamageImage.gameObject.SetActive(false);
        _LaserDamageImage.gameObject.SetActive(false);

        healObject01.gameObject.SetActive(false);
        healObject02.gameObject.SetActive(false);

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
            _laserTextUI.GetComponent<Text>().text = "Laser: " + _laserCountdown;
        }
        StateMachine.ChangeState<AITurn>();
    }

    void OnPressedHeal()
    {
        Debug.Log("Player recovers!");
        
        IDamageable recover = _playerPiece.GetComponent<IDamageable>();
        if (recover != null)
        {      
            recover.Heal(10);
        }
        StateMachine.ChangeState<AITurn>();
    }

    void ObjectsHeal()
    {
        healObject01.gameObject.SetActive(true);
        healObject02.gameObject.SetActive(true);

        _HealDamageTextUI.gameObject.SetActive(true);
        _HealDamageImage.gameObject.SetActive(true);
        HealFeedback();
    }

    void ObjectsAttack()
    {
        _AttackDamageTextUI.gameObject.SetActive(true);
        _AttackDamageImage.gameObject.SetActive(true);
        Instantiate(attack, firePosition.position, firePosition.rotation);
        AttackFeedback();
    }

    void ObjectsLaser()
    {
        _LaserDamageTextUI.gameObject.SetActive(true);
        _LaserDamageImage.gameObject.SetActive(true);
        Instantiate(laser, firePosition.position, firePosition.rotation);
        LaserFeedback();
    }

    private void AttackFeedback()
    {
        if (_shootAttackSound != null)
        {
            AudioHelper.PlayClip2D(_shootAttackSound, 1f);
        }
    }

    private void LaserFeedback()
    {
        if (_shootLaserSound != null)
        {
            AudioHelper.PlayClip2D(_shootLaserSound, 1f);
        }
    }

    private void HealFeedback()
    {
        if (_shootLaserSound != null)
        {
            AudioHelper.PlayClip2D(_healSound, 1f);
        }
    }

}
