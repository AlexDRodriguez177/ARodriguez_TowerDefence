using UnityEngine;

public class FireBallTower : Tower
{   
    [SerializeField] private ParticleSystem fireEffect;
    /// <summary>
    /// Works like the base Tower class, but uses a fireball projectile.
    /// </summary>
    protected override void Update()
    {
        base.Update();
    }
    /// <summary>
    /// Fires a fireball at the target enemy.
    /// Plays the fire effect when firing.
    /// </summary>
    protected override void FireAt(Enemy target)
    {
        base.FireAt(target);
        fireEffect.Play();
        
    }
}
