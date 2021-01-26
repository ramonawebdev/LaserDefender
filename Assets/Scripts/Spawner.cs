
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    bool spawn = true;
    [SerializeField] float min = 1f, max = 5f, objSpeed = 3f;
    [SerializeField] GameObject[] prefabs;

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
    }
}
