using UnityEngine;

public class RoleInputComponent {

    int moveHorDir;
    public int MoveHorDir => moveHorDir;
    public void SetMoveHorDir(int value) => this.moveHorDir = value;

    bool inputJump;
    public bool InputJump => inputJump;
    public void SetInputJump(bool value) => this.inputJump = value;

    bool inputShoot;
    public bool InputShoot => inputShoot;
    public void SetInputShoot(bool value) => this.inputShoot = value;

    bool inputPick;
    public bool InputPick => inputPick;
    public void SetInputPick(bool value) => this.inputPick = value;

    Vector3 chosenPoint;
    public Vector3 ChosenPoint => chosenPoint;
    public void SetChosenPoint(Vector3 value) => this.chosenPoint = value;

    public RoleInputComponent() { }

    public void Reset() {
        moveHorDir = 0;
        inputJump = false;
        inputShoot = false;
        inputPick = false;
        chosenPoint = Vector2.zero;
    }

}