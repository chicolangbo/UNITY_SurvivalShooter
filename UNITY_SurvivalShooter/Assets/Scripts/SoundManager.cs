using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance
    {
        get
        {
            if (m_instacne == null)
            {
                m_instacne = FindObjectOfType<SoundManager>();
            }

            return m_instacne;
        }
    }

    private static SoundManager m_instacne;

    public List<AudioSource> enemyAudioSource;
    public AudioSource playerAudioSource;
    public AudioSource gunShotAudioSource;

    public Slider effectAudioSlider;

    public void SetEnemyVolume()
    {
        for (int i = 0; i < enemyAudioSource.Count; i++)
        {
            enemyAudioSource[i].volume = effectAudioSlider.value;
        }
    }
}
