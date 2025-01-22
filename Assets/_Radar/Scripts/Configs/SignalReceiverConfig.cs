using UnityEngine;

[CreateAssetMenu(fileName = "SignalReceiverConfig", menuName = "Configs/SignalReceiverConfig", order = 1)]
public class SignalReceiverConfig : ScriptableObject
{
    public bool desiredReceivingSignalState;
    public bool defaultReceivingSignalState;
        
    public Color normalColor;
    public Color receivingSignalColor;
}
