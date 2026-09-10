using UnityEngine;
using UnityEngine.InputSystem;

public class QuitHandler : MonoBehaviour
{

    public GameObject exitButton;
    public void Start()
    {
        exitButton.SetActive(false);
    }
    
    public void Update()
    {
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            //Überprüfen ob Button schon aktiviert wurde
           bool isCurrentlyActive = exitButton.activeSelf;
           exitButton.SetActive(!isCurrentlyActive);
        }
    }

    public void QuitGame()
    {
        Application.Quit();

        //Stoppt den Playmodus, wenn man im Unity Editor testet
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

}
