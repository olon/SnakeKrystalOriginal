using UnityEngine;
using System;

// Player script needs a component object CharacterController
[RequireComponent(typeof(CharacterController))]
public class SnakeThirdPersonControll : MonoBehaviour, ISnakeController {

    private int currentRotation = 0;
    private int checkInputButton = -1;
    private int joystickInput = -1;

    private CharacterController _controller;
    public VirtualJoystick joystick;

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
        if (transform.position.z - lastPosZ > 1.0f)
            transform.position = new Vector3(transform.position.x, transform.position.y, lastPosZ + 0.2f);

        currentRotation = Mathf.RoundToInt(transform.localEulerAngles.y);

        //Checking position joystick and in what direction will turn
        CheckButtonClick();

        //Help keep track of the next cell on the field
        TrackNextCell();

        //Help with rotation and move forward
        MoveAndRotate();

        lastPosZ = transform.position.z;
    }

    public void CheckButtonClick()
    {
        if (joystick.GetHorizontalInput() == 1 && checkInputButton == -1 && (currentRotation == 0 || currentRotation == 180))
        {
            horizontalAxis = (int)Mathf.Sign(joystick.AxisHorizontal()) * GameThirdPerson.Instance._currentController;
            if (horizontalAxis == 1 || horizontalAxis == -1)
                joystickInput = 0;
        }
        if (joystick.GetVerticalInput() == -1 && checkInputButton == -1 && (currentRotation == 90 || currentRotation == 270))
        {
            verticalAxis = (int)Mathf.Sign(joystick.AxisVertical()) * -1 * GameThirdPerson.Instance._currentController;
            if (verticalAxis == 1 || verticalAxis == -1)
                joystickInput = 1;
        }

        if (Input.GetButtonUp("Horizontal") && joystickInput == -1 && (currentRotation == 0 || currentRotation == 180))
        {
            horizontalAxis = (int)Mathf.Sign(Input.GetAxis("Horizontal")) * GameThirdPerson.Instance._currentController;
            if (horizontalAxis == 1 || horizontalAxis == -1)
                checkInputButton = 0;
        }
        if (Input.GetButtonUp("Vertical") && joystickInput == -1 && (currentRotation == 90 || currentRotation == 270))
        {
            verticalAxis = (int)Mathf.Sign(Input.GetAxis("Vertical")) * -1 * GameThirdPerson.Instance._currentController;
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
        if ((checkInputButton == 0 || joystickInput == 0) && ((currentRotation == 0 && startPositionZ - 0.5f < transform.position.z) ||
                             (currentRotation == 180 && startPositionZ + 0.5f > transform.position.z)))
        {
            transform.position = new Vector3(startPositionX, -1, startPositionZ);
            SnakeRotation(0, 180, horizontalAxis);
            checkInputButton = -1;
        }
        else if ((checkInputButton == 1 || joystickInput == 1) && ((currentRotation == 90 && startPositionX - 0.5f < transform.position.x) ||
                                         (currentRotation == 270 && startPositionX + 0.5f > transform.position.x)))
        {
            transform.position = new Vector3(startPositionX, -1, startPositionZ);
            SnakeRotation(90, 270, verticalAxis);
            checkInputButton = -1;
        }
        else
            _controller.Move(transform.forward * GameThirdPerson.Instance._currentSpeed * Time.deltaTime);
    }

    //Method helps to turn the snake
    public void SnakeRotation(int firstRotation, int secondRotation, int rightTurn)
    {
        if (currentRotation == firstRotation)
            transform.Rotate(0, 90.0f * rightTurn, 0);
        if (currentRotation == secondRotation)
            transform.Rotate(0, -90.0f * rightTurn, 0);
    }

    public void OnControllerColliderHit(ControllerColliderHit other)
    {
        if (other.gameObject.tag == "Wall" || other.gameObject.GetComponent<Tail>())
        {
            SingletonGame.Instance.lifeSnake--;
            SingletonGame.Instance.points = GameThirdPerson.Instance.points;

            if (SingletonGame.Instance.lifeSnake == 0)
                GameController.GoToMenu();
            else
                GameController.LoadLevelSnakeThirdPerson();
        }
    }

    //Method add new body snake
    public void AddNewBody()
    {
        GameThirdPerson.Instance.lengthSnake++;
        GameObject BodySnake = Instantiate(Resources.Load("Prefabs/BodySnake"),
            Current.transform.position - Current.transform.forward * 0.8f,
            transform.rotation) as GameObject;
        BodySnake.GetComponent<Tail>().target = Current.transform;
        TailSnake.GetComponent<Tail>().target = BodySnake.transform;
        Current = BodySnake;
    }
}
