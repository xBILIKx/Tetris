using UnityEngine;

public class NextFigureSpawner : MonoBehaviour
{
    public GameObject[] Figures;
    GameObject Figure;
    FigureControle fc;
    void Start()
    {
        GenerateNextFigure();
    }

    public void GenerateNextFigure()
    {
        Figure = Instantiate(Figures[Random.Range(0, Figures.Length)], transform.position, Quaternion.identity);
        fc = Figure.GetComponent<FigureControle>();
        fc.enabled = false;
    }

    public GameObject NextFigureSpawn()
    {
        fc.enabled = true;
        return Figure;
    }
    public void DestroyFigure()
    {
        Destroy(Figure);
    }
}
