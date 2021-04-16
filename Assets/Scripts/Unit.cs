using UnityEngine;
using System;

public class Unit : MonoBehaviour
{
    public event Action<int> OnHealthChanged;

    [SerializeField] private int _health = 100;

    public int Health => _health;

    public void TakeAttack(int damage)
    {
        if (_health > 0)
        {
            _health -= damage; 
            OnHealthChanged?.Invoke(_health);
        }
    }

    public void TakeHeal(int heal)
    {
        if (_health < 100)
        {
            _health += heal; 
            OnHealthChanged?.Invoke(_health);
        }
    }
}
