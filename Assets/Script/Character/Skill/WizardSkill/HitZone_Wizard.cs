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
        // EnemyHealth enemy = ;
        if (collision.GetComponent<EnemyHealth>() != null || collision.GetComponent<RedDevil_Health>() != null
            || collision.GetComponent<StoneGolemHealth>() != null || collision.GetComponent<OrcWarlock>() != null
            || collision.GetComponent<WorldBoss>() != null)
        {
            wizardSkill.EnemyInRange.Add(collision.gameObject);
        }

    }
}
