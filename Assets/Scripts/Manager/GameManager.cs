using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] public Health playerHealth;

    [SerializeField] private TextMeshProUGUI coinText;
    public int coins = 15;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        playerHealth = GetComponent<Health>();
    }

    private void Start()
    {
        UpdateCoinText();
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        UpdateCoinText();
    }
    public void SpendCoins(int amount)
    {
        coins -= amount;
        UpdateCoinText();
    }

    private void UpdateCoinText()
    {
        coinText.text = "= " + coins;
    }
}
