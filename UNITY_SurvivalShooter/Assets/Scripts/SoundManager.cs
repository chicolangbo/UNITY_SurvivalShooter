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

    public Slider effectAudioSlider;
    public Slider bgAudioSlider;

    private float prevBgVolume;
    private float prevEffectVolume;

    public List<AudioSource> enemyAudioSource;
    public AudioSource bgAudioSource;
    public AudioSource playerAudioSource;
    public AudioSource gunShotAudioSource;

    public void SetEnemyVolume()
    {
        for (int i = 0; i < enemyAudioSource.Count; i++)
        {
            enemyAudioSource[i].volume = effectAudioSlider.value;
        }
    }

    public void OnMuteClick(bool isOn)
    {
        if (isOn)
        {
            prevBgVolume = bgAudioSlider.value;
            prevEffectVolume = effectAudioSlider.value;

            bgAudioSlider.value = 0f;
            effectAudioSlider.value = 0f;
        }
        else
        {
            bgAudioSlider.value = prevBgVolume;
            effectAudioSlider.value = prevEffectVolume;
        }
    }
}
