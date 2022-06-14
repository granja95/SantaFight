using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarterAssets
{
    public class PlayerStats : MonoBehaviour
    {
        public int healthLevel = 10;
        public int maxHealth;
        public int currentHealth;

        public HealthBar healthbar;

        private Animator _animator;

        public bool isAlive = true;

        protected void Death()
        {
            isAlive = false;
        }
                

        private void Awake()
        {
            
            _animator = GetComponentInChildren<Animator>();
            
        }

        void Start()
        {
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
            healthbar.SetMaxHealth(maxHealth);
            
        }

        private int SetMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }

        public void TakeDamage(int damage)
        {

            currentHealth = currentHealth - damage;

            healthbar.SetCurrentHealth(currentHealth);


            if (currentHealth > 0)
            {
                _animator.SetTrigger("Damage");
                
            }
            else
            {
                _animator.Play("die");
                Death();
                currentHealth = 0;
                //Destroy(gameObject, 1f);
                GetComponent<ThirdPersonController>().enabled = false;
                FindObjectOfType<GameManager>().EndGame();
            }
            
        }
    }
}

