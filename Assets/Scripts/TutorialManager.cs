using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject tutorialPanel;
    public GameObject playerCharacter;
    public GameObject sticksContainer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(playerCharacter!=null) playerCharacter.SetActive(false);
        if(sticksContainer!=null) sticksContainer.SetActive(false);

        if(tutorialPanel!=null) tutorialPanel.SetActive(true);
    }
    public void StartGame()
    {
        tutorialPanel.SetActive(false);
        playerCharacter.SetActive(true);
        sticksContainer.SetActive(true);
    }
}
