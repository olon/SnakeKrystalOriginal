using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonMenuHelper : MonoBehaviour {

    public Text _textCamera;
    public Image _backgroundImage;

    void Awake()
    {
        _backgroundImage = GetComponent<Image>();
        OnChangeCameraClick();
        SingletonGame.Instance.points = 0;
        SingletonGame.Instance.lifeSnake = 3;
    }
    
    public void OnStartGameClick()
    {
        if(SingletonGame.Instance._cameraSwitch == 1)
            GameController.LoadLevelSnakeThirdPerson();
        else
            GameController.LoadLevelSnake();
    }

    public void OnChangeCameraClick()
    {
        switch(SingletonGame.Instance._cameraSwitch)
        {
            case 0:
                {
                    _textCamera.text = "Camera:" + Environment.NewLine + "Third Person";
                    _backgroundImage.sprite = Resources.Load<Sprite>("backgroundThirdPerson");
                    SingletonGame.Instance._cameraSwitch = 1;
                }
            break;
            case 1:
                {
                    _textCamera.text = "Camera:" + Environment.NewLine + "Classic Camera";
                    _backgroundImage.sprite = Resources.Load<Sprite>("backgroundClassicGame");
                    SingletonGame.Instance._cameraSwitch = 0;
                }
            break;
        }
    }

    public void OnExitGameClick()
    {
        GameController.ExitGame();
    }
}
