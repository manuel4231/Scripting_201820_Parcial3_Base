using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    private ActorController[] players;

    [SerializeField]
    private float gameTime = 25F;

    [SerializeField]
    int numPlayers;

    public float CurrentGameTime { get; private set; }

    public GameObject iA;

    // Use this for initialization
    private IEnumerator Start()
    {
        numPlayers = Mathf.Clamp(numPlayers, 2, 5);

        PlayerCreate();

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

            foreach (ActorController actorController in players)
            {
                actorController.Die();
      
            }
            CurrentGameTime = 0;
            Winner();
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

/*    [SerializeField]
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
    }*/

    private void PlayerCreate()
    {

        for (int i = 0; i < numPlayers - 1; i++)
        {
            Instantiate(iA, new Vector3(Random.Range(-25, 25), 2, Random.Range(-25, 25)), Quaternion.identity);
        }
    }

    void Winner()
    {
        int temporal = 100;
        foreach (ActorController actorController in players)
        {
            if (actorController.timesTagged < temporal)
            {
                temporal = actorController.timesTagged;
            }

        }

        foreach (ActorController actorController in players)
        {
            if (actorController.timesTagged == temporal)
            {
                actorController.gameObject.GetComponent<Renderer>().material.color = new Color(1, 1, 0);
            }

        }
    }
}