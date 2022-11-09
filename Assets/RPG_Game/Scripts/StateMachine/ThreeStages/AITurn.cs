using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class AITurn : TurnGameState
{
    public static event Action EnemyTurnBegan;
    public static event Action EnemyTurnEnded;

    [SerializeField] Text _AIAttackTextUI = null;
    [SerializeField] Text _AIHealTextUI = null;
    [SerializeField] Text _AILaserTextUI = null;

    [SerializeField] float _pauseDuration = 1.5f;

    [SerializeField] Text _enemyLaserTextUI = null;

    [SerializeField] Collider _playerPiece;
    [SerializeField] Collider _AIPiece;

    private int _laserCountdown = 0;

    int _enemyAction = 0;

    public override void Enter()
    {
        Debug.Log("Enemy Turn: Enter");
        EnemyTurnBegan?.Invoke();

        _enemyAction += 1;
        _laserCountdown += 1;

        _enemyLaserTextUI.GetComponent<Text>().text = "Laser: " + _laserCountdown;

        StartCoroutine(EnemyThinkingRoutine(_pauseDuration));
    }

    public override void Exit()
    {
        Debug.Log("Enemy Turn: Exit");
        _AIAttackTextUI.gameObject.SetActive(false);
        _AIHealTextUI.gameObject.SetActive(false);
        _AILaserTextUI.gameObject.SetActive(false);
    }

    IEnumerator EnemyThinkingRoutine(float pauseDuration)
    {
        Debug.Log("Enemy thinking");
        yield return new WaitForSeconds(pauseDuration);

        Debug.Log("Enemy performs action");
        EnemyTurnEnded?.Invoke();

        if (_enemyAction == 1)
        {
            AIPressedAttack();
            _AIAttackTextUI.gameObject.SetActive(true);
        }
        if (_enemyAction == 2)
        {
            AIPressedHeal();
            _AIHealTextUI.gameObject.SetActive(true);
        }
        if (_enemyAction == 3)
        {
            AIPressedLaser();
            _AILaserTextUI.gameObject.SetActive(true);
            _enemyAction -= 3;
        }

        // turn over. Go back to Player.
        //StateMachine.ChangeState<PlayerTurn>();
    }

    void AIPressedAttack()
    {
        Debug.Log("Enemy attacks the AI!");
        IDamageable damage = _playerPiece.GetComponent<IDamageable>();
        if (damage != null)
        {
            damage.TakeAttackDamage(20);
        }
        StateMachine.ChangeState<PlayerTurn>();
    }

    void AIPressedLaser()
    {
        Debug.Log("Enemy shoots laser!");
        IDamageable number = _playerPiece.GetComponent<IDamageable>();
        if (number != null)
        {
            number.TakeLaserDamage(30);
            _laserCountdown -= 3;
            _enemyLaserTextUI.GetComponent<Text>().text = "Laser: " + _laserCountdown;
        }
        StateMachine.ChangeState<PlayerTurn>();
    }

    void AIPressedHeal()
    {
        Debug.Log("Enemy recovers!");
        IDamageable recover = _AIPiece.GetComponent<IDamageable>();
        if (recover != null)
        {
            recover.Heal(10);
        }
        StateMachine.ChangeState<PlayerTurn>();
    }
}
