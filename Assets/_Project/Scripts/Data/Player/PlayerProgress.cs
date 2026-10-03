using System;


namespace _Project.Scripts.Data.Player
{
    [Serializable]
    public class PlayerProgress
    {
        public AudioSettingsData AudioSettingsData = new AudioSettingsData();
        public long LastSaveTimeUnix;
    }
}