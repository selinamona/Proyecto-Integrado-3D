using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform Target;
    public float catchDistance = 1.5f;
    public GameObject losePanel;

    private NavMeshAgent agent;
    private bool isGameOver = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        // Desactiva la rotación automática
        agent.updateRotation = false;
    }

    private void OnEnable()
    {
        isGameOver = false;

        if (agent != null)
        {
            agent.ResetPath();
            agent.isStopped = false;
        }
    }

    private void Start()
    {
        if (losePanel != null)
            losePanel.SetActive(false);
    }

    private void Update()
    {
        if (isGameOver || Target == null)
            return;

        // El enemigo sigue al jugador
        agent.SetDestination(Target.position);

        // Distancia para perder
        float distance = Vector3.Distance(
            transform.position,
            Target.position
        );

        if (distance <= catchDistance)
        {
            Lose();
        }
    }

    private void Lose()
    {
        if (isGameOver)
            return;

        isGameOver = true;

        if (losePanel != null)
            losePanel.SetActive(true);

        if (agent != null)
            agent.isStopped = true;

        Time.timeScale = 0f;
    }
}