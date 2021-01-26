using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeShield : MonoBehaviour {

    [SerializeField] int life = 200;

    public int GetLife()
    {
        return life;
    }
}
