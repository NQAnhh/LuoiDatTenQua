using GameProject;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using CharacterController = GameProject.CharacterController;

public class ComboAttack : MonoBehaviour
{
    Animator ani;
    bool trigger;
    public int combo;
    public bool attacking;
    public int comboNumber;
    public float comboTiming;
    public float comboTempo;
    BoxCollider2D boxCollider;
    [SerializeField] Transform hitPoint;
    [SerializeField] GameObject[] hitCombo;
    public LayerMask enemyLayer;  // Để chỉ định lớp "Enemy".
    public float attackRadius = 0.5f;  // Bán kính của khu vực tấn công.
    [SerializeField] Transform attackPoint;  // Điểm từ đâu chúng ta kiểm tra va chạm.

    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        combo = -1;
        comboTiming = 0.5f;
        comboTempo = comboTiming;
        comboNumber = 4;
    }

    // Update is called once per frame
    void Update()
    {
        Combo();
    }
    public void Combo()
    {
        //Giảm combo Tempo theo thời gian khung hình time.datatime;
        comboTempo -= Time.deltaTime;
        //Nếu có lệnh tấn công sẽ thực hiện combo
        if (Input.GetKeyDown(KeyCode.Q) && comboTempo < 0)
        {
            //Bật trạng thái tấn công
            attacking = true;
            //Kích hoạt animation tấn công
            ani.SetTrigger("Attack" + combo);
            //Combotempo = comboTinming
            comboTempo = comboTiming;
        }
        //else if chưa hết thời gian kích hoạt combo
        else if (Input.GetKeyDown(KeyCode.Q) && comboTempo > 0 && comboTempo < 0.3)
        {
            //Đặt trạng thái tấn công
            attacking = true;
            // Tăng giá trí biến đếm combo và kiểm tra xem đã vưới giới hạn combo chưa
            combo++;

            //Nếu đã đạt giới hạn combo thì set combo về 1
            if (combo > comboNumber)
            {
                combo = 1;
            }

            //Show ra animation tấn công
            ani.SetTrigger("Attack" + combo);
            //Thiết lập lại giá trị comboTiming
            comboTempo = comboTiming;
        }
        //Nếu không có lệnh tấn công nào được thực hiện thì trạng thái tấn công tắt
        else if (comboTempo < 0 && !Input.GetKeyDown(KeyCode.Q))
        {
            attacking = false;
        }
        //Nếu combo tempo bé hơn 0, thì reset giá trị combo về 1 để bắt đầu thực hiện combo tới
        if (comboTempo < 0)
        {
            combo = 1;
        }

    }

    public void ApplyDamageToEnemy()
    {
        // Kiểm tra tất cả "Enemy" trong khu vực tấn công.
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, enemyLayer);

        // Gây sát thương cho mỗi "Enemy" trong khu vực tấn công.
        foreach (Collider2D enemy in hitEnemies)
        {
            Health enemyHealth = enemy.GetComponent<Health>();
            if (enemyHealth)
            {
                enemyHealth.TakeDamage(1);
            }
        }
    }
}
