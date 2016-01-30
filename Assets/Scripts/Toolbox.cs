public class Toolbox : Singleton<Toolbox>
{
    protected Toolbox() { } // guarantee this will be always a singleton only - can't use the constructor!

    public string someoneIsGod = "false";

    public Language language = new Language();

    void Awake()
    {
        someoneIsGod = "false";
    }

    // (optional) allow runtime registration of global objects
    //static public T RegisterComponent<T>()
    //{
    //    return Instance.GetOrAddComponent<T>();
    //}
}

[System.Serializable]
public class Language
{
    public string current;
    public string lastLang;
}
