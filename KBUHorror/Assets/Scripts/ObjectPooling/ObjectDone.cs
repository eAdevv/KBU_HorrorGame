
public class ObjectDone : PoolObject
{
    public override void OnAwake()
    {
        if (poolKey.Equals("impactEffect")) Done(0.2f);

        if (poolKey.Equals("bulletHole")) Done(2f);
    }
}
