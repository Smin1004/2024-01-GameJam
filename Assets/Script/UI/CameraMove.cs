using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public int[,] mapSize;
    public bool isTop;
    [SerializeField] private int clampX;
    [SerializeField] private int clampY;

    private void Update()
    {
        if (isTop)
        {
            transform.position = new Vector3
            (Mathf.Clamp(Player.Instance.transform.position.x, clampX, mapSize.GetLength(0) - clampX),
            12, Mathf.Clamp(Player.Instance.transform.position.z, clampY - 3, mapSize.GetLength(1) - clampY) - 3);
            transform.rotation = Quaternion.Euler(90, 0, 0);
        }
        else
        {
            transform.position = new Vector3
            (Mathf.Clamp(Player.Instance.transform.position.x, clampX, mapSize.GetLength(0) - clampX),
            12, Mathf.Clamp(Player.Instance.transform.position.z, clampY, mapSize.GetLength(1) - clampY) - 13);
            transform.rotation = Quaternion.Euler(60, 0, 0);
        }
    }
}
