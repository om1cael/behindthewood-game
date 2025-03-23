using System.Collections;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [Header("Attack")]
    [SerializeField] private byte attackStrength;
    [Header("Check Settings")]
    [SerializeField] private Transform[] checkPoints;
    [SerializeField] private byte defaultCheckInterval;
    [SerializeField] private byte initialCheckInterval;
    private byte checkInterval;
    private float _checkTime;
    [Header("Sound Settings")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip punchAudio;
    [SerializeField] private AudioClip invasionAudio;
    [Header("Scene Settings")]
    [SerializeField] private LayerMask placeableLayerMask;

    [SerializeField] private bool isActivated = false;

    private byte _maxPunchAmount = 4;

    void Start()
    {
        checkInterval = initialCheckInterval;
        isActivated = false;
    }

    void OnEnable()
    {
        GameEvents.OnHouseEntered += ActivateMonster;
    }

    void OnDisable()
    {
        GameEvents.OnHouseEntered -= ActivateMonster;
    }

    void Update()
    {
        if(!isActivated) return;
        _checkTime += Time.deltaTime;

        if(_checkTime >= checkInterval)
        {
            SetNewPosition();
            CheckDamageable();

            if (checkInterval == initialCheckInterval)
            {
                checkInterval = defaultCheckInterval;
            }

            _checkTime = 0f;
        }
    }

    private void SetNewPosition()
    {
        Transform checkPoint = checkPoints[Random.Range(0, checkPoints.Length)];
        transform.position = checkPoint.position;
        transform.rotation = Quaternion.Euler(0, checkPoint.localRotation.eulerAngles.y, 0);
    }

    void CheckDamageable() {
        if(Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, placeableLayerMask)) {
            IDamageable damageable = hit.collider.gameObject.GetComponent<IDamageable>();
            if(damageable == null) return;

            Attack(damageable);
            InvadeIfNotProtected(damageable);
        }
    }

    void Attack(IDamageable damageable) {
        damageable.Damage(attackStrength);
        StartCoroutine(playPunchAudio(Random.Range(1, _maxPunchAmount)));
        audioSource.PlayOneShot(invasionAudio);
    }

    void InvadeIfNotProtected(IDamageable damageable) {
        if(damageable.IsProtected()) return;

        GameEvents.OnHouseInvaded?.Invoke();
        isActivated = false;
    }

    void ActivateMonster() 
    {
        isActivated = true;
    }

    IEnumerator playPunchAudio(int times) {
        for(int i = 0; i < times; i++) {
            audioSource.PlayOneShot(punchAudio);
            yield return new WaitForSeconds(1f);
        }
    }
}
