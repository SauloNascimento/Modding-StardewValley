using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Framework
{
    class Characters
    {
        public static readonly string Abigail = "Abigail";
        public static readonly string Alex = "Alex";
        public static readonly string Caroline = "Caroline";
        public static readonly string Clint = "Clint";
        public static readonly string Demetrius = "Demetrius";
        public static readonly string Dwarf = "Dwarf";
        public static readonly string Elliot = "Elliot";
        public static readonly string Emily = "Emily";
        public static readonly string Evelyn = "Evelyn";
        public static readonly string George = "George";
        public static readonly string Gus = "Gus";
        public static readonly string Haley = "Haley";
        public static readonly string Harvey = "Harvey";
        public static readonly string Jas = "Jas";
        public static readonly string Jodi ="Jodi";
        public static readonly string Kent ="Kent";
        public static readonly string Krobus = "Krobus";
        public static readonly string Leah = "Leah";
        public static readonly string Leo = "Leo";
        public static readonly string Lewis = "Lewis";
        public static readonly string Linus = "Linus";
        public static readonly string Marnie = "Marnie";
        public static readonly string Maru = "Maru";
        public static readonly string Pam = "Pam";
        public static readonly string Penny = "Penny";
        public static readonly string Pierre = "Pierre";
        public static readonly string Robin = "Robin";
        public static readonly string Sam = "Sam";
        public static readonly string Sandy = "Sandy";
        public static readonly string Sebastian = "Sebastian";
        public static readonly string Shane = "Shane";
        public static readonly string Vincent = "Vincent";
        public static readonly string Willy = "Willy";
        public static readonly string Wizard = "Wizard";

        /*public static List<string> All()
        {
            return typeof(Characters).GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(f => f.FieldType == typeof(string))
                .Select(field => (string) field.GetValue(null)).ToList();
        }*/
    }
}
