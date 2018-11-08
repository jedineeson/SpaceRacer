using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeTunnel : MonoBehaviour
{
    [SerializeField]
    private Transform m_Transform;
    private Vector3 m_SpawnPos;

    private int m_TunnelTypeQty = 4;
    private Vector3 m_FollowTransition = new Vector3(0f, 0f, 6.5f);
    private Vector3 m_ObstacleSpawn = new Vector3(0f, 0f, 5f);

    private GameObject m_Tunnel1;
    private GameObject m_Tunnel2;
    private GameObject m_Obstacle;
    private GameObject m_Transition;
    private GameObject m_BonusTrigger;

    private List<GameObject> m_TunnelElements = new List<GameObject>();
    private List<GameObject> m_ObstacleElements = new List<GameObject>();
    private List<GameObject> m_BonusTriggers = new List<GameObject>();
    private List<EPoolType> m_ElementPoolType = new List<EPoolType>();
    private List<EPoolType> m_ObstaclePoolType = new List<EPoolType>();

    private int m_LastTunnel = 1;
    private int m_NextTunnel = 1;

    private void Start()
    {
        GameManager.Instance.TunnelGenerator = this;
        m_SpawnPos = m_Transform.position;
        m_NextTunnel = (Random.Range(0, m_TunnelTypeQty)) + 1;
        for (int i = 0; i<30; i++)
        {
            m_NextTunnel = (Random.Range(0, m_TunnelTypeQty)) + 1;
            if (i != 19)
            {
                SpawnTransition(m_LastTunnel, m_NextTunnel, false);
            }
            else
            {
                SpawnTransition(m_LastTunnel, m_NextTunnel, true);
            }
            SpawnTunnel(m_NextTunnel);
        }
    }

    public void GenerateTunnel()
    {
        for (int i = 0; i < 10; i++)
        {
            PoolManager.Instance.ReturnToPool(EPoolType.Bonus, m_BonusTriggers[0]);
            m_BonusTriggers.RemoveAt(0);
            m_NextTunnel = (Random.Range(0, m_TunnelTypeQty)) + 1;
            if (i != 9)
            {
                SpawnTransition(m_LastTunnel, m_NextTunnel, false);
            }
            else
            {
                SpawnTransition(m_LastTunnel, m_NextTunnel, true);
            }
            SpawnTunnel(m_NextTunnel);
        }
        for (int i = 0; i < 30; i++)
        {
            PoolManager.Instance.ReturnToPool(m_ElementPoolType[0], m_TunnelElements[0]);
            m_ElementPoolType.RemoveAt(0);
            m_TunnelElements.RemoveAt(0);
        }
        for (int i = 0; i < 40; i++)
        {
            PoolManager.Instance.ReturnToPool(m_ObstaclePoolType[0], m_ObstacleElements[0]);
            m_ObstaclePoolType.RemoveAt(0);
            m_ObstacleElements.RemoveAt(0);
        }
    }


    private void SpawnTransition(int lastTunnel, int nextTunnel, bool IsLast)
    {
        if(IsLast)
        {
            PoolManager.Instance.GetFromPool(EPoolType.GenerateTunnel, m_SpawnPos);
        }
        if(lastTunnel == 1)
        {
            if (nextTunnel == 1)
            {
                //34|34TO34|34
                m_BonusTrigger = PoolManager.Instance.GetFromPool(EPoolType.Bonus, m_SpawnPos);
                m_BonusTriggers.Add(m_BonusTrigger);
                m_Transition = PoolManager.Instance.GetFromPool(EPoolType.Tr3434to3434, m_SpawnPos);
                m_TunnelElements.Add(m_Transition);
                m_ElementPoolType.Add(EPoolType.Tr3434to3434);
                m_SpawnPos += m_FollowTransition;
            }
            else if (nextTunnel == 2)
            {
                //34|34TO64|34
                m_BonusTrigger = PoolManager.Instance.GetFromPool(EPoolType.Bonus, m_SpawnPos);
                m_BonusTriggers.Add(m_BonusTrigger);
                m_Transition = PoolManager.Instance.GetFromPool(EPoolType.Tr3434to6434, m_SpawnPos);
                m_TunnelElements.Add(m_Transition);
                m_ElementPoolType.Add(EPoolType.Tr3434to6434);
                m_SpawnPos += m_FollowTransition;
            }
            else if (nextTunnel == 3)
            {
                //34|34TO64|64
                m_BonusTrigger = PoolManager.Instance.GetFromPool(EPoolType.Bonus, m_SpawnPos);
                m_BonusTriggers.Add(m_BonusTrigger);
                m_Transition = PoolManager.Instance.GetFromPool(EPoolType.Tr3434to6464, m_SpawnPos);
                m_TunnelElements.Add(m_Transition);
                m_ElementPoolType.Add(EPoolType.Tr3434to6464);
                m_SpawnPos += m_FollowTransition;
            }
            else if (nextTunnel == 4)
            {
                m_BonusTrigger = PoolManager.Instance.GetFromPool(EPoolType.Bonus, m_SpawnPos);
                m_BonusTriggers.Add(m_BonusTrigger);
                m_Transition = PoolManager.Instance.GetFromPool(EPoolType.Tr3434to9494, m_SpawnPos);
                m_TunnelElements.Add(m_Transition);
                m_ElementPoolType.Add(EPoolType.Tr3434to9494);
                m_SpawnPos += m_FollowTransition;
            }
        }
        else if (lastTunnel == 2)
        {
            if (nextTunnel == 1)
            {
                //64|34TO34|34
                m_BonusTrigger = PoolManager.Instance.GetFromPool(EPoolType.Bonus, m_SpawnPos);
                m_BonusTriggers.Add(m_BonusTrigger);
                m_Transition = PoolManager.Instance.GetFromPool(EPoolType.Tr6434to3434, m_SpawnPos);
                m_TunnelElements.Add(m_Transition);
                m_ElementPoolType.Add(EPoolType.Tr6434to3434);
                m_SpawnPos += m_FollowTransition;
            }
            else if (nextTunnel == 2)
            {
                //64|34TO64|34
                m_BonusTrigger = PoolManager.Instance.GetFromPool(EPoolType.Bonus, m_SpawnPos);
                m_BonusTriggers.Add(m_BonusTrigger);
                m_Transition = PoolManager.Instance.GetFromPool(EPoolType.Tr6434to6434, m_SpawnPos);
                m_TunnelElements.Add(m_Transition);
                m_ElementPoolType.Add(EPoolType.Tr6464to6434);
                m_SpawnPos += m_FollowTransition;
            }
            else if (nextTunnel == 3)
            {
                //64|34TO64|64
                m_BonusTrigger = PoolManager.Instance.GetFromPool(EPoolType.Bonus, m_SpawnPos);
                m_BonusTriggers.Add(m_BonusTrigger);
                m_Transition = PoolManager.Instance.GetFromPool(EPoolType.Tr6434to6464, m_SpawnPos);
                m_TunnelElements.Add(m_Transition);
                m_ElementPoolType.Add(EPoolType.Tr6434to6464);
                m_SpawnPos += m_FollowTransition;
            }
            else if (nextTunnel == 4)
            {
                //64|34TO94|94
                m_BonusTrigger = PoolManager.Instance.GetFromPool(EPoolType.Bonus, m_SpawnPos);
                m_BonusTriggers.Add(m_BonusTrigger);
                m_Transition = PoolManager.Instance.GetFromPool(EPoolType.Tr6434to9494, m_SpawnPos);
                m_TunnelElements.Add(m_Transition);
                m_ElementPoolType.Add(EPoolType.Tr6434to9494);
                m_SpawnPos += m_FollowTransition;
            }
        }
        else if (lastTunnel == 3)
        {
            if (nextTunnel == 1)
            {
                //64|64TO34|34
                m_BonusTrigger = PoolManager.Instance.GetFromPool(EPoolType.Bonus, m_SpawnPos);
                m_BonusTriggers.Add(m_BonusTrigger);
                m_Transition = PoolManager.Instance.GetFromPool(EPoolType.Tr6464to3434, m_SpawnPos);
                m_TunnelElements.Add(m_Transition);
                m_ElementPoolType.Add(EPoolType.Tr6464to3434);
                m_SpawnPos += m_FollowTransition;
            }
            else if (nextTunnel == 2)
            {
                //64|64TO64|34
                m_BonusTrigger = PoolManager.Instance.GetFromPool(EPoolType.Bonus, m_SpawnPos);
                m_BonusTriggers.Add(m_BonusTrigger);
                m_Transition = PoolManager.Instance.GetFromPool(EPoolType.Tr6464to6434, m_SpawnPos);
                m_TunnelElements.Add(m_Transition);
                m_ElementPoolType.Add(EPoolType.Tr6464to6434);
                m_SpawnPos += m_FollowTransition;
            }
            else if (nextTunnel == 3)
            {
                //64|64TO64|64
                m_BonusTrigger = PoolManager.Instance.GetFromPool(EPoolType.Bonus, m_SpawnPos);
                m_BonusTriggers.Add(m_BonusTrigger);
                m_Transition = PoolManager.Instance.GetFromPool(EPoolType.Tr6464to6464, m_SpawnPos);
                m_TunnelElements.Add(m_Transition);
                m_ElementPoolType.Add(EPoolType.Tr6464to6464);
                m_SpawnPos += m_FollowTransition;
            }
            else if (nextTunnel == 4)
            {
                //64|64TO94|94
                m_BonusTrigger = PoolManager.Instance.GetFromPool(EPoolType.Bonus, m_SpawnPos);
                m_BonusTriggers.Add(m_BonusTrigger);
                m_Transition = PoolManager.Instance.GetFromPool(EPoolType.Tr6464to9494, m_SpawnPos);
                m_TunnelElements.Add(m_Transition);
                m_ElementPoolType.Add(EPoolType.Tr6464to9494);
                m_SpawnPos += m_FollowTransition;
            }
        }
        else if (lastTunnel == 4)
        {
            if (nextTunnel == 1)
            {
                //94|94TO34|34
                m_BonusTrigger = PoolManager.Instance.GetFromPool(EPoolType.Bonus, m_SpawnPos);
                m_BonusTriggers.Add(m_BonusTrigger);
                m_Transition = PoolManager.Instance.GetFromPool(EPoolType.Tr9494to3434, m_SpawnPos);
                m_TunnelElements.Add(m_Transition);
                m_ElementPoolType.Add(EPoolType.Tr9494to3434);
                m_SpawnPos += m_FollowTransition;
            }
            else if (nextTunnel == 2)
            {
                //94|94TO64|34
                m_BonusTrigger = PoolManager.Instance.GetFromPool(EPoolType.Bonus, m_SpawnPos);
                m_BonusTriggers.Add(m_BonusTrigger);
                m_Transition = PoolManager.Instance.GetFromPool(EPoolType.Tr9494to6434, m_SpawnPos);
                m_TunnelElements.Add(m_Transition);
                m_ElementPoolType.Add(EPoolType.Tr9494to6434);
                m_SpawnPos += m_FollowTransition;
            }
            else if (nextTunnel == 3)
            {
                //94|94TO64|64
                m_BonusTrigger = PoolManager.Instance.GetFromPool(EPoolType.Bonus, m_SpawnPos);
                m_BonusTriggers.Add(m_BonusTrigger);
                m_Transition = PoolManager.Instance.GetFromPool(EPoolType.Tr9494to6464, m_SpawnPos);
                m_TunnelElements.Add(m_Transition);
                m_ElementPoolType.Add(EPoolType.Tr9494to6464);
                m_SpawnPos += m_FollowTransition;
            }
            else if (lastTunnel == 4)
            {
                //94|94TO94|94
                m_BonusTrigger = PoolManager.Instance.GetFromPool(EPoolType.Bonus, m_SpawnPos);
                m_BonusTriggers.Add(m_BonusTrigger);
                m_Transition = PoolManager.Instance.GetFromPool(EPoolType.Tr9494to9494, m_SpawnPos);
                m_TunnelElements.Add(m_Transition);
                m_ElementPoolType.Add(EPoolType.Tr9494to9494);
                m_SpawnPos += m_FollowTransition;
            }
        }
    }

    private void SpawnTunnel(int nextTunnel)
    {
        switch (nextTunnel)
        {
            case 1:
                m_Tunnel1 = PoolManager.Instance.GetFromPool(EPoolType.T3434, m_SpawnPos);
                m_SpawnPos += m_ObstacleSpawn;
                m_Obstacle = PoolManager.Instance.GetFromPool(EPoolType.obstaclesSpawnPos, m_SpawnPos);
                m_SpawnPos += m_ObstacleSpawn;
                m_Tunnel2 = PoolManager.Instance.GetFromPool(EPoolType.T3434, m_SpawnPos);
                m_SpawnPos += m_FollowTransition;
                m_TunnelElements.Add(m_Tunnel1);
                m_TunnelElements.Add(m_Tunnel2);
                m_ElementPoolType.Add(EPoolType.T3434);
                m_ElementPoolType.Add(EPoolType.T3434);
                m_ObstacleElements.Add(m_Obstacle);
                m_ObstaclePoolType.Add(EPoolType.obstaclesSpawnPos);
                SpawnObstacles(m_Obstacle.transform, nextTunnel);
                break;
            case 2:
                m_Tunnel1 = PoolManager.Instance.GetFromPool(EPoolType.T6434, m_SpawnPos);
                m_SpawnPos += m_ObstacleSpawn;
                m_Obstacle = PoolManager.Instance.GetFromPool(EPoolType.obstaclesSpawnPos, m_SpawnPos);
                m_SpawnPos += m_ObstacleSpawn;
                m_Tunnel2 = PoolManager.Instance.GetFromPool(EPoolType.T6434, m_SpawnPos);
                m_SpawnPos += m_FollowTransition;
                m_TunnelElements.Add(m_Tunnel1);
                m_TunnelElements.Add(m_Tunnel2);
                m_ElementPoolType.Add(EPoolType.T6434);
                m_ElementPoolType.Add(EPoolType.T6434);
                m_ObstacleElements.Add(m_Obstacle);
                m_ObstaclePoolType.Add(EPoolType.obstaclesSpawnPos);
                SpawnObstacles(m_Obstacle.transform, nextTunnel);
                break;
            case 3:
                m_Tunnel1 = PoolManager.Instance.GetFromPool(EPoolType.T6464, m_SpawnPos);
                m_SpawnPos += m_ObstacleSpawn;
                m_Obstacle = PoolManager.Instance.GetFromPool(EPoolType.obstaclesSpawnPos, m_SpawnPos);
                m_SpawnPos += m_ObstacleSpawn;
                m_Tunnel2 = PoolManager.Instance.GetFromPool(EPoolType.T6464, m_SpawnPos);
                m_SpawnPos += m_FollowTransition;
                m_TunnelElements.Add(m_Tunnel1);
                m_TunnelElements.Add(m_Tunnel2);
                m_ElementPoolType.Add(EPoolType.T6464);
                m_ElementPoolType.Add(EPoolType.T6464);
                m_ObstacleElements.Add(m_Obstacle);
                m_ObstaclePoolType.Add(EPoolType.obstaclesSpawnPos);
                SpawnObstacles(m_Obstacle.transform, nextTunnel);
                break;
            case 4:
                m_Tunnel1 = PoolManager.Instance.GetFromPool(EPoolType.T9494, m_SpawnPos);
                m_SpawnPos += m_ObstacleSpawn;
                m_Obstacle = PoolManager.Instance.GetFromPool(EPoolType.obstaclesSpawnPos, m_SpawnPos);             
                m_SpawnPos += m_ObstacleSpawn;
                m_Tunnel2 = PoolManager.Instance.GetFromPool(EPoolType.T9494, m_SpawnPos);
                m_SpawnPos += m_FollowTransition;
                m_TunnelElements.Add(m_Tunnel1);
                m_TunnelElements.Add(m_Tunnel2);
                m_ElementPoolType.Add(EPoolType.T9494);
                m_ElementPoolType.Add(EPoolType.T9494);
                m_ObstacleElements.Add(m_Obstacle);
                m_ObstaclePoolType.Add(EPoolType.obstaclesSpawnPos);
                SpawnObstacles(m_Obstacle.transform, nextTunnel);
                break;
        }
        m_LastTunnel = nextTunnel;
    }

    private void SpawnObstacles(Transform goParent, int Tunnel)
    {
        for (int i = 0; i < goParent.childCount; i++)
        {
            
            switch (Tunnel)
            {
                
                case 1:
                    switch (Random.Range(1, 5))
                    {
                        case 1:
                            m_Obstacle = PoolManager.Instance.GetFromPool(EPoolType.High3434, goParent.GetChild(i).position);
                            m_ObstacleElements.Add(m_Obstacle);
                            m_ObstaclePoolType.Add(EPoolType.High3434);
                            break;
                        case 2:
                            m_Obstacle = PoolManager.Instance.GetFromPool(EPoolType.Left3434, goParent.GetChild(i).position);
                            m_ObstacleElements.Add(m_Obstacle);
                            m_ObstaclePoolType.Add(EPoolType.Left3434);
                            break;
                        case 3:
                            m_Obstacle = PoolManager.Instance.GetFromPool(EPoolType.Right3434, goParent.GetChild(i).position);
                            m_ObstacleElements.Add(m_Obstacle);
                            m_ObstaclePoolType.Add(EPoolType.Right3434);
                            break;
                        case 4:
                            m_Obstacle = PoolManager.Instance.GetFromPool(EPoolType.Low3434, goParent.GetChild(i).position);
                            m_ObstacleElements.Add(m_Obstacle);
                            m_ObstaclePoolType.Add(EPoolType.Low3434);
                            break;
                    }
                    break;
                case 2:
                    float moveSpeed = Random.Range(3, 8);
                    moveSpeed /= 10f;
                    int headOrTail = Random.Range(0, 2);
                    bool invert;
                    if(headOrTail == 1)
                    {
                        invert = true;
                    }
                    else
                    {
                        invert = false;
                    }
                    m_Obstacle = PoolManager.Instance.GetFromPool(EPoolType.Mid6434, goParent.GetChild(i).position);
                    m_Obstacle.GetComponentInChildren<MoveObject>().SetUpObstacles(moveSpeed, invert);
                    m_ObstacleElements.Add(m_Obstacle);
                    m_ObstaclePoolType.Add(EPoolType.Mid6434);
                    break;
                case 3:
                    switch (Random.Range(1, 5))
                    {
                        case 1:
                            m_Obstacle = PoolManager.Instance.GetFromPool(EPoolType.High6464, goParent.GetChild(i).position);
                            m_ObstacleElements.Add(m_Obstacle);
                            m_ObstaclePoolType.Add(EPoolType.High6464);
                            break;
                        case 2:
                            m_Obstacle = PoolManager.Instance.GetFromPool(EPoolType.Left6464, goParent.GetChild(i).position);
                            m_ObstacleElements.Add(m_Obstacle);
                            m_ObstaclePoolType.Add(EPoolType.Left6464);
                            break;
                        case 3:
                            m_Obstacle = PoolManager.Instance.GetFromPool(EPoolType.Right6464, goParent.GetChild(i).position);
                            m_ObstacleElements.Add(m_Obstacle);
                            m_ObstaclePoolType.Add(EPoolType.Right6464);
                            break;
                        case 4:
                            m_Obstacle = PoolManager.Instance.GetFromPool(EPoolType.Low6464, goParent.GetChild(i).position);
                            m_ObstacleElements.Add(m_Obstacle);
                            m_ObstaclePoolType.Add(EPoolType.Low6464);
                            break;
                    }
                    break;
                case 4:
                    int rotDir = Random.Range(0, 2);
                    int rotSpeed = Random.Range(30, 60);
                    m_Obstacle = PoolManager.Instance.GetFromPool(EPoolType.Propeller, goParent.GetChild(i).position);
                    m_ObstacleElements.Add(m_Obstacle);
                    m_ObstaclePoolType.Add(EPoolType.Propeller);
                    m_Obstacle.transform.GetComponentInChildren<Propeller>().setProp(rotSpeed, rotDir);
                    break;
            }
        }
    }

    public void ReturnStuff()
    {
        for(int i = m_TunnelElements.Count - 1; i >= 0; i--)
        {
            PoolManager.Instance.ReturnToPool(m_ElementPoolType[0], m_TunnelElements[0]);
            m_ElementPoolType.RemoveAt(0);
            m_TunnelElements.RemoveAt(0);
        }
        for (int i = m_BonusTriggers.Count - 1; i >= 0; i--)
        {
            PoolManager.Instance.ReturnToPool(EPoolType.Bonus, m_BonusTriggers[0]);
            m_BonusTriggers.RemoveAt(0);
        }
        for (int i = m_ObstacleElements.Count - 1; i >= 0; i--)
        {
            PoolManager.Instance.ReturnToPool(m_ObstaclePoolType[0], m_ObstacleElements[0]);
            m_ObstaclePoolType.RemoveAt(0);
            m_ObstacleElements.RemoveAt(0);
        }
    }
}

