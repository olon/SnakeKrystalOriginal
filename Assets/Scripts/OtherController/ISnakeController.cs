using UnityEngine;

public interface ISnakeController {

    void SnakeRotation(int firstRotation, int secondRotation, int rightTurn);
    void OnControllerColliderHit(ControllerColliderHit other);
    void AddNewBody();
    void CheckButtonClick();
    void TrackNextCell();
    void MoveAndRotate();
}
