using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using DG.Tweening;

public class EnemySystem : MonoBehaviour
{

    [Header("Attributes")]
    public GameObject FloatingTextProjectile;
    public float enemyHealth;
    public float enemyDamage;
    public float earnedMoney;
    public float attackDelay;
    private float waitForAttack;
    public float enemyLevel;
    [Header("AI")]
    public float lookRadius;
    Transform target;
    NavMeshAgent agent;


    private void OnEnable()
    {
        EventManager.OnEnemyTakeDamage += enemyTakeDamage;
    }

    private void OnDisable()
    {
        EventManager.OnEnemyTakeDamage -= enemyTakeDamage;
    }

    void Start()
    {
        target = PlayerManager.Instance.gameObject.transform;
        agent = GetComponent<NavMeshAgent>();
        waitForAttack = attackDelay;
    }


    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if(distance <= lookRadius)
        {
            agent.SetDestination(target.position);

            if(distance <= agent.stoppingDistance)
            {
                enemyAttack();
                FaceTarget();            
            }
        }
    }
    
    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    public void enemyTakeDamage(float damage,Vector3 bulletPositon)
    {
        if (enemyHealth <= 0) Destroy(this.gameObject); // Will update with enemyDie() func.    

        enemyHealth -= damage;     
        PopUp(damage,bulletPositon);
    }

    void PopUp(float Damage , Vector3 Position)
    {
        var myGameObject = Instantiate(FloatingTextProjectile, Position , Quaternion.identity) as GameObject;
        myGameObject.GetComponent<TextMesh>().text = Damage.ToString();
        myGameObject.gameObject.transform.DOScale(0, 0.5F);
        myGameObject.gameObject.transform.DOMove(new Vector3(transform.position.x + Random.Range(-0.5F, 1), transform.position.y + Random.Range(-0.5F, 1), transform.position.z + Random.Range(-0.5F, 0.5F)), 1);
    }
    public void enemyAttack()
    {
        if (waitForAttack <= 0)
        {
            EventManager._PlayerTakeDamage(enemyDamage);
            waitForAttack = attackDelay;
            damageScreen();
        }
        if (waitForAttack > 0) waitForAttack -= Time.deltaTime;
    }
    void damageScreen()
    {
        var color = PlayerManager.Instance.getHitScreen.GetComponent<Image>().color;
        color.a = 0.9f;
        PlayerManager.Instance.getHitScreen.GetComponent<Image>().color = color;
    }

}
