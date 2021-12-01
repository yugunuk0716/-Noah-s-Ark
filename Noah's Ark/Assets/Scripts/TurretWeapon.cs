using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurretWeaponType 
{
    Vulcan,
    SoloGun,
    Sniper
}

public class TurretWeapon : MonoBehaviour
{
    protected TurretWeaponType weaponType = TurretWeaponType.Vulcan;
    protected Transform projectile;
    protected Transform muzzle;
    protected Transform impact;
}
