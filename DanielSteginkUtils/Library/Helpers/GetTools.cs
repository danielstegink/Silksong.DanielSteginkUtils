using System.Collections.Generic;
using System.Linq;

namespace DanielSteginkUtils.Helpers
{
    /// <summary>
    /// Library for reviewing tools
    /// </summary>
    public static class GetTools
    {
        /// <summary>
        /// Gets a list of all equipped tools
        /// </summary>
        /// <returns></returns>
        public static List<string> GetEquippedTools()
        {
            List<string> equippedTools = new List<string>();
            List<ToolItem> toolData = ToolItemManager.GetUnlockedTools().ToList();
            foreach (ToolItem toolItem in toolData)
            {
                if (toolItem.IsEquipped)
                {
                    equippedTools.Add(toolItem.name);
                }
            }

            return equippedTools;
        }
    }
}