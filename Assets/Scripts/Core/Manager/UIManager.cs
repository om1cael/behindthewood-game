using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Inventory UI")]
    [SerializeField] private TextMeshProUGUI plankAmount;
    [SerializeField] private TextMeshProUGUI nailAmount;
    [SerializeField] private TextMeshProUGUI hammerText;
    [Header("Building UI")]
    [SerializeField] private TextMeshProUGUI insufficientResourcesText;
    [Header("Note UI")]
    [SerializeField] private GameObject notePanel;
    [SerializeField] private TextMeshProUGUI noteText;
    [Header("Message UI")]
    [SerializeField] private GameObject messagePanel;
    [SerializeField] private TextMeshProUGUI messageText;
    [Header("Death Container")]
    [SerializeField] private GameObject deathContainer;
    [SerializeField] private Button restartSceneBtn;
    [Header("Input")]
    [SerializeField] private InputActionReference closeButton;

    private bool _showedLighterTutorial = false;

    void OnEnable()
    {
        GameEvents.OnPlankUpdate += UpdatePlankUI;
        GameEvents.OnNailUpdate += UpdateNailUI;
        GameEvents.OnAcquireHammer += SetHammerAcquired;
        GameEvents.OnHammerHealthChange += UpdateHammerHealth;
        GameEvents.OnHammerDestroyed += SetHammerDestroyed;
        GameEvents.OnInsufficientResources += ShowInsufficientResourcesText;
        GameEvents.OnReadNote += ShowNote;

        GameEvents.OnHouseEntered += ShowItemInteractionTutorial;
        GameEvents.OnHouseInvaded += ShowDeathContainer;
        GameEvents.OnRitualItemPicked += ShowRitualItemPicked;
    }

    void OnDisable()
    {
        GameEvents.OnPlankUpdate -= UpdatePlankUI;
        GameEvents.OnNailUpdate -= UpdateNailUI;
        GameEvents.OnAcquireHammer -= SetHammerAcquired;
        GameEvents.OnHammerHealthChange -= UpdateHammerHealth;
        GameEvents.OnHammerDestroyed -= SetHammerDestroyed;
        GameEvents.OnInsufficientResources -= ShowInsufficientResourcesText;
        GameEvents.OnReadNote -= ShowNote;

        GameEvents.OnHouseEntered -= ShowItemInteractionTutorial;
        GameEvents.OnHouseInvaded -= ShowDeathContainer;
        GameEvents.OnRitualItemPicked -= ShowRitualItemPicked;
    }

    void Update()
    {
        if(notePanel.activeSelf && closeButton.action.triggered) {
            notePanel.SetActive(false);
            GameEvents.OnNoteClosed?.Invoke();

            if(!_showedLighterTutorial) {
                ShowLighterTutorial();
                _showedLighterTutorial = true;
            }
        }
    }

    void UpdatePlankUI(int amount) {
        plankAmount.text = amount.ToString();
    }

    void UpdateNailUI(int amount) {
        nailAmount.text = amount.ToString();
    }

    void SetHammerAcquired() {
        hammerText.text = "100%";
    }

    void SetHammerDestroyed() {
        hammerText.text = "No";
    }

    void UpdateHammerHealth(int health) {
        hammerText.text = health + "%";
    }

    void ShowInsufficientResourcesText() {
        if(!insufficientResourcesText.IsActive()) {
            insufficientResourcesText.gameObject.SetActive(true);
            Invoke("HideInsufficientResourcesText", 1.0f);
        }
    }

    void HideInsufficientResourcesText() {
        insufficientResourcesText.gameObject.SetActive(false);
    }

    void ShowNote(string text) {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        noteText.text = text;
        notePanel.SetActive(true);
    }

    void ShowLighterTutorial() {
        messageText.text = "Press X to activate your lighter";
        messagePanel.SetActive(true);

        StartCoroutine(DisableMessagePanel());
    }

    void ShowRitualItemPicked() {
        messageText.text = "You found a ritual item!";
        messagePanel.SetActive(true);

        StartCoroutine(DisableMessagePanel());
    }

    void ShowItemInteractionTutorial() {
        messageText.text = "Press E to interact with objects";
        messagePanel.SetActive(true);

        StartCoroutine(DisableMessagePanel());
    }

    void ShowDeathContainer() {
        deathContainer.SetActive(true);

        restartSceneBtn.onClick.AddListener(() => {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    IEnumerator DisableMessagePanel() {
        yield return new WaitForSeconds(5.0f);
        messagePanel.SetActive(false);
    }
}
