using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameBehavior : MonoBehaviour
{
    // Variables untuk menyimpan data game
    private int _itemsCollected = 0;
    private int _playerHP = 10;

    public int MaxItems;

    // Referensi UI
    public TMP_Text HealthText;
    public TMP_Text ItemText;
    public TMP_Text ProgressText;

    public Button WinButton;

    // ===== Tambahan 4.6 =====
    public Button LossButton;

    void Start()
    {
        // Initialize UI display
        ItemText.text = "Items: " + _itemsCollected;
        HealthText.text = "Health: " + _playerHP;

        // Sembunyikan Win Button di awal
        if (WinButton != null)
            WinButton.gameObject.SetActive(false);

        // ===== Tambahan 4.6 =====
        // Sembunyikan Loss Button di awal
        if (LossButton != null)
            LossButton.gameObject.SetActive(false);
    }

    // ==========================
    // ITEM SYSTEM
    // ==========================
    public int Items
    {
        get { return _itemsCollected; }
        set
        {
            _itemsCollected = value;
            ItemText.text = "Items: " + _itemsCollected;

            // Jika semua item terkumpul
            if (_itemsCollected >= MaxItems)
            {
                UpdateScene("You've found all the items!");

                if (WinButton != null)
                    WinButton.gameObject.SetActive(true);

                Time.timeScale = 0f;
            }
            else
            {
                int remaining = MaxItems - _itemsCollected;

                ProgressText.text =
                    "Item found, only " + remaining + " more to go!";
            }
        }
    }

    // ==========================
    // HP SYSTEM
    // ==========================
    public int HP
    {
        get { return _playerHP; }

        set
        {
            _playerHP = value;

            HealthText.text = "Health: " + _playerHP;

            Debug.LogFormat("Lives: {0}", _playerHP);

            // ===== Tambahan 4.6 =====
            // Cek Game Over
            if (_playerHP <= 0)
            {
                UpdateScene("You want another life with that?");

                if (LossButton != null)
                    LossButton.gameObject.SetActive(true);

                Time.timeScale = 0f;
            }
            else
            {
                ProgressText.text = "Ouch... that got hurt.";
            }
        }
    }

    // ==========================
    // UPDATE STATUS TEXT
    // ==========================
    public void UpdateScene(string updatedText)
    {
        ProgressText.text = updatedText;
    }

    // ==========================
    // RESTART GAME
    // ==========================
    public void RestartScene()
    {
        SceneManager.LoadScene("SampleScene3DOnly");
        Time.timeScale = 1f;
    }
}