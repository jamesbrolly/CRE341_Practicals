using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    private UnityEngine.AI.NavMeshAgent agent;
    public int health = 50;
    public int damage = 20;
    public float attackRange = 2f;
    public float attackRate = 1.5f;
    private float nextAttackTime = 0f;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;

        agent.SetDestination(player.position);

        if (Vector3.Distance(transform.position, player.position) <= attackRange && Time.time >= nextAttackTime)
        {
            AttackPlayer();
            nextAttackTime = Time.time + attackRate;
        }
    }

    void AttackPlayer()
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
