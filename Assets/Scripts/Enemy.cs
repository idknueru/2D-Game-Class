using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public int score = 10;
    protected override void OnDie()
    {
        base.OnDie();
        GameController.Instance.OnEnemyDie(this);
    }
}
