using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Script.Service
{
    public abstract class WeaponService : IWeaponService
    {
        protected readonly IWeaponRepository _weaponRepository;
        protected bool _inHand;
        protected SpriteRenderer _characterSR;
        protected Transform _transform;
        protected WeaponService(IWeaponRepository weaponRepository, bool inHand, SpriteRenderer characterSR, Transform transform)
        {
            _weaponRepository = weaponRepository;
            _inHand = inHand;
            _characterSR = characterSR;
            _transform = transform;
        }

        public abstract void Attack();
        public abstract void RotateWeapon();
        public abstract void DropWeapon();
        public abstract void PickWeapon();
    }
}
