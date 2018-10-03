using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    private ActorController[] players;

    private float gameTime = 25F;

    public float CurrentGameTime { get; private set; }

    // Use this for initialization
    private IEnumerator Start()
    {
        CurrentGameTime = gameTime;

        // Sets the first random tagged player
        players = FindObjectsOfType<ActorController>();

        yield return new WaitForSeconds(0.5F);

        players[Random.Range(0, players.Length)].onActorTagged(true);
    }

    private void Update()
    {
        CurrentGameTime -= Time.deltaTime;

        if (CurrentGameTime <= 0F)
        {
            //TODO: Send GameOver event.
        }
    }

    private static GameController instance;

    public static GameController Instance
    {
        get
        {
            return instance;
        }
    }

    [SerializeField]
    private Rigidbody AiPrefab;

    [SerializeField]
    private int size;

    private List<Rigidbody> Ais;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            PrepareAis();
        }
        else
            Destroy(gameObject);
    }

    private void PrepareAis()
    {
        Ais = new List<Rigidbody>();
        for (int i = 0; i < size; i++)
            AddAi();
    }

    public Rigidbody GetAi()
    {
        if (Ais.Count == 0)
            AddAi();
        return AllocateAi();
    }

    public void ReleaseAi(Rigidbody Ai)
    {
        Ai.gameObject.SetActive(false);
        Ais.Add(Ai);
    }

    private void AddAi()
    {
        Rigidbody instance = Instantiate(AiPrefab);
        instance.gameObject.SetActive(true);
        Ais.Add(instance);
    }

    private Rigidbody AllocateAi()
    {
        Rigidbody Ai = Ais[Ais.Count - 1];
        Ais.RemoveAt(Ais.Count - 1);
        Ai.gameObject.SetActive(true);
        return Ai;
    }
}
