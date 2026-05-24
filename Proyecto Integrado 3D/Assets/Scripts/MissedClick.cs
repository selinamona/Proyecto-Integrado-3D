using UnityEngine;

public class MissedClick : MonoBehaviour
{
    public GameObject Player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
        Player.GetComponent<Health>().CurrentHealth -= 1;
    }
}
