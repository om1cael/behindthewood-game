using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    [SerializeField] private InputActionReference playButton;
    [SerializeField] private TextMeshProUGUI anyKeyToPlayText;

    void Update()
    {
        if(playButton.action.triggered) {
            anyKeyToPlayText.text = "Loading...";
            SceneManager.LoadSceneAsync(1);
        }
    }
}
