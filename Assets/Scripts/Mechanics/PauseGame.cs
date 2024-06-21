using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public Animator[] animators;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Pause();
        }
    }

    void Pause()
    {
        foreach (Animator animator in animators)
        {
            animator.enabled = !animator.enabled;
        }
        Time.timeScale = Time.timeScale == 1 ? 0 : 1;
    }
}
