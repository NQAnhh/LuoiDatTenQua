using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameProject
{
    public class Health : MonoBehaviour
    {
        [Header("Health")]
        [SerializeField] float startingHealth;
        public float currentHealth { get; set; }
        private Animator ani;
        bool dead;
        [Header("iFrames")]
        [SerializeField] float iFramesDuration;
        [SerializeField] int numberOfFlashes;
        private SpriteRenderer spriteRend;
        [Header("Components")]
        [SerializeField] private Behaviour[] component;
        private void Awake()
        {
            currentHealth = startingHealth;
            ani = GetComponent<Animator>();
            spriteRend = GetComponent<SpriteRenderer>();
        }
        public void TakeDamage(float _damage)
        {
            currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
            if (currentHealth > 0)
            {
                ani.SetTrigger("Hurt");
                StartCoroutine(Invunerability());
            }
            else
            {
                if (!dead)
                {
                    ani.SetTrigger("Die");
                   foreach (Behaviour component in component)
                    {
                        component.enabled = false;
                    }
                    dead = true;
                }
            }
        }
        public void AddHealth(float _value)
        {
            currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
        }

        public void Respawn()
        {
            dead = false;
            AddHealth(startingHealth);
            ani.ResetTrigger("Die");
            ani.Play("Idle");
            StartCoroutine(Invunerability());

            foreach (Behaviour component in component)
            {
                component.enabled = true;
            }
        }
        private IEnumerator Invunerability()
        {
            Physics2D.IgnoreLayerCollision(3, 7, true);
            for (int i = 0; i < numberOfFlashes; i++)
            {
                spriteRend.color = new Color(1, 0, 0, 0.5f);
                yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
                spriteRend.color = Color.white;
                yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            }

            Physics2D.IgnoreLayerCollision(3, 7, false);

        }
        private void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}