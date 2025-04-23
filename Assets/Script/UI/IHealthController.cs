using UnityEngine;
using UnityEngine.Events;

public interface IHealthController 
{
    float MaxHealth { get; }
    float CurrentHealth { get; }
    UnityEvent<float> OnHealthChanged { get; }
}
