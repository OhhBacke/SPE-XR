using UnityEngine;
using System.Collections;

public class StartMenuManager : MonoBehaviour
{
    public CanvasGroup startPanelCanvasGroup;
    //dauer des Fades in sec
    public float fadeDuration =1.0f;
    
//speichert ob Game gestartet hat oder nicht
    public static bool isProgamStarted;
    
    void start()
    {   
        //setzt starten auf false
        isProgamStarted = false;
        //pausiert Physik und Zeitverlauf
        Time.timeScale = 0.0f;
        //Panel auf undurchsichtig stellen
        startPanelCanvasGroup.alpha =1.0f;
        //Lässt das Panel erscheinen
        startPanelCanvasGroup.gameObject.SetActive(true);
    }

    //diese Methode wird beim klicken auf den Startbutton aufgerufen
    public void startProgramm()
    {
                isProgamStarted = true;
        Time.timeScale = 1.0f;
        // Lässt das Panel verschwinden
        StartCoroutine(FadeOutRoutine());
    }

    public IEnumerator FadeOutRoutine()
    {
        //verhindert mehrfaches Ancklicken während Fade Out
        startPanelCanvasGroup.blocksRaycasts = false;
        float startAlpha = startPanelCanvasGroup.alpha;
        float timeElapsed =0.0f;
        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.unscaledDeltaTime;
            //Errechnet den neuen alphaWert stufenlos
            startPanelCanvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, timeElapsed / fadeDuration);
            //warten bis zum nächsten Frame
            yield return null;
        }
        startPanelCanvasGroup.alpha = 0.0f;
        //Blendet Object komplett aus um Kapazitäten zu sparen
        startPanelCanvasGroup.gameObject.SetActive(false);
        
    }

}
