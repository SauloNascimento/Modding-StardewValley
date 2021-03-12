namespace SocialNetwork.Framework
{
    /// <summary>
    /// Stores all characters names to avoid mistyping.
    /// </summary>
    class Characters
    {
        public static string Abigail => "Abigail";
        public static string Alex => "Alex";
        public static string Caroline => "Caroline";
        public static string Clint => "Clint";
        public static string Demetrius => "Demetrius";
        public static string Dwarf => "Dwarf";
        public static string Elliot => "Elliot";
        public static string Emily => "Emily";
        public static string Evelyn => "Evelyn";
        public static string George => "George";
        public static string Gus => "Gus";
        public static string Haley => "Haley";
        public static string Harvey => "Harvey";
        public static string Jas => "Jas";
        public static string Jodi => "Jodi";
        public static string Kent => "Kent";
        public static string Krobus => "Krobus";
        public static string Leah => "Leah";
        public static string Leo => "Leo";
        public static string Lewis => "Lewis";
        public static string Linus => "Linus";
        public static string Marnie => "Marnie";
        public static string Maru => "Maru";
        public static string Pam => "Pam";
        public static string Penny => "Penny";
        public static string Pierre => "Pierre";
        public static string Robin => "Robin";
        public static string Sam => "Sam";
        public static string Sandy => "Sandy";
        public static string Sebastian => "Sebastian";
        public static string Shane => "Shane";
        public static string Vincent => "Vincent";
        public static string Willy => "Willy";
        public static string Wizard => "Wizard";

        /*public static List<string> All()
        {
            return typeof(Characters).GetProperties(BindingFlags.Public | BindingFlags.Static)
                .Where(p => p.PropertyType == typeof(string))
                .Select(prop => (string) prop.GetValue(null)).ToList();
        }*/
    }
}
