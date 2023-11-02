using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace GameProject
{
    public class CheckGround : MonoBehaviour
    {
        [SerializeField] Transform GroundCheckBox;
        [SerializeField] LayerMask Ground;
        public bool GroundCheck;
        Vector2 center;
        Vector2 size;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            size = new(0.5f, 0.1f);
            center = new(GroundCheckBox.position.x, GroundCheckBox.position.y);
            GroundCheck = Physics2D.OverlapBox(center, size, 0, Ground);
            if (GroundCheck)
            {
                Debug.Log("Cham san");
            }
            else
            {
                Debug.Log("Ko cham san");
            }
        }
       
    }
}
