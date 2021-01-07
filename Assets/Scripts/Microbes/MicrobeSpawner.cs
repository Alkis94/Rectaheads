using UnityEngine;
using System.Collections;

public class MicrobeSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject virus;
    [SerializeField]
    private GameObject bacteria;
    [SerializeField]
    private GameObject fungi;
    [SerializeField]
    private bool spawnVirus = false;
    [SerializeField]
    private bool spawnBacteria = true;
    [SerializeField]
    private bool spawnFungi = false;
    [SerializeField]
    private float virusSpawnRate = 3;
    [SerializeField]
    private float bacteriaSpawnRate = 3;
    [SerializeField]
    private float fungiSpawnRate = 3;

    private void Start()
    {
        if(spawnVirus)
        {
            StartCoroutine(SpawnMicrobe(virus, virusSpawnRate));
        }

        if (spawnBacteria)
        {
            StartCoroutine(SpawnMicrobe(bacteria, bacteriaSpawnRate));
        }

        if (spawnFungi)
        {
            StartCoroutine(SpawnMicrobe(fungi, fungiSpawnRate));
        }
    }

    private IEnumerator SpawnMicrobe (GameObject microbe, float spawnRate)
    {
        while(true)
        {
            yield return new WaitForSeconds(spawnRate);
            Vector3 randomRectaheadLocation = RectaheadManager.Instance.RandomRectaheadLocation();
            Vector3 targetRectaheadLocation = RectaheadManager.Instance.RandomRectaheadLocation();
            while(randomRectaheadLocation == targetRectaheadLocation)
            {
                targetRectaheadLocation = RectaheadManager.Instance.RandomRectaheadLocation();
            }

            Vector3 spawnPosition = new Vector3(randomRectaheadLocation.x, randomRectaheadLocation.y, 0);
            GameObject newMicrobe = Instantiate(microbe, spawnPosition, Quaternion.identity, transform);
            newMicrobe.GetComponent<Microbe>().TargetRectahead = targetRectaheadLocation;
        }
    }


}
