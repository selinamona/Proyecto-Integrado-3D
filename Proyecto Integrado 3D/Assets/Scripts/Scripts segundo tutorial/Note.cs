using UnityEngine;

public class Note : MonoBehaviour
{
    public float assignedTime;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // Time until the note should be hit
        double timeLeft = assignedTime - SongManager.GetAudioSourceTime();

        // Normalize movement based on noteTime
        float t = 1f - (float)(timeLeft / SongManager.Instance.noteTime);

        // Move note from spawn position to tap position
        transform.localPosition = Vector3.Lerp(
            Vector3.up * SongManager.Instance.noteSpawnY,
            Vector3.up * SongManager.Instance.noteTapY,
            t
        );

        // Enable renderer once visible
        if (!spriteRenderer.enabled)
        {
            spriteRenderer.enabled = true;
        }

        // Destroy after passing the hit line
        if (t > 1.2f)
        {
            Destroy(gameObject);
        }
    }
}