﻿using UnityEngine;

// Player script needs a component object CharacterController
[RequireComponent(typeof(CharacterController))]
public class Snake : MonoBehaviour, ISnakeController
{
    private int currentRotation = 0;
    private int checkInputButton = -1;
     
    private CharacterController _controller;

    private bool _testing = false;
    private static bool _snake = true;

    private static int countSphereBodySnake = 0;

    public GameObject TailSnake;
    public GameObject Current;

    private int horizontalAxis = 0;
    private int verticalAxis = 0;

    private float startPositionZ = 2.5f;

    private float startPositionX = -2.5f;

    float lastPosZ = 2.5f;

    public void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    public void Update()
    {
        //hint
        if (transform.position.z - lastPosZ > 1f)
            transform.position = new Vector3(transform.position.x, transform.position.y, lastPosZ + 0.2f);

        currentRotation = Mathf.RoundToInt(transform.localEulerAngles.y);

        //Checking for button press and in what direction will turn
        CheckButtonClick();

        //Help keep track of the next cell on the field
        TrackNextCell();

        //Help with rotation and move forward
        MoveAndRotate();

        lastPosZ = transform.position.z;
    }

    public void CheckButtonClick()
    {
        if (Input.GetButtonUp("Horizontal") && checkInputButton == -1 && (currentRotation == 0 || currentRotation == 180))
        {
            horizontalAxis = (int)Mathf.Sign(Input.GetAxis("Horizontal"));
            if (horizontalAxis == 1 || horizontalAxis == -1)
                checkInputButton = 0;
        }
        if (Input.GetButtonUp("Vertical") && checkInputButton == -1 && (currentRotation == 90 || currentRotation == 270))
        {
            verticalAxis = (int)Mathf.Sign(Input.GetAxis("Vertical")) * -1;
            if (verticalAxis == 1 || verticalAxis == -1)
                checkInputButton = 1;
        }
    }

    public void TrackNextCell()
    {
        if (startPositionZ < transform.position.z && currentRotation == 0)
            startPositionZ += 5.0f;
        else if (startPositionZ > transform.position.z && currentRotation == 180)
            startPositionZ -= 5.0f;
        if (startPositionX < transform.position.x && currentRotation == 90)
            startPositionX += 5.0f;
        else if (startPositionX > transform.position.x && currentRotation == 270)
            startPositionX -= 5.0f;
    }

    public void MoveAndRotate()
    {
        if (checkInputButton == 0 && ((currentRotation == 0 && startPositionZ - 0.5f < transform.position.z) ||
                             (currentRotation == 180 && startPositionZ + 0.5f > transform.position.z)))
        {
            transform.position = new Vector3(startPositionX, -1, startPositionZ);
            SnakeRotation(0, 180, horizontalAxis);
            checkInputButton = -1;
        }
        else if (checkInputButton == 1 && ((currentRotation == 90 && startPositionX - 0.5f < transform.position.x) ||
                                         (currentRotation == 270 && startPositionX + 0.5f > transform.position.x)))
        {
            transform.position = new Vector3(startPositionX, -1, startPositionZ);
            SnakeRotation(90, 270, verticalAxis);
            checkInputButton = -1;
        }
        else
            _controller.Move(transform.forward * Game.Instance._currentSpeed * Time.deltaTime);
    }

    //help to turn the snake
    public void SnakeRotation(int firstRotation, int secondRotation, int rightTurn)
    {
        if (currentRotation == firstRotation)
        {
            transform.Rotate(0, 90.0f * rightTurn, 0);
        }
           
        if (currentRotation == secondRotation)
            transform.Rotate(0, -90.0f * rightTurn, 0);
    }

    //Controller. Help track collision snake
    public void OnControllerColliderHit(ControllerColliderHit other)
    {
        if (other.gameObject.tag == "Wall" || other.gameObject.GetComponent<Tail>())
        {
            SingletonGame.Instance.lifeSnake--;
            SingletonGame.Instance.points = Game.Instance.points;
            if (SingletonGame.Instance.lifeSnake == 0)
                GameController.GoToMenu();
            else
                GameController.LoadLevelSnake();
        }
    }

    //Method add new body snake
    public void AddNewBody()
    {
        Game.Instance.lengthSnake++;
        GameObject BodySnake = Instantiate(Resources.Load("Prefabs/BodySnake"),
            Current.transform.position - Current.transform.forward * 0.8f,
            transform.rotation) as GameObject;
        BodySnake.GetComponent<Tail>().target = Current.transform;
        TailSnake.GetComponent<Tail>().target = BodySnake.transform;
        Current = BodySnake;
    }
}

