using UnityEngine;

public class AIMoveTest : MonoBehaviour
{
    public static AIMoveTest Instance { get; private set; }

    public delegate void OnAIMoveIssued();
    public OnAIMoveIssued onAIMoveIssued;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (onAIMoveIssued != null)
            {
                onAIMoveIssued(); 
            }
        }
    }
}