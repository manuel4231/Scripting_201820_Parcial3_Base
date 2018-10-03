using UnityEngine;
using UnityEngine.AI;

public class AIController : ActorController
{
    [SerializeField]
    private float moveRadius = 50F;

    [SerializeField]
    private Root btRootNode;
    float velocidad = 2;
    Transform mTrasform, jugador;
    public NavMeshAgent navAgent;
    public bool patrullando = true, enRango = false;


    protected override void Start()
    {
        mTrasform = GetComponent<Transform>();
        jugador = GameObject.Find("AI").GetComponent<Transform>();
        base.Start();

        if (btRootNode != null)
        {
            btRootNode.SetControlledAI(this);
        }

        AIMoveTest.Instance.onAIMoveIssued += MoveAI;
    }

    void Update()
    {
        MoveAI();
    }

    public void MoveAI()
    {
        Vector3 rangoJugador = mTrasform.position - jugador.position;
        if (rangoJugador.sqrMagnitude <= 500)
        {
            //navAgent.ResetPath();
            enRango = true;
            patrullando = false;
            if (enRango == true)
            {
                mTrasform.LookAt(jugador.position);
                mTrasform.position += mTrasform.forward * Time.deltaTime * velocidad;
            }
        }
        if (rangoJugador.sqrMagnitude >= 500)
        {
            enRango = false;
            patrullando = true;
        }
    }

    protected override void OnDestroy()
    {
        AIMoveTest.Instance.onAIMoveIssued -= MoveAI;
        base.OnDestroy();
    }

    protected override Vector3 GetTargetLocation()
    {
        Vector3 result = transform.position;

        Vector3 randomDirection = Random.insideUnitSphere * moveRadius;
        randomDirection += transform.position;

        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomDirection, out hit, moveRadius, 1))
        {
            result = hit.position;
        }

        return result;
    }


}