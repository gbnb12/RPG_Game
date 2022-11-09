using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour, IDamageable
{
    public int _currentHealth = 100;

    [SerializeField] Text _healthText;

    public GameObject targetObject;


    void Start()
    {
        _currentHealth = 100;
    }

    public void Kill()
    {
        Destroy(targetObject);
    }

    public void TakeAttackDamage(int amount)
    {
        _currentHealth -= amount;
        _healthText.GetComponent<Text>().text = "Health: " + _currentHealth;
        Debug.Log("Game Piece took damage: " + amount);
        if (_currentHealth <= 0)
        {
            Kill();
            SceneManager.LoadScene("Restart");
        }
    }

    public void TakeLaserDamage(int number)
    {
        _currentHealth -= number;
        _healthText.GetComponent<Text>().text = "Health: " + _currentHealth;
        Debug.Log("Game Piece took damage: " + number);
        if (_currentHealth <= 0)
        {
            Kill();
            SceneManager.LoadScene("Restart");
        }
    }

    public void Heal(int level)
    {
        _currentHealth += level;
        _healthText.GetComponent<Text>().text = "Health: " + _currentHealth;
    }
}
