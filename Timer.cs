using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float revealTime = 5f; // Time cards stay face-up
    [SerializeField] private SceneController sceneController;

    private bool _isTimerRunning;

    void Start() {
        
        if (timerText == null) {
            Debug.LogError("Timer Text not assigned!");
            return;
        }     
        if (sceneController == null) {
            Debug.LogError("SceneController not assigned!");
            return;
        }

        // Clear existing cards and set up new ones
        sceneController.ClearCards(); // Add this method to SceneController
        
        // Reveal cards but block flipping until timer ends
        sceneController.RevealAll();
        sceneController.EnableRevealing(false); // Disable flips during reveal
        
        _isTimerRunning = true;
    }

    void Update() {
        if (!_isTimerRunning) return;

        revealTime -= Time.deltaTime;
        UpdateTimerDisplay();

        if (revealTime <= 0) {
            _isTimerRunning = false;
            sceneController.HideAll(); // Flip cards face-down
            sceneController.EnableRevealing(true); // Allow flipping
        }
    }

    private void UpdateTimerDisplay() {
        int seconds = Mathf.CeilToInt(revealTime);
        timerText.text = $"00:{seconds:00}";
    }
}
