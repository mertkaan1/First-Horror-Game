using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyVision : MonoBehaviour
{
    [Header("Görüş Ayarları")]
    public float gorusMesafesi = 10f;
    public float gorusAcisi = 120f;
    public LayerMask playerLayer;
    public LayerMask engelLayer;

    [Header("Devriye Ayarları")]
    public float randomDevriyeYaricapi = 8f;
    public float devriyeHizi = 2f;
    public float kovalamacaHizi = 3.5f;
    public float durmaSuresi = 2f;

    private Transform player;
    private NavMeshAgent agent;
    private Animator anim;
    private Vector3 hedefPozisyon;

    private bool goruyorMu = false;
    private bool bekliyor = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();

        agent.speed = devriyeHizi;
        hedefPozisyon = GetRandomNavmeshPoint();
        agent.SetDestination(hedefPozisyon);
    }

    void Update()
    {
        if (player == null || agent == null || anim == null)
            return;
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        float aci = Vector3.Angle(transform.forward, directionToPlayer);

        goruyorMu = false;

        if (distanceToPlayer <= gorusMesafesi && aci <= gorusAcisi / 2f)
        {
            if (!Physics.Raycast(transform.position + Vector3.up * 1.5f, directionToPlayer, distanceToPlayer, engelLayer))
            {
                goruyorMu = true;
            }
        }

        if (goruyorMu)
        {
            agent.isStopped = false;
            agent.speed = kovalamacaHizi;
            agent.SetDestination(player.position);
            // 🔥 Animasyon
            anim.SetBool("IsWalking", true);
            anim.SetBool("IsSneaky", true);
        }
        else
        {
            DevriyedeGez();
        }
        // Düşman hiç hareket etmiyorsa idle'a geçsin
        bool yuru = agent.velocity.magnitude > 0.1f && !agent.isStopped;
        anim.SetBool("IsWalking", yuru);

        // Animasyonu yürüyüp yürümemeye göre güncelle
        anim.SetBool("IsWalking", agent.velocity.magnitude > 0.1f && !agent.isStopped);
    }

    void DevriyedeGez()
    {
        if (bekliyor) return;

        agent.speed = devriyeHizi;

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            StartCoroutine(BekleVeYeniHedefSec());
        }

        agent.isStopped = false;

        // 🧍‍♂️ Devriyede normal yürüyor
        anim.SetBool("IsSneaky", false);
    }
    IEnumerator BekleVeYeniHedefSec()
    {
        bekliyor = true;
        agent.isStopped = true;

        yield return new WaitForSeconds(durmaSuresi);

        agent.isStopped = false;
        hedefPozisyon = GetRandomNavmeshPoint();
        agent.SetDestination(hedefPozisyon);

        bekliyor = false;
    }

    Vector3 GetRandomNavmeshPoint()
    {
        for (int i = 0; i < 10; i++)
        {
            Vector3 rastgeleYon = Random.insideUnitSphere * randomDevriyeYaricapi;
            rastgeleYon += transform.position;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(rastgeleYon, out hit, 2f, NavMesh.AllAreas))
            {
                return hit.position;
            }
        }

        return transform.position;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, gorusMesafesi);

        Vector3 solSinir = Quaternion.Euler(0, -gorusAcisi / 2f, 0) * transform.forward;
        Vector3 sagSinir = Quaternion.Euler(0, gorusAcisi / 2f, 0) * transform.forward;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + solSinir * gorusMesafesi);
        Gizmos.DrawLine(transform.position, transform.position + sagSinir * gorusMesafesi);
    }
}