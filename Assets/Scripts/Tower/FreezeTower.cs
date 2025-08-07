using UnityEngine;

public class FreezeTower : Tower
{
    [SerializeField] private ParticleSystem freezeEffect;

    protected override void Update()
    {
        base.Update();
    }

    protected override void FireAt(Enemy target)
    {
        base.FireAt(target);

        if (freezeEffect != null)
        {
            freezeEffect.Play();
        }
    }
}
