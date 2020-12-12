using System;

namespace Interfaces
{
    public interface IKillable
    {
        event Action OnDeath;
        void Kill();
    }
}