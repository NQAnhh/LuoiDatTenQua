using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameProject
{
    public class CharacterController : MonoBehaviour
    {
        float Horizontal;
        [SerializeField] CheckGround Box;
        Rigidbody2D rb2D;
        Animator ani;
        bool isGround;
        bool moving;
        Vector3 respawnPoint;
        private Transform previousCheckpoint;
        private Transform currentCheckpoint;
        GameObject fallDetector;
        [SerializeField] float Char_speed;
        [SerializeField] float wSpeed;
        [SerializeField] float rSpeed;
        [SerializeField] float Char_JumpForce;
        

        // Start is called before the first frame update
        void Start()
        {
            ani = GetComponent<Animator>();
            rb2D = GetComponent<Rigidbody2D>();
            respawnPoint = transform.position;
            currentCheckpoint = GameObject.FindGameObjectWithTag("Checkpoint").transform;

        }

        // Update is called once per frame
        void Update()
        {
            isGround = Box.GroundCheck;
            speedControll(wSpeed, rSpeed);
            Hmovement(Char_speed);
            Vmovement();
            animationCheck();
        }
        void Hmovement(float MoveSpeed)
        {
            Horizontal = Input.GetAxisRaw("Horizontal");
            rb2D.velocity = new Vector2(Horizontal * MoveSpeed, rb2D.velocity.y);
            if (Horizontal > 0)
            {
                flipH(1);
            }
            else if (Horizontal < 0)
            {
                flipH(-1);
            }
        }
        void speedControll(float walkspeed, float runspeed)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                Char_speed = runspeed;
            }
            else { Char_speed = wSpeed; }
        }
        void flipH(int scale)
        {
            Vector3 characterScale = new(scale, 1, 1);
            transform.localScale = characterScale;
        }
        void animationCheck()
        {
            //if(rb2D.velocity.x == wSpeed && isGround || rb2D.velocity.x == -wSpeed && isGround)
            //{
            //    ani.SetInteger("Status",1);
            //}
            //else if (rb2D.velocity.x == rSpeed && isGround || rb2D.velocity.x == -rSpeed && isGround)
            //{
            //    ani.SetInteger("Status", 2);
            //}
            //else if (!isGround)
            //{
            //    ani.SetInteger("Status", 3);
            //}
            //else if(rb2D.velocity.x == 0 && rb2D.velocity.y == 0)
            //{
            //    ani.SetInteger("Status", 0);
            //}
            ani.SetFloat("Speed", Char_speed);
            ani.SetBool("Moving", CheckMoving());
            ani.SetBool("IsGround", isGround);
            fallDetector.transform.position = new Vector2(transform.position.x, fallDetector.transform.position.y);
        }
        void Vmovement()
        {
            if (Input.GetKeyDown(KeyCode.Space) && isGround)
            {
                rb2D.AddForce(Vector2.up * Char_JumpForce, ForceMode2D.Impulse);
            }
            
        }
        bool CheckMoving()
        {
            if (rb2D.velocity.x != 0)
                moving = true;
            else
            {
                moving = false;
            }
            return moving;
        }
        //void OnTriggerEnter2D(Collider2D collision)
        //{
        //    if (collision.tag == "FallDetector")
        //    {
                
        //        if (currentCheckpoint != null)
        //        {
        //            transform.position = currentCheckpoint.position;
        //        }
        //        else
        //        {              
        //            transform.position = respawnPoint;
        //        }
        //    }
        //    if (collision.tag == "Checkpoint")
        //    {
        //        previousCheckpoint = currentCheckpoint;
        //        currentCheckpoint = collision.transform;
        //    }
        //}


    }
}
