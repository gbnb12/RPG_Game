using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AITurn : TurnGameState
{
    public static event Action EnemyTurnBegan;
    public static event Action EnemyTurnEnded;

    [SerializeField] Text _AIAttackTextUI = null;
    [SerializeField] Text _AIHealTextUI = null;
    [SerializeField] Text _AILaserTextUI = null;

    [SerializeField] Image _AttackImage = null;
    [SerializeField] Image _HealImage = null;
    [SerializeField] Image _LaserImage = null;

    [SerializeField] float _pauseDuration = 1.5f;

    [SerializeField] Text _enemyLaserTextUI = null;

    [SerializeField] Collider _playerPiece;
    [SerializeField] Collider _AIPiece;

    [SerializeField] Text _AttackDamageTextUI = null;
    [SerializeField] Text _HealDamageTextUI = null;
    [SerializeField] Text _LaserDamageTextUI = null;

    [SerializeField] Image _AttackDamageImage = null;
    [SerializeField] Image _HealDamageImage = null;
    [SerializeField] Image _LaserDamageImage = null;

    private int _laserCountdown = 0;

    public CameraShaking cameraShake;

    int _enemyAction = 0;

    public GameObject healObject;

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
        _AttackImage.gameObject.SetActive(false);
        _HealImage.gameObject.SetActive(false);
        _LaserImage.gameObject.SetActive(false);

        _AttackDamageTextUI.gameObject.SetActive(false);
        _HealDamageTextUI.gameObject.SetActive(false);
        _LaserDamageTextUI.gameObject.SetActive(false);
        _AttackDamageImage.gameObject.SetActive(false);
        _HealDamageImage.gameObject.SetActive(false);
        _LaserDamageImage.gameObject.SetActive(false);

        healObject.SetActive(false);
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
            _AttackImage.gameObject.SetActive(true);

            _AttackDamageTextUI.gameObject.SetActive(true);
            _AttackDamageImage.gameObject.SetActive(true);
        }
        if (_enemyAction == 2)
        {
            AIPressedHeal();
            _AIHealTextUI.gameObject.SetActive(true);
            _HealImage.gameObject.SetActive(true);

            _HealDamageTextUI.gameObject.SetActive(true);
            _HealDamageImage.gameObject.SetActive(true);

            healObject.SetActive(true);
        }
        if (_enemyAction == 3)
        {
            AIPressedLaser();
            _AILaserTextUI.gameObject.SetActive(true);
            _LaserImage.gameObject.SetActive(true);

            _LaserDamageTextUI.gameObject.SetActive(true);
            _LaserDamageImage.gameObject.SetActive(true);

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
            //StartCoroutine(cameraShake.Shake(0.015f, -0.010f));
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
            //StartCoroutine(cameraShake.Shake(0.015f, -0.010f));
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
