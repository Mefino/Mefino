using System;
using System.Collections.Generic;

namespace Mefino.Core
{
    public static class PackageTags
    {
        public enum Tags
        {
            Balancing,
            Characters,
            Classes,
            Items,
            Library,
            Mechanics,
            Quests,
            Skills,
            Utility,
            UI,
        }

        public static HashSet<string> AcceptedTags
        {
            get
            {
                if (s_acceptedTags == null)
                {
                    s_acceptedTags = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                    foreach (var value in Enum.GetValues(typeof(Tags)))
                        s_acceptedTags.Add(value.ToString());
                }
                return s_acceptedTags;
            }
        }
        private static HashSet<string> s_acceptedTags;

        public static bool IsValidTag(string tag, bool showLibraries = true)
        {
            if (!showLibraries && string.Equals(tag, "library", StringComparison.OrdinalIgnoreCase))
                return false;

            return AcceptedTags.Contains(tag);
        }
    }
}
