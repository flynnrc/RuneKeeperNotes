using System.Text;

namespace RuneKeeperNotes
{
    public static class RuneData
    {
        public record Rune(string Name, string Symbol, string Meaning);
        public static readonly List<Rune> Runes = new()
        {
            new("Fehu", "ᚠ", "Wealth, prosperity, and new beginnings."),
            new("Uruz", "ᚢ", "Strength, endurance, and health."),
            new("Thurisaz", "ᚦ", "Protection, defense, and challenges."),
            new("Ansuz", "ᚨ", "Wisdom, communication, and divine inspiration."),
            new("Raidho", "ᚱ", "Travel, journey, and personal growth."),
            new("Kenaz", "ᚲ", "Knowledge, creativity, and enlightenment."),
            new("Gebo", "ᚷ", "Gift, balance, and partnership."),
            new("Wunjo", "ᚹ", "Joy, harmony, and success."),
            new("Hagalaz", "ᚺ", "Disruption, transformation, and necessary change."),
            new("Nauthiz", "ᚾ", "Need, restriction, and resilience."),
            new("Isa", "ᛁ", "Stillness, patience, and inner focus."),
            new("Jera", "ᛃ", "Harvest, rewards, and natural cycles."),
            new("Eiwaz", "ᛇ", "Transformation, death, and rebirth."),
            new("Perthro", "ᛈ", "Mystery, fate, and hidden knowledge."),
            new("Algiz", "ᛉ", "Protection, higher self, and divine guidance."),
            new("Sowilo", "ᛋ", "Success, energy, and vitality."),
            new("Tiwaz", "ᛏ", "Honor, justice, and warrior energy."),
            new("Berkano", "ᛒ", "Growth, fertility, and healing."),
            new("Ehwaz", "ᛖ", "Movement, progress, and trust."),
            new("Mannaz", "ᛗ", "Humanity, self-awareness, and cooperation."),
            new("Laguz", "ᛚ", "Intuition, flow, and the subconscious."),
            new("Ingwaz", "ᛜ", "Fertility, potential, and inner growth."),
            new("Dagaz", "ᛞ", "Breakthrough, hope, and clarity."),
            new("Othala", "ᛟ", "Heritage, home, and spiritual legacy.")
        };
    }

}
