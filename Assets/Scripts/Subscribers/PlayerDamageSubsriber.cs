using Assets.Scripts;
using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.SceneManagement;

public class PlayerDamageSubsriber : MonoBehaviour
{
    private Health playerHealth;
    private Animator playerAnimator;
    private AnimationClip deathAnimationClip;

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        InitLoad();
    }

    private void Start()
    {
        InitLoad();
    }

    void InitLoad()
    {
        GameObject playerGameObject = GameObject.FindGameObjectWithTag(Settings.TagPlayer);
        if (playerGameObject != null)
        {
            playerHealth = playerGameObject.GetComponent<Health>();
            playerAnimator = playerGameObject.GetComponentInChildren<Animator>();
            deathAnimationClip = playerAnimator.runtimeAnimatorController.animationClips.FirstOrDefault((x) => x.name == "Death");
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        Publisher.publish.PlayerHit += OnPlayerHit;
        Publisher.publish.PlayerDeath += OnPlayerDeath;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        Publisher.publish.PlayerHit -= OnPlayerHit;
        Publisher.publish.PlayerDeath -= OnPlayerDeath;
    }

    private void OnPlayerHit(int damage)
    {
        playerHealth.TakeDamage(damage);
    }

    private void OnPlayerDeath()
    {
        Settings.isGamePaused = true;
        Settings.isGameActive = false;
        StartCoroutine("StartPlayerDeath");
    }

    private IEnumerator StartPlayerDeath()
    {
        playerAnimator.SetBool("isDead", true);
        yield return new WaitForSeconds(deathAnimationClip.length);
        Time.timeScale = 0;
    }
}
