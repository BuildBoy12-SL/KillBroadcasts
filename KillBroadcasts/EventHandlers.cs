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
    using MEC;

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
            string content = FormatContent(broadcast.Content, ev);

            Timing.CallDelayed(0.1f, () =>
            {
                foreach (Player player in Player.Get(RoleType.Spectator))
                    player.Broadcast(broadcast.Duration, content, broadcast.Type, broadcast.Show);
            });
        }

        private Broadcast GetRelatedBroadcast(DyingEventArgs ev)
        {
            if (ev.Killer == null)
                return plugin.Config.SuicideBroadcast;

            if (ev.Target.IsCuffed)
                return plugin.Config.CuffedBroadcast;

            return plugin.Config.KillBroadcast;
        }

        private string FormatContent(string content, DyingEventArgs ev)
        {
            content = content.Replace("$DamageType", ev.Handler.Type.Translation())
                .Replace("$KilledName", ev.Target.Nickname)
                .Replace("$KilledRoleColor", ev.Target.Role.Color.ToHex())
                .Replace("$KilledRole", ev.Target.Role.Type.Translation())
                .Replace("$KilledUserId", ev.Target.UserId);

            if (ev.Killer != null)
            {
                content = content.Replace("$KillersName", ev.Killer.Nickname)
                    .Replace("$KillersRoleColor", ev.Killer.Role.Color.ToHex())
                    .Replace("$KillersRole", ev.Killer.Role.Type.Translation())
                    .Replace("$KillersUserId", ev.Killer.UserId);
            }

            return content;
        }
    }
}