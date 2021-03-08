using System;
using System.CodeDom.Compiler;
using System.Diagnostics.CodeAnalysis;
using StardewModdingAPI;

namespace GreenhouseUpgrades
{
    /// <summary>Get translations from the mod's <c>i18n</c> folder.</summary>
    /// <remarks>This is auto-generated from the <c>i18n/default.json</c> file when the T4 template is saved.</remarks>
    [GeneratedCode("TextTemplatingFileGenerator", "1.0.0")]
    [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Deliberately named for consistency and to match translation conventions.")]
    internal static class I18n
    {
        /*********
        ** Fields
        *********/
        /// <summary>The mod's translation helper.</summary>
        private static ITranslationHelper Translations;


        /*********
        ** Public methods
        *********/
        /// <summary>Construct an instance.</summary>
        /// <param name="translations">The mod's translation helper.</param>
        public static void Init(ITranslationHelper translations)
        {
            I18n.Translations = translations;
        }

        /// <summary>Get a translation equivalent to "Big Greenhouse".</summary>
        public static string Upgrade1_Name()
        {
            return I18n.GetByKey("upgrade1.name");
        }

        /// <summary>Get a translation equivalent to "Increase growing space to a 15x15.".</summary>
        public static string Upgrade1_Description()
        {
            return I18n.GetByKey("upgrade1.description");
        }

        /// <summary>Get a translation equivalent to "Deluxe Greenhouse".</summary>
        public static string Upgrade2_Name()
        {
            return I18n.GetByKey("upgrade2.name");
        }

        /// <summary>Get a translation equivalent to "Increase growing space to a 21x21. Comes with a auto-watering system.".</summary>
        public static string Upgrade2_Description()
        {
            return I18n.GetByKey("upgrade2.description");
        }


        /*********
        ** Private methods
        *********/
        /// <summary>Get a translation by its key.</summary>
        /// <param name="key">The translation key.</param>
        /// <param name="tokens">An object containing token key/value pairs. This can be an anonymous object (like <c>new { value = 42, name = "Cranberries" }</c>), a dictionary, or a class instance.</param>
        private static Translation GetByKey(string key, object tokens = null)
        {
            if (I18n.Translations == null)
                throw new InvalidOperationException($"You must call {nameof(I18n)}.{nameof(I18n.Init)} from the mod's entry method before reading translations.");
            return I18n.Translations.Get(key, tokens);
        }
    }
}

