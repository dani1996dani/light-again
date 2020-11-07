to use the event publisher you need to add and remove the OnEvent method in each script you use the event.
example:

  private void OnEnable() {
    Publisher.publish.PlayerDeath += OnPlayerDeath;
  }

  private void OnPlayerDeath() {
    Debug.Log("Oh no! He died!");
  }

  private void OnDisable() {
    Publisher.publish.PlayerDeath -= OnPlayerDeath;   
  }
