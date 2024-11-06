using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageUI : MonoBehaviour
{

    private TextMeshProUGUI damageText;

    private void OnEnable()
    {
        damageText = GetComponent<TextMeshProUGUI>();
    }


    public void DamageText(int damageAmount)
    {
        damageText.text = damageAmount.ToString();
    }

    void Destroy()
    {
        Destroy(gameObject);
    }

}
