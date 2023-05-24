using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/PlayerSkinData", fileName = "New PlayerSkinData")]
public class PlayerSkinData : ScriptableObject
{
    public int BodyIndex;
    public int BodypartIndex;
    public int EyeIndex;
    public int GloveIndex;
    public int HeadIndex;
    public int MouthIndex;
    public int TailIndex;
}
