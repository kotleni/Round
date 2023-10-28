using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    private enum HeartState
    {
        EMPTY, HALF, FULL    
    }
    
    [SerializeField] private Image firstHeart;
    [SerializeField] private Image secondHeart;
    [SerializeField] private Image thirdHeart;
    [SerializeField] private Image fourHeart;

    [SerializeField] private Sprite emptyHeartSprite;
    [SerializeField] private Sprite halfHeartSprite;
    [SerializeField] private Sprite fullHeartSprite;
    
    private void Start()
    {
        GameSettings.Load();
    }

    private void UpdateHearts(float health, float maxHealth)
    {
        // Ensure health is within valid bounds
        health = Mathf.Clamp(health, 0f, maxHealth);
    
        float healthPercentage = health / maxHealth;
        float magic = 1.0f / 7;

        SetHeartState(firstHeart, CalculateHeartState(healthPercentage, 0, magic*1));
        SetHeartState(secondHeart, CalculateHeartState(healthPercentage, magic*2, magic*3));
        SetHeartState(thirdHeart, CalculateHeartState(healthPercentage, magic*4, magic*5));
        SetHeartState(fourHeart, CalculateHeartState(healthPercentage, magic*6, magic*7));
    }

    private HeartState CalculateHeartState(float healthPercentage, float threshold, float halfThreshold)
    {
        if (healthPercentage < threshold)
            return HeartState.EMPTY;
        if (healthPercentage < halfThreshold)
            return HeartState.HALF;
        
        return HeartState.FULL;
    }

    private void SetHeartState(Image heartImage, HeartState heartState)
    {
        switch (heartState)
        {
            case HeartState.EMPTY:
                heartImage.sprite = emptyHeartSprite;
                break;
            case HeartState.HALF:
                heartImage.sprite = halfHeartSprite;
                break;
            case HeartState.FULL:
                heartImage.sprite = fullHeartSprite;
                break;
        }
    }

    private void OnGUI()
    {
        PlayerController pl = PlayerController.instance;
        
        // Hide all hearts UI if player in dialog
        firstHeart.enabled = !pl.IsInDialog();
        secondHeart.enabled = !pl.IsInDialog();
        thirdHeart.enabled = !pl.IsInDialog();
        fourHeart.enabled = !pl.IsInDialog();
        
        UpdateHearts(pl.GetHealth(), pl.GetMaxHealth());
    }
}