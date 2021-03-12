using UnityEngine;

public abstract class BossBaseState
{
    public abstract void EnterState(BossController boss);

    public abstract void Update(BossController boss);
}
