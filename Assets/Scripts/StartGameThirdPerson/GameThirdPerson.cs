using UnityEngine;
using UnityEngine.UI;

public class GameThirdPerson : MonoBehaviour, IGame {

    public static GameThirdPerson Instance;

    public int points = 0;
    public int lengthSnake = 0;

    private int _lastPoints = -1;
    private int _lastLengthSnake = -1;

    public float _currentSpeed = 16;
    public int _currentController = 1;

    public Text TextLengthSnake;
    public Text TextScore;
    public Text TextLife;

    public void Awake()
    {
        Instance = this;

        points = SingletonGame.Instance.points;

        CreateSquareField();
    }

    void Start()
    {
        TextLife.text = "Life: " + SingletonGame.Instance.lifeSnake;
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

    void Update()
    {
        TextDisplay();
    }

    // Display text and increases the speed
    public void TextDisplay()
    {
        // Update the displayed text points only when they change
        if (_lastPoints == points && _lastLengthSnake == lengthSnake) return;

        TextScore.text = "Score: " + points;

        TextLengthSnake.text = "Length Snake: " + lengthSnake;

        _lastPoints = points;
        _lastLengthSnake = lengthSnake;

        _currentSpeed = _currentSpeed + 1;
    }

    public void ExitGame()
    {
        GameController.ExitGame();
    }

}


