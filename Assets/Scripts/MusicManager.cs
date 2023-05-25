using System;

public class MusicManager : GenericSingleton<MusicManager>
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    
}
