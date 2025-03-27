using System.Text;

namespace RuneKeeperNotes
{
    public static class RuneData
    {
        public record Rune(string Name, string Symbol, string Meaning);
        // Two dictionaries for lookups
        private static readonly Dictionary<string, Rune> RunesByName = new();
        private static readonly Dictionary<string, Rune> RunesBySymbol = new();

        static RuneData()
        {
            // Initialize runes in both dictionaries
            var runeList = new List<Rune>
        {
            new Rune("Fehu", "ᚠ", "Wealth, prosperity, and new beginnings."),
            new Rune("Uruz", "ᚢ", "Strength, endurance, and health."),
            new Rune("Thurisaz", "ᚦ", "Protection, defense, and challenges."),
            new Rune("Ansuz", "ᚨ", "Wisdom, communication, and divine inspiration."),
            new Rune("Raidho", "ᚱ", "Travel, journey, and personal growth."),
            new Rune("Kenaz", "ᚲ", "Knowledge, creativity, and enlightenment."),
            new Rune("Gebo", "ᚷ", "Gift, balance, and partnership."),
            new Rune("Wunjo", "ᚹ", "Joy, harmony, and success."),
            new Rune("Hagalaz", "ᚺ", "Disruption, transformation, and necessary change."),
            new Rune("Nauthiz", "ᚾ", "Need, restriction, and resilience."),
            new Rune("Isa", "ᛁ", "Stillness, patience, and inner focus."),
            new Rune("Jera", "ᛃ", "Harvest, rewards, and natural cycles."),
            new Rune("Eiwaz", "ᛇ", "Transformation, death, and rebirth."),
            new Rune("Perthro", "ᛈ", "Mystery, fate, and hidden knowledge."),
            new Rune("Algiz", "ᛉ", "Protection, higher self, and divine guidance."),
            new Rune("Sowilo", "ᛋ", "Success, energy, and vitality."),
            new Rune("Tiwaz", "ᛏ", "Honor, justice, and warrior energy."),
            new Rune("Berkano", "ᛒ", "Growth, fertility, and healing."),
            new Rune("Ehwaz", "ᛖ", "Movement, progress, and trust."),
            new Rune("Mannaz", "ᛗ", "Humanity, self-awareness, and cooperation."),
            new Rune("Laguz", "ᛚ", "Intuition, flow, and the subconscious."),
            new Rune("Ingwaz", "ᛜ", "Fertility, potential, and inner growth."),
            new Rune("Dagaz", "ᛞ", "Breakthrough, hope, and clarity."),
            new Rune("Othala", "ᛟ", "Heritage, home, and spiritual legacy.")
        };

            // Populate both dictionaries for fast lookup
            foreach (var rune in runeList)
            {
                RunesByName[rune.Name] = rune;
                RunesBySymbol[rune.Symbol] = rune;
            }
        }

        // Method to check if the rune name is valid
        public static bool IsValidRuneByName(string runeName)
        {
            return RunesByName.ContainsKey(runeName);
        }

        // Method to check if the rune symbol is valid
        public static bool IsValidRuneBySymbol(string runeSymbol)
        {
            return RunesBySymbol.ContainsKey(runeSymbol);
        }

        // Method to get rune details by name
        public static Rune? GetRuneByName(string runeName)
        {
            RunesByName.TryGetValue(runeName, out var rune);
            return rune;
        }

        // Method to get rune details by symbol
        public static Rune? GetRuneBySymbol(string runeSymbol)
        {
            RunesBySymbol.TryGetValue(runeSymbol, out var rune);
            return rune;
        }
    }

}
