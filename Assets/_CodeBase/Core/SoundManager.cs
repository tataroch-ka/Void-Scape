using UnityEngine;

namespace _CodeBase.LevelModule
{
  public class SoundManager : MonoBehaviour
  {
    [SerializeField] private AudioSource _audioSource;
    
    [SerializeField] private AudioClip _flipSound;
    [SerializeField] private AudioClip _matchSound;
    [SerializeField] private AudioClip _missMatchSound;
    [SerializeField] private AudioClip _gameOverSound;
    
    public void PlayFlip()
    {
      _audioSource.clip = _flipSound;
      _audioSource.Play();
    }

    public void PlayMatch()
    {
      _audioSource.clip = _matchSound;
      _audioSource.Play();
    }

    public void PlayMissMatch()
    {
      _audioSource.clip = _missMatchSound;
      _audioSource.Play();
    }

    public void PlayGameOver()
    {
      _audioSource.clip = _gameOverSound;
      _audioSource.Play();
    }
  }
}