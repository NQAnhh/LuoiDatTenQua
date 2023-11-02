using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameProject
{
    public class HealthCollection : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField] float healthValue;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                collision.GetComponent<Health>().AddHealth(healthValue);
                gameObject.SetActive(false);
            }
        }
    }
}