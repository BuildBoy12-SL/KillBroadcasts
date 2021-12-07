// -----------------------------------------------------------------------
// <copyright file="Extensions.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace KillBroadcasts
{
    using Exiled.API.Enums;

    /// <summary>
    /// Various extension methods.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Returns the translation of the role based on <see cref="Config.RoleTranslations"/>.
        /// </summary>
        /// <param name="roleType">The role to translate.</param>
        /// <returns>The translated role.</returns>
        public static string Translation(this RoleType roleType)
        {
            if (Plugin.Instance.Config.RoleTranslations.TryGetValue(roleType, out string translation))
                return translation;

            return roleType.ToString();
        }

        /// <summary>
        /// Returns the translation of the damage type based on <see cref="Config.DamageTranslations"/>.
        /// </summary>
        /// <param name="damageType">The damage type to translate.</param>
        /// <returns>The translated damage type.</returns>
        public static string Translation(this DamageType damageType)
        {
            if (Plugin.Instance.Config.DamageTranslations.TryGetValue(damageType, out string translation))
                return translation;

            return damageType.ToString();
        }
    }
}