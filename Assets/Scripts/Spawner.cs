using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject ballPrefab;
    public Transform spawnArea;


    public float ballRadius = 0.5f;
    public Color[] ballColors;

    private void Start()
    {
        SpawnBalls();
    }

    private void SpawnBalls()
    {
        int ballAmount = PlayerPrefs.GetInt("BallCount");
        for (int i = 0; i < ballAmount; i++)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(spawnArea.position.x - spawnArea.localScale.x / 2, spawnArea.position.x + spawnArea.localScale.x / 2),
                Random.Range(spawnArea.position.y + 1f, spawnArea.position.y + spawnArea.localScale.y / 2),
                Random.Range(spawnArea.position.z - spawnArea.localScale.z / 2, spawnArea.position.z + spawnArea.localScale.z / 2)
            );


            GameObject ball = Instantiate(ballPrefab, randomPosition, Quaternion.identity);
            Renderer ballRend = ball.GetComponent<Renderer>();
            ballRend.material.color = ballColors[Random.Range(0, ballColors.Length)];
            ball.transform.localScale = Vector3.one * ballRadius * 2f;
        }
    }
}
