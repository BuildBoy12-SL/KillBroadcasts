// -----------------------------------------------------------------------
// <copyright file="EventHandlers.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace KillBroadcasts
{
    using Exiled.API.Features;
    using Exiled.Events.EventArgs;

    /// <summary>
    /// Handles events derived from <see cref="Exiled.Events.Handlers"/>.
    /// </summary>
    public class EventHandlers
    {
        private readonly Plugin plugin;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandlers"/> class.
        /// </summary>
        /// <param name="plugin">An instance of the plugin class.</param>
        public EventHandlers(Plugin plugin) => this.plugin = plugin;

        /// <inheritdoc cref="Exiled.Events.Handlers.Player.OnDying(DyingEventArgs)"/>
        public void OnDying(DyingEventArgs ev)
        {
            Broadcast broadcast = GetRelatedBroadcast(ev);
            broadcast = FormatBroadcast(broadcast, ev);
            foreach (Player player in Player.Get(RoleType.Spectator))
                player.Broadcast(broadcast);
        }

        private Broadcast GetRelatedBroadcast(DyingEventArgs ev)
        {
            if (ev.Killer == null)
                return plugin.Config.SuicideBroadcast;

            if (ev.Target.IsCuffed)
                return plugin.Config.CuffedBroadcast;

            return plugin.Config.KillBroadcast;
        }

        private Broadcast FormatBroadcast(Broadcast broadcast, DyingEventArgs ev)
        {
            string content = broadcast.Content.Replace("$DamageType", ev.Handler.Type.Translation())
                .Replace("$KilledName", ev.Target.Nickname)
                .Replace("$KilledRole", ev.Target.Role.Translation())
                .Replace("$KilledRoleColor", ev.Target.RoleColor.ToHex())
                .Replace("$KilledUserId", ev.Target.UserId);

            if (ev.Killer != null)
            {
                content = content.Replace("$KillersName", ev.Killer.Nickname)
                    .Replace("$KillersRole", ev.Killer.Role.Translation())
                    .Replace("$KillersRoleColor", ev.Killer.RoleColor.ToHex())
                    .Replace("$KillersUserId", ev.Killer.UserId);
            }

            return new Broadcast(content, broadcast.Duration, broadcast.Show, broadcast.Type);
        }
    }
}