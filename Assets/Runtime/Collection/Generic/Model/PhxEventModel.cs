using UnityEngine;

public struct PhxEventModel {

    public EntityIDArgs one;
    public EntityIDArgs two;

    public PhxEventModel(in EntityIDArgs one, in EntityIDArgs two) {
        this.one = one;
        this.two = two;
    }

    public override string ToString() {
        return $"PHX - 碰撞事件\none:{one}\ntwo:{two}";
    }

}