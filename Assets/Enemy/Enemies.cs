using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameProject
{
    public class Enemies : MonoBehaviour
    {
        [Header("Attack Parameters")]
        [SerializeField] float attackCooldown;
        [SerializeField] int damage;
        [SerializeField] float range;
        [Header("Collider Parameters")]
        [SerializeField] float colliderDistance;
        [SerializeField] private BoxCollider2D boxCollider;
        [Header("Player Layer")]
        [SerializeField] private LayerMask playerLayer;
        private float cooldownTimer = Mathf.Infinity;

        private Animator ani;
        private Health playerHealth;
        private EnemyPatrol enemyPatrol;
        private void Awake()
        {
            ani = GetComponent<Animator>();
            enemyPatrol = GetComponentInParent<EnemyPatrol>();
        }

        private void Update()
        {
            cooldownTimer += Time.deltaTime;
            if (PlayerIntSight())
            {
                if (cooldownTimer >= attackCooldown)
                {
                    //attack
                    cooldownTimer = 0;
                    ani.SetTrigger("attack");

                }
            }
            if (enemyPatrol != null)
                enemyPatrol.enabled = !PlayerIntSight();
        }
        private bool PlayerIntSight()
        {
            RaycastHit2D
                hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
                new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
                0, Vector2.left, 0, playerLayer);
            if (hit.collider != null)
            {
                playerHealth = hit.transform.GetComponent<Health>();
            }
            return hit.collider != null;
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
                new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
        }
        private void DamagePlayer()
        {
            if (PlayerIntSight())
            {
                //Damage to player
                playerHealth.TakeDamage(damage);
            }
        }
        private void Die()
        {
            boxCollider.enabled = false;
            ani.enabled = false;
            Destroy(gameObject);
        }


    }
}
