using UnityEngine;

public class CamerThirdPerson : MonoBehaviour {

    public Transform target;
    private Vector3 targetDistance;

    void Start()
    {
        targetDistance = transform.position - target.position;
        GameThirdPerson.Instance._currentController = 1;
    }

    //Changes the position and rotation of the camera and joystick controller
    void LateUpdate()
    {
        if(target.position.z < -20 && (int)transform.eulerAngles.y == 0)
        {
            transform.eulerAngles = new Vector3((int)transform.eulerAngles.x, 180.0f, 0);
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - targetDistance.z * 2);
            targetDistance = transform.position - target.position;
            GameThirdPerson.Instance._currentController = -1;
        }
        else if(target.position.z > 20 && (int)transform.eulerAngles.y == 180)
        {
            transform.eulerAngles = new Vector3((int)transform.eulerAngles.x, 0, 0);
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - targetDistance.z * 2);
            targetDistance = transform.position - target.position;
            GameThirdPerson.Instance._currentController = 1;
        }
        else 
            transform.position = target.position + targetDistance;
    }
}
