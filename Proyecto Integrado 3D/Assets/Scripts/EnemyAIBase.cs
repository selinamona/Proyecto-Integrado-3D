using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyAIBase : MonoBehaviour
{
    [Header("AI Configuration")]
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform target;

    private bool isGameOver = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            target = playerObj.transform;
    }

    private void Update()
    {
        if (isGameOver || target == null) return;

        agent.SetDestination(target.position);
        transform.LookAt(target);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isGameOver) return;

        if (other.CompareTag("Player"))
        {
            isGameOver = true;
            Time.timeScale = 1f; // importante para evitar bugs al reiniciar
            SceneManager.LoadScene(3);
        }
    }
}
