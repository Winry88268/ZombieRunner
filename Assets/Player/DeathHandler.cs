using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    [SerializeField] Canvas gameOverCanvas;

    void Start() 
    {
        this.gameOverCanvas.enabled = false;
    }

    public void ProcessDeath()
    {
        this.gameOverCanvas.enabled = true;
        Time.timeScale = 0;
        Cursor.lockState  = CursorLockMode.None;
        Cursor.visible = true;
    }
}
