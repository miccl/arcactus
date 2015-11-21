using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public float yPosMin = 0.0f;
    public float yPosMax = 5.0f;

    public GameObject enemy;
    public float startWait = 1.0f;
    public float waveWait = 4.0f;
    public float spawnWait = 0.5f;
    public int waveCount = 10;

    public float spawnRadius = 100f;


    // Use this for initialization
    void Start () {
        StartCoroutine(SpawnWaves());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while(true)
        {
            for (int i = 0; i < waveCount; i++)
            {
                float alpha = Random.Range(0, Mathf.PI);

                float xPos = Mathf.Cos(alpha) * spawnRadius;
                float yPos = Random.Range(yPosMin, yPosMax);
                float zPos = Mathf.Sin(alpha) * spawnRadius;
               
                Vector3 spawnPosition = new Vector3(xPos, yPos, zPos);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(enemy, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
        }
    }
}
