using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeTunnel : MonoBehaviour
{
    [SerializeField]
    private Transform m_Transform;
    private Vector3 m_SpawnPos;

    private int m_TunnelTypeQty = 5;
    //private string m_LastTunnel;
    private Vector3 m_FollowTunnel = new Vector3(0f, 0f, 10f);
    private Vector3 m_FollowTransition = new Vector3(0f, 0f, 6.5f);
    private Vector3 m_ObstacleSpawn = new Vector3(0f, 0f, 5f);

    private float m_SpawnTimer = 0;
    private float m_SpawnSpeed = 4;

    private GameObject tunnel1;
    private GameObject tunnel2;
    private GameObject transition;

    private List<GameObject> m_TunnelElements = new List<GameObject>();
    private List<EPoolType> m_ElementPoolType = new List<EPoolType>();

    private int m_LastTunnel = 1;
    private int m_NextTunnel = 1;

    private static MakeTunnel m_Instance;
    public static MakeTunnel Instance
    {
        get { return m_Instance; }
    }

    private void Awake()
    {
        if (m_Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            m_Instance = this;
        }
    }
    private void Start()
    {
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
    }

    // Update is called once per frame
    private void Update()
    {
       /* m_SpawnTimer += Time.deltaTime;
        if (m_SpawnTimer >= m_SpawnSpeed)
        {
            m_SpawnTimer = 0;
            m_NextTunnel = (Random.Range(0, m_TunnelTypeQty)) + 1;
            SpawnTransition(m_LastTunnel, m_NextTunnel, false);
            SpawnTunnel(m_NextTunnel);
        }*/
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
                transition = PoolManager.Instance.GetFromPool(EPoolType.Tr3434to3434, m_SpawnPos);
                m_TunnelElements.Add(transition);
                m_ElementPoolType.Add(EPoolType.Tr3434to3434);
                m_SpawnPos += m_FollowTransition;
            }
            else if (nextTunnel == 2)
            {
                Debug.Log("34/34 to 64/34");
                //34|34TO64|34
                transition = PoolManager.Instance.GetFromPool(EPoolType.Tr3434to6434, m_SpawnPos);
                m_TunnelElements.Add(transition);
                m_ElementPoolType.Add(EPoolType.Tr3434to6434);
                m_SpawnPos += m_FollowTransition;
            }
            else if (nextTunnel == 3 || nextTunnel == 4)
            {
                Debug.Log("34/34 to 64/64");
                //34|34TO64|64
                transition = PoolManager.Instance.GetFromPool(EPoolType.Tr3434to6464, m_SpawnPos);
                m_TunnelElements.Add(transition);
                m_ElementPoolType.Add(EPoolType.Tr3434to6464);
                m_SpawnPos += m_FollowTransition;
            }
            else if (nextTunnel == 5)
            {
                //34|34TO94|94
                transition = PoolManager.Instance.GetFromPool(EPoolType.Tr3434to9494, m_SpawnPos);
                m_TunnelElements.Add(transition);
                m_ElementPoolType.Add(EPoolType.Tr3434to9494);
                m_SpawnPos += m_FollowTransition;
            }
        }
        else if (lastTunnel == 2)
        {
            if (nextTunnel == 1)
            {
                //64|34TO34|34
                transition = PoolManager.Instance.GetFromPool(EPoolType.Tr6434to3434, m_SpawnPos);
                m_TunnelElements.Add(transition);
                m_ElementPoolType.Add(EPoolType.Tr6434to3434);
                m_SpawnPos += m_FollowTransition;
            }
            else if (nextTunnel == 2)
            {
                //64|34TO64|34
                transition = PoolManager.Instance.GetFromPool(EPoolType.Tr6434to6434, m_SpawnPos);
                m_TunnelElements.Add(transition);
                m_ElementPoolType.Add(EPoolType.Tr6464to6434);
                m_SpawnPos += m_FollowTransition;
            }
            else if (nextTunnel == 3 || nextTunnel == 4)
            {
                //64|34TO64|64
                transition = PoolManager.Instance.GetFromPool(EPoolType.Tr6434to6464, m_SpawnPos);
                m_TunnelElements.Add(transition);
                m_ElementPoolType.Add(EPoolType.Tr6434to6464);
                m_SpawnPos += m_FollowTransition;
            }
            else if (nextTunnel == 5)
            {
                //64|34TO94|94
                transition = PoolManager.Instance.GetFromPool(EPoolType.Tr6434to9494, m_SpawnPos);
                m_TunnelElements.Add(transition);
                m_ElementPoolType.Add(EPoolType.Tr6434to9494);
                m_SpawnPos += m_FollowTransition;
            }
        }
        else if (lastTunnel == 3 || lastTunnel == 4)
        {
            if (nextTunnel == 1)
            {
                //64|64TO34|34
                transition = PoolManager.Instance.GetFromPool(EPoolType.Tr6464to3434, m_SpawnPos);
                m_TunnelElements.Add(transition);
                m_ElementPoolType.Add(EPoolType.Tr6464to3434);
                m_SpawnPos += m_FollowTransition;
            }
            else if (nextTunnel == 2)
            {
                //64|64TO64|34
                transition = PoolManager.Instance.GetFromPool(EPoolType.Tr6464to6434, m_SpawnPos);
                m_TunnelElements.Add(transition);
                m_ElementPoolType.Add(EPoolType.Tr6464to6434);
                m_SpawnPos += m_FollowTransition;
            }
            else if (nextTunnel == 3 || nextTunnel == 4)
            {
                //64|64TO64|64
                transition = PoolManager.Instance.GetFromPool(EPoolType.Tr6464to6464, m_SpawnPos);
                m_TunnelElements.Add(transition);
                m_ElementPoolType.Add(EPoolType.Tr6464to6464);
                m_SpawnPos += m_FollowTransition;
            }
            else if (nextTunnel == 5)
            {
                //64|64TO94|94
                transition = PoolManager.Instance.GetFromPool(EPoolType.Tr6464to9494, m_SpawnPos);
                m_TunnelElements.Add(transition);
                m_ElementPoolType.Add(EPoolType.Tr6464to9494);
                m_SpawnPos += m_FollowTransition;
            }
        }
        else if (lastTunnel == 5)
        {
            if (nextTunnel == 1)
            {
                //94|94TO34|34
                transition = PoolManager.Instance.GetFromPool(EPoolType.Tr9494to3434, m_SpawnPos);
                m_TunnelElements.Add(transition);
                m_ElementPoolType.Add(EPoolType.Tr9494to3434);
                m_SpawnPos += m_FollowTransition;
            }
            else if (nextTunnel == 2)
            {
                //94|94TO64|34
                transition = PoolManager.Instance.GetFromPool(EPoolType.Tr9494to6434, m_SpawnPos);
                m_TunnelElements.Add(transition);
                m_ElementPoolType.Add(EPoolType.Tr9494to6434);
                m_SpawnPos += m_FollowTransition;
            }
            else if (nextTunnel == 3 || nextTunnel == 4)
            {
                //94|94TO64|64
                transition = PoolManager.Instance.GetFromPool(EPoolType.Tr9494to6464, m_SpawnPos);
                m_TunnelElements.Add(transition);
                m_ElementPoolType.Add(EPoolType.Tr9494to6464);
                m_SpawnPos += m_FollowTransition;
            }
            else if (lastTunnel == 5)
            {
                //94|94TO94|94
                transition = PoolManager.Instance.GetFromPool(EPoolType.Tr9494to9494, m_SpawnPos);
                m_TunnelElements.Add(transition);
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
                tunnel1 = PoolManager.Instance.GetFromPool(EPoolType.T3434, m_SpawnPos);
                m_SpawnPos += m_ObstacleSpawn;
                PoolManager.Instance.GetFromPool(EPoolType.obstaclesSpawnPos, m_SpawnPos);
                m_SpawnPos += m_ObstacleSpawn;
                tunnel2 = PoolManager.Instance.GetFromPool(EPoolType.T3434, m_SpawnPos);
                m_SpawnPos += m_FollowTransition;
                m_TunnelElements.Add(tunnel1);
                m_TunnelElements.Add(tunnel2);
                m_ElementPoolType.Add(EPoolType.T3434);
                m_ElementPoolType.Add(EPoolType.T3434);
                break;
            case 2:
                tunnel1 = PoolManager.Instance.GetFromPool(EPoolType.T6434, m_SpawnPos);
                m_SpawnPos += m_ObstacleSpawn;
                PoolManager.Instance.GetFromPool(EPoolType.obstaclesSpawnPos, m_SpawnPos);
                m_SpawnPos += m_ObstacleSpawn;
                tunnel2 = PoolManager.Instance.GetFromPool(EPoolType.T6434, m_SpawnPos);
                m_SpawnPos += m_FollowTransition;
                m_TunnelElements.Add(tunnel1);
                m_TunnelElements.Add(tunnel2);
                m_ElementPoolType.Add(EPoolType.T6434);
                m_ElementPoolType.Add(EPoolType.T6434);
                break;
            case 3:
                tunnel1 = PoolManager.Instance.GetFromPool(EPoolType.T6464, m_SpawnPos);
                m_SpawnPos += m_ObstacleSpawn;
                PoolManager.Instance.GetFromPool(EPoolType.obstaclesSpawnPos, m_SpawnPos);
                m_SpawnPos += m_ObstacleSpawn;
                tunnel2 = PoolManager.Instance.GetFromPool(EPoolType.T6464, m_SpawnPos);
                m_SpawnPos += m_FollowTransition;
                m_TunnelElements.Add(tunnel1);
                m_TunnelElements.Add(tunnel2);
                m_ElementPoolType.Add(EPoolType.T6464);
                m_ElementPoolType.Add(EPoolType.T6464);
                break;
            case 4:
                tunnel1 = PoolManager.Instance.GetFromPool(EPoolType.T64642, m_SpawnPos);
                m_SpawnPos += m_ObstacleSpawn;
                PoolManager.Instance.GetFromPool(EPoolType.obstaclesSpawnPos, m_SpawnPos);
                m_SpawnPos += m_ObstacleSpawn;
                tunnel2 = PoolManager.Instance.GetFromPool(EPoolType.T64642, m_SpawnPos);
                m_SpawnPos += m_FollowTransition;
                m_TunnelElements.Add(tunnel1);
                m_TunnelElements.Add(tunnel2);
                m_ElementPoolType.Add(EPoolType.T64642);
                m_ElementPoolType.Add(EPoolType.T64642);
                break;
            case 5:
                tunnel1 = PoolManager.Instance.GetFromPool(EPoolType.T9494, m_SpawnPos);
                m_SpawnPos += m_ObstacleSpawn;
                PoolManager.Instance.GetFromPool(EPoolType.obstaclesSpawnPos, m_SpawnPos);
                m_SpawnPos += m_ObstacleSpawn;
                tunnel2 = PoolManager.Instance.GetFromPool(EPoolType.T9494, m_SpawnPos);
                m_SpawnPos += m_FollowTransition;
                m_TunnelElements.Add(tunnel1);
                m_TunnelElements.Add(tunnel2);
                m_ElementPoolType.Add(EPoolType.T9494);
                m_ElementPoolType.Add(EPoolType.T9494);
                break;
        }
        m_LastTunnel = nextTunnel;
    }

    public void ReturnStuff()
    {
        for(int i = 0; i <= m_TunnelElements.Count - 1; i++)
        {
            PoolManager.Instance.ReturnToPool(m_ElementPoolType[0], m_TunnelElements[0]);
            m_ElementPoolType.RemoveAt(0);
            m_TunnelElements.RemoveAt(0);
        }
    }
}

