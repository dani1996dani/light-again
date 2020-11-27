using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using Assets.Scripts.Helpers;
using System.Linq;

public class OwlmanHPProgress : MonoBehaviour
{
    GameObject owlmanHPBarGameObject;
    Health owlmanHealth;
    float maxXScale;

    private void Start()
    {
        owlmanHealth = gameObject.GetComponent<Health>();
        List<GameObject> childGameObjects = gameObject.GetAllChildren();
        owlmanHPBarGameObject = childGameObjects.FirstOrDefault((x) => x.tag == Settings.TagRegularOwlmanHPBar);
        maxXScale = owlmanHPBarGameObject.transform.localScale.x;
    }

    private void Update()
    {
        float currentHealth = owlmanHealth.GetCurrentHealth();
        float maxHealth = owlmanHealth.GetMaxHealth();
        float newXScale = Mathf.Clamp(currentHealth * maxXScale / maxHealth, 0, maxXScale);
        owlmanHPBarGameObject.transform.localScale = new Vector3(newXScale, 1, 1);
    }
}
