using UnityEngine;
using UnityEngine.UI;

public class FullscreenToggle : MonoBehaviour
{
    [SerializeField] private Toggle toggle;

    void Awake()
    {
        toggle.onValueChanged.AddListener(OnToggleChanged);
        toggle.isOn = Screen.fullScreen;
    }

    private void OnToggleChanged(bool isOn)
    {
        Debug.Log("EVENTO REAL: " + isOn);

        Screen.fullScreenMode = isOn
            ? FullScreenMode.FullScreenWindow
            : FullScreenMode.Windowed;
    }
}