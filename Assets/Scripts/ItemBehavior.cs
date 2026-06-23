using UnityEngine;

public class ItemBehavior : MonoBehaviour
{
    // Referensi ke Game Manager
    public GameBehavior GameManager;

    void Start()
    {
        // Cari Game Manager otomatis
        GameManager = GameObject.Find("Game Manager").GetComponent<GameBehavior>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Destroy(this.transform.gameObject);
            Debug.Log("Item collected!");

            // Update counter di Game Manager
            if (GameManager != null)
            {
                GameManager.Items += 1;
            }
        }
    }
}