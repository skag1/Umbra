using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitPoint : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;
    [SerializeField] private Light[] lights;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(NextLevel());
        }
    }

    void UnlockNewLevel()
    {
        if(SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("LevelsUnlocked", PlayerPrefs.GetInt("LevelsUnlocked", 1) + 1);
            PlayerPrefs.Save();
        }
    }

    private IEnumerator NextLevel()
    {
        player.HasWon();
        player.enabled = false;
        foreach (Light light in lights)
        {
            light.enabled = false;
        }
        yield return new WaitForSeconds(1f);
        UnlockNewLevel();
        SceneController.Instance.NextLevel();
    }
}
