using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class HitZone_Wizard : MonoBehaviour
{
    private WizardSkill wizardSkill;

    private void Start()
    {
        wizardSkill = GetComponentInParent<WizardSkill>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth enemy = collision.GetComponent<EnemyHealth>();
        if (enemy!=null)
        {
            wizardSkill.EnemyInRange.Add(collision.gameObject);
        }
    }
}
