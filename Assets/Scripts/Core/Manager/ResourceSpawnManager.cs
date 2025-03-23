using UnityEngine;

public class ResourceSpawnManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private byte timeBetweenSpawns;
    private float _timePassed;
    [Header("Spawn Positions")]
    [SerializeField] private Transform plankSpawnPosition;
    [SerializeField] private Transform nailSpawnPosition;
    [Header("Prefabs")]
    [SerializeField] private GameObject spawnablePlankPrefab;
    [SerializeField] private GameObject spawnableNailPrefab;

    void Start()
    {
        InstantiateItems();
    }

    void Update()
    {
        _timePassed += Time.deltaTime;

        if(_timePassed >= timeBetweenSpawns) {
            InstantiateItems();
            _timePassed = 0;
        }
    }

    void InstantiateItems() {
        Instantiate(spawnablePlankPrefab, plankSpawnPosition.position, Quaternion.Euler(plankSpawnPosition.eulerAngles));
        Instantiate(spawnableNailPrefab, nailSpawnPosition.position, Quaternion.Euler(nailSpawnPosition.eulerAngles));
    }
}
