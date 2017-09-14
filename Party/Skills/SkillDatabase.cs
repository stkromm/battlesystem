public static class SkillDatabase
{
    private static Skill[] _instance;
    public static Skill[] Skills()
    {
        return _instance ?? (_instance = XmlManager.LoadXmlSkillDatabase());
    }
}

