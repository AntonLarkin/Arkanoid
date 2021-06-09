using UnityEngine;

public class SFxAudioSource : SingletonMonoBehaviour<SFxAudioSource>
{
    #region Variables

    [SerializeField] private AudioClip gameOverClip;
    [SerializeField] private AudioClip loseLifeClip;

    private AudioSource audioSource;

    #endregion


    #region Unity lifecycle

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        GameOverSequence.OnDefeatEndGame += OnGameOver_PlayGameOverClip;
    }

    private void OnDisable()
    {
        GameOverSequence.OnDefeatEndGame -= OnGameOver_PlayGameOverClip;

    }
    #endregion


    #region Public methods

    public void PlaySfx(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }

    public void StopSfx()
    {
        audioSource.Stop();
    }

    public void LoseLifeSfx()
    {
        audioSource.PlayOneShot(loseLifeClip);
    }

    #endregion


    #region Event handlers

    private void OnGameOver_PlayGameOverClip()
    {
        audioSource.PlayOneShot(gameOverClip);
    }

    #endregion
}