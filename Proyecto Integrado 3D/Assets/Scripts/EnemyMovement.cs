using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    public Transform Target;
    public float UpdateSpeed = 0.1f;

    public float catchDistance = 1.5f;
    public GameObject losePanel;

    private NavMeshAgent Agent;
    private bool isGameOver = false;

    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        // Reset total al reiniciar escena
        if (Agent != null)
        {
            Agent.ResetPath();
            Agent.isStopped = false;
        }

        isGameOver = false;
    }

    private void Start()
    {
        if (losePanel != null)
            losePanel.SetActive(false);

        StartCoroutine(FollowTarget());
    }

    private void Update()
    {
        if (isGameOver || Target == null) return;

        float distance = Vector3.Distance(transform.position, Target.position);

        if (distance <= catchDistance)
        {
            Lose();
        }
    }

    private void Lose()
    {
        if (isGameOver) return;

        isGameOver = true;

        if (losePanel != null)
            losePanel.SetActive(true);

        Time.timeScale = 0f;

        if (Agent != null)
            Agent.isStopped = true;
    }

    private IEnumerator FollowTarget()
    {
        WaitForSeconds wait = new WaitForSeconds(UpdateSpeed);

        while (enabled)
        {
            if (!isGameOver && Agent != null && Target != null)
            {
                Agent.SetDestination(Target.position);
            }

            yield return wait;
        }
    }
}