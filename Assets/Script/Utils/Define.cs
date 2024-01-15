using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum Scene
    {
        None,
        Title,
        Lobby,
        InGame,
    }

    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount,
    }

    public enum Layer
    {
        None = 6,
        Enemy = 7,
        Block = 8,

    }

    public enum CameraMode
    {
        None,
        QuarterView,
    }

    public enum KeyType
    {
        None,
        Up,
        Down,
        Right,
        Left,
    }

    public enum EndType
    {
        None,
        Victory,
        Clear,
        Over,
    }

    public enum MapType
    {
        Ground,
        Obj,
        Enemy,
    }

    public enum Weapon
    {
        one, two,
    }
}
