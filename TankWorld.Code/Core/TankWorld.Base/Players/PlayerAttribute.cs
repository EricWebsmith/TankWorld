using System;

namespace TankWorld.Core
{
    /// <summary>
    /// You must decorate your player with this attribute.
    /// </summary>
    public class PlayerAttribute : Attribute
    {
        /// <summary>
        /// Displays in UI. A readable name.
        /// </summary>
        public string DisplayName { get; set; }
    }
}
