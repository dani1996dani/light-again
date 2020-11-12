using Assets.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        Publisher.publish.PlayerDeath += OnPlayerDeath;
    }

    private void OnPlayerHit(int damage)
    {
        playerHealth.TakeDamage(damage);
    }

    private void OnPlayerDeath()
    {
        Time.timeScale = 0;
        Settings.isGamePaused = true;
    }

    private void OnDisable()
    {
        Publisher.publish.PlayerHit -= OnPlayerHit;
        Publisher.publish.PlayerDeath -= OnPlayerDeath;
    }
}
