using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class text : MonoBehaviour
{
    [SerializeField] private Text moveText;
    public int maxCount;

    private int moveCount;

    public void Init(){
        moveCount = maxCount;
        moveText.text = $"{moveCount} / {maxCount}";
    }

    public int m(){
        moveCount--;
        moveText.text = $"{moveCount} / {maxCount}";

        return moveCount;
    }
}
