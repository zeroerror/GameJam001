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

    Vector2 chosenPoint;
    public Vector2 ChosenPoint => chosenPoint;
    public void SetChosenPoint(Vector2 value) => this.chosenPoint = value;

    public RoleInputComponent() { }

    public void Reset() {
        moveHorDir = 0;
        inputJump = false;
        inputPick = false;
        chosenPoint = Vector2.zero;
    }

}