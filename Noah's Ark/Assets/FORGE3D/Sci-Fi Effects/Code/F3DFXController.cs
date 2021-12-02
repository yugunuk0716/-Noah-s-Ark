using UnityEngine;
using System.Collections;
using System;

namespace FORGE3D
{
   

    public class F3DFXController : MonoBehaviour
    {
        // Singleton instance
        public static F3DFXController instance;


        // Current firing socket
        private int curSocket = 0;

        // Timer reference                
        private int timerID = -1;

        [Header("Turret setup")]
        public Transform[] TurretSocket; // Sockets reference
        public ParticleSystem[] ShellParticles; // Bullet shells particle system


        [Header("Vulcan")] 
        public Transform vulcanProjectile; // Projectile prefab
        public Transform vulcanMuzzle; // Muzzle flash prefab  
        public Transform vulcanImpact; // Impact prefab
        public float vulcanOffset;
        public float VulcanFireRate = 0.07f;

        [Header("Skill Seeker")]
        public Transform seekerProjectile;
        public Transform seekerMuzzle;
        public Transform seekerImpact;
        public float seekerOffset;

        private void Awake()
        {
            // Initialize singleton  
            instance = this;

            // Initialize bullet shells particles
            for (int i = 0; i < ShellParticles.Length; i++)
            {
                var em = ShellParticles[i].emission;
                em.enabled = false;
                ShellParticles[i].Stop();
                ShellParticles[i].gameObject.SetActive(true);
            }
        }



        // Advance to next turret socket
        private void AdvanceSocket()
        {
            curSocket++;
            if (curSocket >= TurretSocket.Length)
                curSocket = 0;
        }

        // Fire turret weapon
        public void Fire(bool isSkill = false)
        {

            if (!isSkill) 
            {
                // Fire vulcan at specified rate until canceled
                timerID = F3DTime.time.AddTimer(VulcanFireRate, Vulcan);
                // Invoke manually before the timer ticked to avoid initial delay
                Vulcan();
            }
            if(isSkill)
            {
                timerID = F3DTime.time.AddTimer(VulcanFireRate, Seeker);
                Seeker();
            }


        }

        // Stop firing 
        public void Stop()
        {
            // Remove firing timer
            if (timerID != -1)
            {
                F3DTime.time.RemoveTimer(timerID);
                timerID = -1;
            }

            
        }

        // Fire vulcan weapon
        private void Vulcan()
        {
            // Get random rotation that offset spawned projectile
            var offset = Quaternion.Euler(UnityEngine.Random.onUnitSphere);
            // Spawn muzzle flash and projectile with the rotation offset at current socket position
            F3DPoolManager.Pools["GeneratedPool"].Spawn(vulcanMuzzle, TurretSocket[curSocket].position,
                TurretSocket[curSocket].rotation, TurretSocket[curSocket]);
            var newGO =
                F3DPoolManager.Pools["GeneratedPool"].Spawn(vulcanProjectile,
                    TurretSocket[curSocket].position + TurretSocket[curSocket].forward,
                    offset * TurretSocket[curSocket].rotation, null).gameObject;

            var proj = newGO.gameObject.GetComponent<F3DProjectile>();
            if (proj)
            {
                proj.SetOffset(vulcanOffset);
            }

            // Emit one bullet shell
            if (ShellParticles.Length > 0)
                ShellParticles[curSocket].Emit(1);

            // Play shot sound effect
            F3DAudioController.instance.VulcanShot(TurretSocket[curSocket].position);

            // Advance to next turret socket
            AdvanceSocket();
        }

        // Spawn vulcan weapon impact
        public void VulcanImpact(Vector3 pos)
        {
            // Spawn impact prefab at specified position
            F3DPoolManager.Pools["GeneratedPool"].Spawn(vulcanImpact, pos, Quaternion.identity, null);
            // Play impact sound effect
            F3DAudioController.instance.VulcanHit(pos);
        }

        private void Seeker()
        {
            var offset = Quaternion.Euler(UnityEngine.Random.onUnitSphere);
            F3DPoolManager.Pools["GeneratedPool"].Spawn(seekerMuzzle, TurretSocket[curSocket].position,
                TurretSocket[curSocket].rotation, TurretSocket[curSocket]);
            var newGO =
                F3DPoolManager.Pools["GeneratedPool"].Spawn(seekerProjectile, TurretSocket[curSocket].position,
                    offset * TurretSocket[curSocket].rotation, null).gameObject;
            var proj = newGO.GetComponent<F3DProjectile>();
            if (proj)
            {
                proj.SetOffset(seekerOffset);
            }

            F3DAudioController.instance.SeekerShot(TurretSocket[curSocket].position);
            AdvanceSocket();
        }

        // Spawn seeker weapon impact
        public void SeekerImpact(Vector3 pos)
        {
            F3DPoolManager.Pools["GeneratedPool"].Spawn(seekerImpact, pos, Quaternion.identity, null);
            F3DAudioController.instance.SeekerHit(pos);
        }


    }
}