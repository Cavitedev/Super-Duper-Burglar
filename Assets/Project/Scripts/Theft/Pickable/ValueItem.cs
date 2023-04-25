using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pickable))]
public class ValueItem : MonoBehaviour
{

    private Pickable _pickable;
    [Tooltip("100 = €1")]
    public int value = 10000;
    // Start is called before the first frame update
    void Start()
    {
        _pickable = GetComponent<Pickable>();
        _pickable.onPick.AddListener(OnPick);
    }

    private void OnPick()
    {
        PlayerStats.Instance.AddToBounty(value);
    }
}
