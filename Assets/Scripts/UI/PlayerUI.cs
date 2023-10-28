using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    private void Start()
    {
        GameSettings.Load();
    }

    private void OnGUI()
    {
        PlayerController pl = PlayerController.instance;
        
        // Remove all additional UI if player in dialog
        if(pl.IsInDialog())
            return;
        
        // Draw health bar in top-left side
        
        float barWidth = 100f;
        float barHeight = 20f;
        float xPos = 10f;
        float yPos = 10f;
        
        float healthBarFill = Mathf.Clamp01(pl.GetHealth() / pl.GetMaxHealth());

        GUI.color = new Color(0.5f, 0.3f, 0.3f);
        GUI.DrawTexture(new Rect(xPos, yPos, barWidth, barHeight), Texture2D.whiteTexture, ScaleMode.StretchToFill, false);

        GUI.color = new Color(1f, 0.5f, 0.5f);
        GUI.DrawTexture(new Rect(xPos, yPos, barWidth * healthBarFill, barHeight), Texture2D.whiteTexture, ScaleMode.StretchToFill, false);
    }
}