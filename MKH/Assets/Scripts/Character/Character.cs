using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] int _health;
    public int Health => _health;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddHealth(int addHealth)
    {
        _health += addHealth;
    }

    public virtual void Hit(int damage)
    {
        AddHealth(-damage);
    }
}
