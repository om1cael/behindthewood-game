using UnityEngine;
using UnityEngine.InputSystem;

public class LighterController : MonoBehaviour
{
    [SerializeField] private InputActionReference lighterInput;
    private Animator animator;
    private AudioSource onOffAudio;

    void Start()
    {
        animator = GetComponent<Animator>();
        onOffAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(lighterInput.action.triggered) {
            ToggleLighter();
        }
    }

    void ToggleLighter() {
        animator.SetTrigger("Toggle");
        onOffAudio.Play();
    }
}
