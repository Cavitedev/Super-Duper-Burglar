using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{


    [SerializeField] public PlayerStats amount;
    public TextMeshProUGUI score;

    // Start is called before the first frame update
    private void Update()
    {
        score.text = (PlayerStats.Instance.BountyAmount / 100f).ToString("f0") + "€";
    }

}
