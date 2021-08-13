using System;

namespace CCMvNext.Infrastructure.ReinforcedTypings
{
    /// <summary>
    /// Provides common extensions for the Reinforced.Typing configuration.
    /// </summary>
    public static class Common
    {
        /// <summary>
        /// Gets the Short name: removes 'Controller' name part from the type name to comply existing routing.
        /// </summary>
        /// <param name="t">Type to get name.</param>
        /// <returns>The actual item name.</returns>
        public static string GetShortName(this Type t)
        {
            return t.Name.Replace("Controller", string.Empty);
        }
    }
}
