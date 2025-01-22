using UnityEngine;

[CreateAssetMenu(fileName = "SignalEmitterConfig", menuName = "Configs/SignalEmitterConfig")]
public class SignalEmitterConfig : ScriptableObject
{
    public GameObject SignalParticlePrefab;
    public int WaveParticleCount;
    public float WaveSpreadAngle;
    public float WaveSpawnDistance;
    public Color WaveParticlesColor;
    public float SpawnInterval;
}
