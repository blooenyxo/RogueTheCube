using UnityEngine;
using UnityEngine.UI;

public class NameText : MonoBehaviour
{
    private Text nameText;

    void Start()
    {
        nameText = GetComponent<Text>();
        nameText.text = PlayerPrefs.GetString("PlayerName", "randomHero");
    }
}