using DG.Tweening;
using UnityEditor.Analytics;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float speed;
    public NavMeshAgent navMeshAgent;
    public Transform ZPrefab;
    public void StartEnemy(Player player)
    {
        transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
    }
}
