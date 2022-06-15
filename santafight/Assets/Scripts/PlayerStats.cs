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

        public AudioClip tiago_scream;

        public bool isAlive = true;

        public bool alreadyPlayed = false;

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

        public void alive()
        {
            gameObject.SetActive(true);
        }

        public void TakeDamage(int damage)
        {

            currentHealth = currentHealth - damage;

            healthbar.SetCurrentHealth(currentHealth);


            if (currentHealth > 0)
            {
                _animator.Play("hit");
                FindObjectOfType<AudioManager>().Play("Enemyhit");

            }
            else
            {
                _animator.Play("die");
                if (alreadyPlayed)
                    return;
                else
                {
                    GetComponent<AudioSource>().PlayOneShot(tiago_scream, 0.3f);
                    alreadyPlayed = true;
                }
                Death();
                currentHealth = 0;
                GetComponent<ThirdPersonController>().enabled = false;
                FindObjectOfType<GameManager>().EndGame();
                
            }
            
        }
    }
}

