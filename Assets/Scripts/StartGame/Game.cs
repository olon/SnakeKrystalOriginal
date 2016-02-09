using UnityEngine;

public class Game : MonoBehaviour, IGame {

    public static Game Instance;

    public int points = 0;
    public int lengthSnake = 0;
 
    private int _lastPoints = -1;
    private int _lastLengthSnake = -1;

    public float _currentSpeed = 16;

    public void Awake()
    {
        Instance = this;

        points = SingletonGame.Instance.points;

        CreateSquareField();
    }

    public void Start()
    {
        UILabel lableLifeSnake = GameObject.Find("LabelLifeLeft").GetComponent<UILabel>();
        lableLifeSnake.text = "Life: " + SingletonGame.Instance.lifeSnake;
    }

    // Draw a square grid
    public void CreateSquareField()
    {
        for (int i = 0; i < 31; i++)
        {
            Instantiate(Resources.Load("Prefabs/LineVertical"),
                        new Vector3(-75.0f + i * 5.0f, -3.0f, 0.0f),
                        Quaternion.identity);
            if (i < 24)
            {
                Instantiate(Resources.Load("Prefabs/LineHorizontal"),
                            new Vector3(0.0f, -3.0f, -55.0f + i * 5.0f),
                            Quaternion.Euler(0.0f, 90.0f, 0.0f));
            }
        }
    }

    public void Update()
    {
        TextDisplay();
    }

    // Display text and increases the speed
    public void TextDisplay()
    {
        // Update the displayed text points only when they change
        if (_lastPoints == points && _lastLengthSnake == lengthSnake) return;

        UILabel lableScore = GameObject.Find("LabelScore").GetComponent<UILabel>();
        lableScore.text = "Score: " + points;

        UILabel lableLengthSnake = GameObject.Find("LabelBodySnake").GetComponent<UILabel>();
        lableLengthSnake.text = "Length Snake: " + lengthSnake;

        _lastPoints = points;
        _lastLengthSnake = lengthSnake;

        _currentSpeed = _currentSpeed + 1;
    }
}
