using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarterAssets
{
    public class EnemyStats : MonoBehaviour
{
    public int healthLevel = 10;
    public int maxHealth;
    public int currentHealth;
    public GameManager gameManager;

        //public HealthBar healthbar;

        Animator _animator;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        maxHealth = SetMaxHealthFromHealthLevel();
        currentHealth = maxHealth;
        //healthbar.SetMaxHealth(maxHealth);
    }

    private int SetMaxHealthFromHealthLevel()
    {
        maxHealth = healthLevel * 10;
        return maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth = currentHealth - damage;

            //healthbar.SetCurrentHealth(currentHealth);
        

        //_animator.Play("hit 0");
        //_animator.SetTrigger("Damage");

        if (currentHealth <= 0)
        {
            currentHealth = 0;
                gameManager.enemiesAlive--;
                _animator.Play("death");
                Destroy(gameObject, 10f);
                Destroy(GetComponent<AIController>());
                Destroy(GetComponent<EnemyStats>());
                Destroy(GetComponent<CapsuleCollider>());
                //handle player death
            }
            else
            {
                _animator.Play("hit 0");
            }



    }
    }
}
