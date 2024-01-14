using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public int[,] mapSize;
    [SerializeField] private int clampX;
    [SerializeField] private int clampY;

    private void Update()
    {
        transform.position = new Vector3
            (Mathf.Clamp(Player.Instance.transform.position.x, clampX, mapSize.GetLength(0) - clampX),
            Mathf.Clamp(Player.Instance.transform.position.z, clampY, mapSize.GetLength(1) - clampY), -10);
    }
}
