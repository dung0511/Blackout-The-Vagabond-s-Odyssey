using UnityEngine;


public abstract class BaseSkill : MonoBehaviour
{
    public abstract void NormalSkill();
    public abstract void UltimmateSkill();
    public abstract bool CanUseSkill1();
    public abstract bool CanUseSkill2();
    public abstract bool IsUsingSkill();
    public abstract void SetIsUsingNormalFalse();
    public abstract void SetIsUsingUltimateFalse();
    public abstract void SetIsUsingUltimate();
    public abstract void SetIsUsingNormal();
    public abstract void SetActiveWeapon();
    public abstract void setActiveFalseWeapon();


}

