using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private AIHealthBar healthBar;


    public Transform player;
    public float detectionRange;
    public float attackRange;
    //public float moveToAttackRange;
    // bo chay
    public float fleeDistance;


    // attack
    private bool fire;


    


   
    // do uu tien tan cong
    //private float attackPriority; // Độ ưu tiên tấn công thực te
    //private float baseAttackPriority = 1f; // Độ ưu tiên tấn công cơ bản
    //private float attackPriorityIncrease = 0.5f; // Số lượng tăng độ ưu tiên khi điều kiện thích hợp

    private NavMeshAgent agent;
    private AIShooting aIShooting;

    private enum TankState { Idle,  // dung yen
                             Chasing, // duoi theo
                             Attacking, // tan cong
                             //MovingToAttack, // tim kiem muc tieu
                             Fleeing } // bo chay


    private TankState currentState = TankState.Idle;

    private int maxHealth = 100;
    private int currentHealth;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        aIShooting = GetComponent<AIShooting>();
    }

    private void OnEnable()
    {
        currentHealth = maxHealth;
        //attackPriority = baseAttackPriority;
    }


    void Update()
    {
        if (!healthBar.isLowHealthThreshold())
        {
            DecideStateAttack();
        }
        else if(!healthBar.isCriticalHealthThreshold() && PlayerInDetectionRange())
        {
            DecideStateAttack();
        }
        else if(healthBar.isCriticalHealthThreshold())
        {
            DecideStateDefend();
        }

        HandleCurrentState();

        //Debug.Log(attackPriority);
    }

    void DecideStateAttack()
    {
        if (PlayerInDetectionRange())
        {

            // neu khoang cach gan hon kc tan cong => tan cong
            if (PlayerInAttackRange())
            {
                currentState = TankState.Attacking;
            }
            // neu mau qua thap => chay
            else if(healthBar.isCriticalHealthThreshold())
            {
                currentState = TankState.Fleeing;
            }
            
            // neu mau du nhieu => duoi theo
            else if (!healthBar.isCriticalHealthThreshold())
            {
                currentState = TankState.Chasing;
            }
        }
        // neu khong -> move random
        else
        {
            currentState = TankState.Idle;
        }
    }

    void DecideStateDefend()
    {
        // Logic cho trạng thái phòng thủ
        // neu qua gan => tan cong
        if (PlayerInAttackRange())
        {
            currentState = TankState.Attacking;
        }
        // neu gan ma mau it -> chay
        else if (PlayerInDetectionRange())
        {
            currentState = TankState.Fleeing;
        }
        // neu khong => move random
        else
        {
            currentState = TankState.Idle;
        }
    }

    void HandleCurrentState()
    {
        switch (currentState)
        {
            case TankState.Idle:
                IdleState();
                break;

            case TankState.Chasing:
                ChasingState();
                break;

            case TankState.Attacking:
                AttackingState();
                break;

            case TankState.Fleeing:
                FleeingState();
                break;
        }

        //Debug.Log(currentState);
    }

    void IdleState()
    {
        // Logic cho trạng thái nhàn rỗi
        //Debug.Log("Idle");
    }

    void ChasingState()
    {
        if (PlayerInDetectionRange())
        {
            // neu kc qua gan => tan cong
            if (PlayerInAttackRange())
            {
                currentState = TankState.Attacking;

            }
            // dang duoi theo ma bi mat mau nhieu => chay
            else if (healthBar.isCriticalHealthThreshold())
            {
                currentState = TankState.Fleeing;

                // reset agent
                
            }
            else
            {
                agent.SetDestination(player.position);
                /*
                if (PlayerInAttackRange())
                {
                    currentState = TankState.Attacking;
                }
                else if (distanceToPlayer > moveToAttackRange)
                {
                    currentState = TankState.MovingToAttack;
                }*/
            }
        }
        else
        {
            currentState = TankState.Idle;
        }
    }

    void AttackingState()
    {
        agent.ResetPath();
        if (PlayerInAttackRange())
        {
            aIShooting.SetFire(true);
        }
        else
        {
            aIShooting.SetFire(false);
        }

        if (!PlayerInAttackRange() && PlayerInDetectionRange())
        {
            currentState = TankState.Chasing;
        }
        else
        {
            currentState = TankState.Idle;
        }
    }
    /*
    void MovingToAttackState()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (PlayerInAttackRange())
        {
            currentState = TankState.Attacking;
        }
        else if (distanceToPlayer <= closeAttackRange)
        {
            currentState = TankState.Attacking;
        }
        else if (distanceToPlayer <= moveToAttackRange)
        {
            currentState = TankState.Chasing;
        }
    }
    */
    void FleeingState()
    {
        agent.ResetPath();

        if (PlayerInAttackRange())
        {
            currentState = TankState.Attacking;

            // tang do uu tien tan cong
            //attackPriority += attackPriorityIncrease;
        }
        else if(!PlayerInAttackRange() && PlayerInDetectionRange())
        {
            Vector3 fleeDirection = transform.position - player.position;
            Vector3 targetPosition = transform.position + new Vector3(fleeDirection.x, 0, fleeDirection.z).normalized * fleeDistance;
            agent.SetDestination(targetPosition);
            Debug.Log(targetPosition);
        }
        else
        {
            currentState = TankState.Idle;
        }
    }

    bool PlayerInDetectionRange()
    {
        return Vector3.Distance(transform.position, player.position) <= detectionRange;
    }

    bool PlayerInAttackRange()
    {
        return Vector3.Distance(transform.position, player.position) <= attackRange;
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Destroy(gameObject);
        }
    }

    void OnDrawGizmosSelected()
    {
        // Detection Range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);


        // Attack Range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        // Attack Range
        //Gizmos.color = Color.green;
        //Gizmos.DrawWireSphere(transform.position, moveToAttackRange);
    }
}