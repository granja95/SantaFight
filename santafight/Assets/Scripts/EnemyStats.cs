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
    DamageCollider kickCollider;
        public int vida;
        public int damage;

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
            kickCollider = this.GetComponentInChildren<DamageCollider>();
            //healthbar.SetMaxHealth(maxHealth);
            damage = 5;
        }

    private int SetMaxHealthFromHealthLevel()
    {
        maxHealth = healthLevel * 10;
        return maxHealth;
    }

        public void OpenDamageCollider()
        {
            kickCollider.EnableDamageCollider();
        }

        public void CloseDamageCollider()
        {
            kickCollider.DisableDamageCollider();
        }

        public void TakeDamage(int damage)
    {
        currentHealth = currentHealth - damage;

            //healthbar.SetCurrentHealth(currentHealth);
        

        //_animator.Play("hit 0");
        //_animator.SetTrigger("Damage");

        if (currentHealth <= 0)
        {
                FindObjectOfType<AudioManager>().Play("Playerhit");
                if(FindObjectOfType<PlayerStats>().currentHealth < 100 && FindObjectOfType<PlayerStats>().currentHealth > 0)
                {
                    vida = FindObjectOfType<PlayerStats>().currentHealth;
                    vida = vida + 5;
                    FindObjectOfType<PlayerStats>().currentHealth = vida;
                    FindObjectOfType<PlayerStats>().healthbar.SetCurrentHealth(vida);
                }
                currentHealth = 0;
                gameManager.enemiesAlive--;
                _animator.Play("death");
                CloseDamageCollider();
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
