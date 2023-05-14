using UnityEngine;

public struct PhxEventModel {

    public EntityIDArgs one;
    public EntityIDArgs two;

    public Vector2 normal;
    public int layerMask_one;
    public int layerMask_two;

    public PhxEventModel(in EntityIDArgs one, in EntityIDArgs two, in Vector2 normal, int layerMask_one, int layerMask_two) {
        this.one = one;
        this.two = two;
        this.normal = normal;
        this.layerMask_one = layerMask_one;
        this.layerMask_two = layerMask_two;
    }

    public override string ToString() {
        return $"PHX - 碰撞事件\none:{one}\ntwo:{two}\nnormal:{normal}\nlayerMask_one:{layerMask_one}\nlayerMask_two:{layerMask_two}";
    }

}