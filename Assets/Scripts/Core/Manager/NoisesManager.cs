using UnityEngine;

public class NoisesManager : MonoBehaviour
{
    [SerializeField] private AudioSource noiseAudioSource;
    [SerializeField] private AudioClip[] noises;
    [SerializeField] private byte timeBetweenNoises;
    private float _timePassed;

    void Update()
    {
        _timePassed += Time.deltaTime;

        if(_timePassed >= timeBetweenNoises) {
            int _randomNumber = Random.Range(1, 6); // 20%

            if(_randomNumber == 1) {
                AudioClip clip = noises[Random.Range(0, noises.Length)];
                noiseAudioSource.PlayOneShot(clip);
            }

            _timePassed = 0;
        }
    }
}
