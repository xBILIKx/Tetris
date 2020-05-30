using UnityEngine;

public class FigureControle : MonoBehaviour
{
    float posY;
    Vector2 startPos;
    public Vector3 rotatePoint;
    float previosTime;
    public float fallSpeed = 0.8f;
    bool mouseCheckerX;
    bool mouseCheckerY;
    bool lastPosition = true;
    bool mousePressed = true;
    static int width = 10;
    static int height = 20;
    static Transform[,] grid = new Transform[width, height];
    void Update()
    {
        if (lastPosition == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                startPos = Input.mousePosition;
                mouseCheckerX = true;
                mouseCheckerY = true;
            }
            else if (Input.GetMouseButton(0))
            {
                float posX = Input.mousePosition.x - startPos.x;
                posY = Input.mousePosition.y - startPos.y;
                if (posX < 0 && mouseCheckerX == true)
                {
                    transform.position += new Vector3(-1, 0, 0);
                    posY = 0;
                    mouseCheckerX = false;
                    mouseCheckerY = false;
                    mousePressed = false;
                    FindObjectOfType<AudioController>().leftSwipe.Play();
                    if (!ValidMove())
                        transform.position -= new Vector3(-1, 0, 0);
                }
                else if (posX > 0 && mouseCheckerX == true)
                {
                    transform.position += new Vector3(1, 0, 0);
                    posY = 0;
                    mouseCheckerX = false;
                    mouseCheckerY = false;
                    mousePressed = false;
                    FindObjectOfType<AudioController>().rightSwipe.Play();
                    if (!ValidMove())
                        transform.position -= new Vector3(1, 0, 0);
                }
                else if (posX == 0)
                    mousePressed = true;
            }
            else if (Input.GetMouseButtonUp(0) && mousePressed == true)
            {
                transform.RotateAround(transform.TransformPoint(rotatePoint), new Vector3(0, 0, 1), -90);
                FindObjectOfType<AudioController>().rotate.Play();
                if (!ValidMove())
                    transform.RotateAround(transform.TransformPoint(rotatePoint), new Vector3(0, 0, 1), 90);

                mousePressed = true;
            }


            if (Time.time - previosTime > (posY < 0 && mouseCheckerY == true ? fallSpeed / 10 : fallSpeed))
            {
                mouseCheckerX = false;
                mousePressed = false;
                posY = 0;
                transform.position += new Vector3(0, -1, 0);
                previosTime = Time.time;
                if (!ValidMove())
                {
                    transform.position -= new Vector3(0, -1, 0);
                    lastPosition = false;
                    FindObjectOfType<AudioController>().drop.Play();
                    AddToGrid();
                    CheckForLines();
                    FindObjectOfType<TextScore>().score += 5;
                    if (grid[Mathf.RoundToInt(transform.position.x), 18] == null)
                        FindObjectOfType<FigureSpawner>().SpawnFigure();
                    else
                    {
                        FindObjectOfType<AudioController>().gameLosse.Play();
                        FindObjectOfType<TextScore>().GameEnd();
                    }
                }
            }
        }
    }
    void CheckForLines()
    {
        for (int i = height - 1; i >= 0; i--)
        {

            if (HasLine(i))
            {
                FindObjectOfType<AudioController>().lineClear.Play();
                DeleteLine(i);
                RowDown(i);
                FindObjectOfType<TextScore>().score += 95;
            }
        }
    }
    bool HasLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            if (grid[j, i] == null)
                return false;
        }

        return true;
    }
    void DeleteLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            Destroy(grid[j, i].gameObject);
            grid[j, i] = null;
        }
    }
    void RowDown(int i)
    {
        for (int y = i; y < height; y++)
        {
            for (int j = 0; j < width; j++)
            {
                if (grid[j, y] != null)
                {
                    grid[j, y - 1] = grid[j, y];
                    grid[j, y] = null;
                    grid[j, y - 1].gameObject.transform.position -= new Vector3(0, 1, 0);
                }
            }
        }
    }
    void AddToGrid()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            grid[roundedX, roundedY] = children;

        }
    }

    bool ValidMove()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            if (roundedX < 0 || roundedX >= width || roundedY < 0 || roundedY >= height)
                return false;

            if (grid[roundedX, roundedY] != null)
                return false;

        }

        return true;
    }
}
