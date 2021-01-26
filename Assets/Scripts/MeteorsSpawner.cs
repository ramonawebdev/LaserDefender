
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorsSpawner : MonoBehaviour {

    bool spawn = true;
    [SerializeField] float min = 1f, max = 5f, objSpeed = 3f;
    [SerializeField] GameObject[] prefabs;
    [SerializeField] int spawners = 100;
    int i = 0;
    float delayInSeconds = 3f;

    IEnumerator Start () {
        while (spawn)
        {
            yield return new WaitForSeconds(Random.Range(min, max));
            SpawnObj();
        }
    }

   private void SpawnObj()
    {
        int i = Random.Range(0, prefabs.Length);
        Spawn(prefabs[i]);

    }

    private void Spawn(GameObject prefab)
    {
        GameObject prefabObj = Instantiate(prefab, transform.position, Quaternion.identity) as GameObject;
        prefabObj.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -objSpeed);
        i++;
        if (i >= spawners)
        {
            spawn = false;
            LoadNext();
        }
    }

    public void LoadNext()
    {
        StartCoroutine(WaitAndLoad());
    }

    IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(delayInSeconds);
        FindObjectOfType<Level>().LoadNextScene();

    }
}
