using UnityEngine;

public class FigureSpawner : MonoBehaviour
{
    public GameObject[] Figures;
    private void Start()
    {
        FigureSpawn();
    }

    void FigureSpawn()
    {
        Instantiate(Figures[Random.Range(0, Figures.Length)], transform.position, Quaternion.identity);
    }

    public void SpawnFigure()
    {
        Instantiate(FindObjectOfType<NextFigureSpawner>().NextFigureSpawn(), transform.position, Quaternion.identity);
        FindObjectOfType<NextFigureSpawner>().DestroyFigure();
        FindObjectOfType<NextFigureSpawner>().GenerateNextFigure();
    }
}
