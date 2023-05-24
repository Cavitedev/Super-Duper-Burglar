using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{


    [SerializeField] public PlayerStats amount;
    public TextMeshPro score;

    // Start is called before the first frame update
    private void Update()
    {
        score.text = PlayerStats.Instance.BountyAmount.ToString("f0") + "€";
    }

}
