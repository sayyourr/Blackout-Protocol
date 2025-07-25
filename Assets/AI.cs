using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    public Transform player;
    public float HP = 1f;
    NavMeshAgent agent;
    public float attackRange = 10f;
    public float rayLength = 2.5f;
    public LayerMask playerLayer;
    public float attackCooldown = 1.5f;

    private float lastAttackTime;
    Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!transform.GetComponent<NavMeshAgent>().enabled)
        {
            return;
        }
        else
        {
            agent.SetDestination(player.position);

            float distance = Vector3.Distance(transform.position, player.position);

            if (HP <= 0)
            {
                StartCoroutine(Die());
            }

            if (distance <= attackRange)
            {
                // Direction from zombie to player
                Vector3 direction = (player.position - transform.position).normalized;

                // Raycast forward from zombie
                if (Physics.Raycast(transform.position + Vector3.up * 1.2f, direction, out RaycastHit hit, rayLength, playerLayer))
                {
                    if (hit.transform.CompareTag("Player"))
                    {
                        Attack();
                    }
                }
            }
        }
        

    }
    void Attack()
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            lastAttackTime = Time.time;
            animator.SetTrigger("Attack");
            player.GetComponent<Movement>().HP -= 0.34f;
        }
    }
    IEnumerator Die()
    {
        animator.SetBool("Dead", true);
        transform.GetComponent<NavMeshAgent>().enabled = false;
        yield return new WaitForSeconds(3f);
        player.GetComponent<Movement>().Kills++;
        Destroy(gameObject);
    }
}
