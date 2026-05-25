using UnityEngine;

public class CanvasSwitcher : MonoBehaviour
{
    public GameObject[] canvases;

    private int currentCanvas = 0;

    void Start()
    {
        ShowCanvas(0);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            NextCanvas();
        }
    }

    void NextCanvas()
    {
        currentCanvas++;

        if (currentCanvas >= canvases.Length)
        {
            Debug.Log("Fin");
            return;
        }

        ShowCanvas(currentCanvas);
    }

    void ShowCanvas(int index)
    {
        for (int i = 0; i < canvases.Length; i++)
        {
            canvases[i].SetActive(false);
        }

        canvases[index].SetActive(true);
    }
}