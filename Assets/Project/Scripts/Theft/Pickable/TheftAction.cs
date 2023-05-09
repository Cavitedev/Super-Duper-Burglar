using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TheftAction : MonoBehaviour
{
    [SerializeField] ParticleSystem moneyParticleSystem;
    [SerializeField] TextMeshPro moneyEarnedText;

    private bool onPlay = false;
    private Vector3 moneyTextStandardPos;

    private void Start()
    {
        moneyTextStandardPos = moneyEarnedText.transform.position;
        moneyEarnedText.gameObject.SetActive(false);

    }
    public void Update()
    {
        if (onPlay)
        {
            moneyEarnedText.transform.Translate(0f, 2f * Time.deltaTime,0f);
        }
    }
    public void TheftAnItem(float moneyEarned)
    {
        StartCoroutine(PlayParticles(moneyEarned));
    }
    IEnumerator PlayParticles(float moneyEarned)
    {
        onPlay = true;
        moneyEarnedText.text = "+" + moneyEarned + "$";
        moneyEarnedText.gameObject.SetActive(true);
        moneyParticleSystem.Play();
        yield return new WaitForSeconds(1f);
        moneyEarnedText.gameObject.SetActive(false);
        moneyParticleSystem.Stop();
        moneyEarnedText.transform.position = moneyTextStandardPos;
        onPlay = false;
    }
}
