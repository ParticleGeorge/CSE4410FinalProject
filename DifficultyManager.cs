using UnityEngine;

public enum Difficulty { Easy, Normal, Hard }

public class DifficultyManager : MonoBehaviour
{
    public static Difficulty SelectedDifficulty { get; private set; }

    public void SetEasy() {
        SelectedDifficulty = Difficulty.Easy;
        LoadGame();
    }

    public void SetNormal() {
        SelectedDifficulty = Difficulty.Normal;
        LoadGame();
    }

    public void SetHard() {
        SelectedDifficulty = Difficulty.Hard;
        LoadGame();
    }

    private void LoadGame() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }
}
