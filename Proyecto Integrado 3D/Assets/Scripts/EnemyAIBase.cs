using UnityEngine;
using UnityEngine.AI;

public class EnemyAIBase : MonoBehaviour
{
    [Header("AI")]
    public NavMeshAgent agent;
    public Transform target;

    [Header("Game Over")]
    public float catchDistance = 1.5f;
    public GameObject losePanel;

    private bool isGameOver = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        // Evita rotación automática rara
        agent.updateRotation = false;

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
            target = playerObj.transform;
    }

    private void Start()
    {
        if (losePanel != null)
            losePanel.SetActive(false);
    }

    private void Update()
    {
        if (isGameOver || target == null)
            return;

        // Seguir al jugador
        agent.SetDestination(target.position);

        // Distancia para perder
        float distance = Vector3.Distance(
            transform.position,
            target.position
        );

        if (distance <= catchDistance)
        {
            GameOver();
        }
    }

    private void GameOver()
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

    private void OnEnable()
    {
        isGameOver = false;

        if (agent != null)
        {
            agent.isStopped = false;
            agent.ResetPath();
        }
    }
}
