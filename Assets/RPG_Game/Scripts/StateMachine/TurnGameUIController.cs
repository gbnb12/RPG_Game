using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnGameUIController : MonoBehaviour
{
    [SerializeField] Text _AIThinkingTextUI = null;

    private void OnEnable()
    {
        AITurn.EnemyTurnBegan += OnAITurnBegan;
        AITurn.EnemyTurnEnded += OnAITurnEnded;
    }

    private void OnDisable()
    {
        AITurn.EnemyTurnBegan -= OnAITurnBegan;
        AITurn.EnemyTurnEnded -= OnAITurnEnded;
    }

    private void Start()
    {
        // make sure text is disabled on start
        _AIThinkingTextUI.gameObject.SetActive(false);
    }

    void OnAITurnBegan()
    {
        _AIThinkingTextUI.gameObject.SetActive(true);
    }

    void OnAITurnEnded()
    {
        _AIThinkingTextUI.gameObject.SetActive(false);
    }
}
