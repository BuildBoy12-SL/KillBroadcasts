// -----------------------------------------------------------------------
// <copyright file="Config.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace KillBroadcasts
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using Exiled.API.Enums;
    using Exiled.API.Features;
    using Exiled.API.Interfaces;

    /// <inheritdoc />
    public class Config : IConfig
    {
        /// <inheritdoc />
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// Gets or sets the basic broadcast to send when a player kills another player.
        /// </summary>
        [Description("The basic broadcast to send when a player kills another player.")]
        public Broadcast KillBroadcast { get; set; } = new Broadcast("$KilledName [<color=$KilledRoleColor>$KilledRole</color>] was killed by $KillersName as <color=$KillersRoleColor>$KillersRole</color> with $DamageType", 5);

        /// <summary>
        /// Gets or sets the broadcast to send when a player kills a handcuffed player.
        /// </summary>
        [Description("The broadcast to send when a player kills a handcuffed player.")]
        public Broadcast CuffedBroadcast { get; set; } = new Broadcast("$KillersUserId killed $KilledUserId", 5);

        /// <summary>
        /// Gets or sets the broadcast to send when a player commits suicide.
        /// </summary>
        [Description("The broadcast to send when a player commits suicide.")]
        public Broadcast SuicideBroadcast { get; set; } = new Broadcast("$KilledName committed suicide.");

        /// <summary>
        /// Gets or sets a collection of roles and the names that correlate with them.
        /// </summary>
        [Description("A collection of roles and the names that correlate with them.")]
        public Dictionary<RoleType, string> RoleTranslations { get; set; } = new Dictionary<RoleType, string>
        {
            { RoleType.ClassD, "Class D" },
            { RoleType.Scp106, "Scp106" },
        };

        /// <summary>
        /// Gets or sets a collection of damage types and the names that correlate with them.
        /// </summary>
        [Description("A collection of damage types and the names that correlate with them.")]
        public Dictionary<DamageType, string> DamageTranslations { get; set; } = new Dictionary<DamageType, string>
        {
            { DamageType.Poison, "<color=green>poison</color>" },
        };
    }
}