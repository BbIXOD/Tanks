using UnityEngine;

public class AdditionalModule : MonoBehaviour, IModule
{
    [SerializeField]private Module module;
    
    public Module Root => module;
    
    
}
