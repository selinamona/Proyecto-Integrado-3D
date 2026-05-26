using UnityEngine;

public class Note : MonoBehaviour
{
    public float assignedTime;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        // Get renderer immediately to avoid flickering
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // Current song time
        double songTime = SongManager.GetAudioSourceTime();

        // Time remaining before note should be hit
        double timeLeft = assignedTime - songTime;

        // Smooth normalized movement
        float t = Mathf.Clamp01(
            1f - (float)(timeLeft / SongManager.Instance.noteTime)
        );

        // Start and end positions
        Vector3 startPos = Vector3.up * SongManager.Instance.noteSpawnY;
        Vector3 endPos = Vector3.up * SongManager.Instance.noteTapY;

        // Smooth movement
        transform.localPosition = Vector3.Lerp(startPos, endPos, t);

        // Enable sprite once visible
        if (spriteRenderer != null && !spriteRenderer.enabled)
        {
            spriteRenderer.enabled = true;
        }

        // Destroy after passing hit line
        if (songTime > assignedTime + 0.3f)
        {
            Destroy(gameObject);
        }
    }
}