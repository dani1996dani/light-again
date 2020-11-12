using Assets.Scripts;
using UnityEngine;

public class PlayerDamageSubsriber : MonoBehaviour
{
    private Health playerHealth;

    void Awake()
    {
        playerHealth = GameObject.FindGameObjectWithTag(Settings.TagPlayer).GetComponent<Health>();
    }

    private void OnEnable()
    {
        Publisher.publish.PlayerHit += OnPlayerHit;
    }

    private void OnPlayerHit(int damage)
    {
        playerHealth.TakeDamage(damage);
    }

    private void OnDisable()
    {
        Publisher.publish.PlayerHit -= OnPlayerHit;
    }
}
