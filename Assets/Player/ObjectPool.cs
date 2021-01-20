using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance;
    public List<GameObject> pooledObjects;
    public List<GameObject> pooledFlamesObjects;

    public GameObject bulletsParent;
    public GameObject flamesParent;

    public GameObject objectToPool;
    public GameObject flameObjectToPool;
    public int amountToPool;
    public int flamesAmountToPool;

    private void Awake()
    {
        SharedInstance = this;
    }

    private void Start()
    {
        pooledObjects = new List<GameObject>();
        pooledFlamesObjects= new List<GameObject>();
        GameObject tmp;
        
        for(int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.transform.parent = bulletsParent.transform;
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
        GameObject tmpFlame;

        for(int i=0; i<flamesAmountToPool; i++)
        {
            tmpFlame = Instantiate(flameObjectToPool);
            tmpFlame.transform.parent = flamesParent.transform;
            tmpFlame.SetActive(false);
            pooledFlamesObjects.Add(tmpFlame);
        }
    }

    public GameObject GetPooledObject()
    {
        for(int i=0; i<amountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
    
    public GameObject GetPooledFlameObject()
    {
        for(int i=0; i<flamesAmountToPool; i++)
        {
            if (!pooledFlamesObjects[i].activeInHierarchy)
            {
                return pooledFlamesObjects[i];
            }
        }
        return null;
    }
}
