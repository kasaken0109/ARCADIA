using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IDamage
{
    void AddDamage(int damage);
}

interface IGetDamage
{
    void GetDamage(int damage);
}
