using Eremite;
using Eremite.Model;
using Eremite.WorldMap;

namespace MyFirstMod
{
    public class Utils
    {        
        public static void SetSeasonsTime(float drizzleTime, float clearanceTime, float stormTime, SeasonQuarter firstCornerstoneTime = SeasonQuarter.Second)
        {
            foreach (BiomeModel biome in MB.Settings.biomes)
            {
                biome.seasons.DrizzleTime = drizzleTime;
                biome.seasons.ClearanceTime = clearanceTime;
                biome.seasons.StormTime = stormTime;
                biome.seasons.SeasonRewards[0].quarter = firstCornerstoneTime;
            }
        }
        public static string GetGoodIconAndName(GoodModel good)
        {

            string goodName = good.Name;
            int indexOfClosingParentheses = goodName.LastIndexOf("]");
            if (indexOfClosingParentheses != -1)
            {
                goodName = goodName.Substring(indexOfClosingParentheses + 1);
            }
            else if (goodName.Contains("_Meta"))
            {
                goodName = goodName.Substring("_Meta".Length + 1);
            }

            goodName = goodName.Trim();

            return $"<sprite name=\"{good.Name.ToLowerInvariant()}\"> {goodName}";
        }
    }
}
