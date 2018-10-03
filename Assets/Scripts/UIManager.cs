using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{ 
    [SerializeField]
    private Text timeText;

    GameController gameController;

    // Use this for initialization
    private void Start()
    {
        gameController = GetComponent<GameController>();

        if (timeText == null)
        {
            enabled = false;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        //TODO: Set text from GameController
        timeText.text = gameController.CurrentGameTime.ToString("F2");
    }
}