using System;
using System.Linq;
using Torch.Commands;
using Torch.Commands.Permissions;
using VRage.Game.ModAPI;
using Sandbox.ModAPI;
using Sandbox.Game;
using Sandbox.Game.World;
using System.Collections.Generic;
using VRage.ModAPI;

namespace EventTeams
{

    [Category("et")]
    public class EventTeamsCommands : CommandModule
    {
        public EventTeams Plugin => (EventTeams)Context.Plugin;

        [Command("join", "This command will auto assign you to a faction")]
        [Permission(MyPromoteLevel.None)]
        public void FactionJoin()
        {
            // Make sure only a player can run this command
            if (Context.Player != null)
            {
                // Check if player is already in a faction
                var playerFaction = MyAPIGateway.Session.Factions.TryGetPlayerFaction(Context.Player.IdentityId);

                if (playerFaction != null)
                {
                    Context.Respond("You are already in faction");
                    return;
                }

                // Work out which faction to assign player (one with least amount of players)
                var pairs = new List<KeyValuePair<string, int>>();

                foreach (string Tag in Plugin.Config.FactionTags)
                {
                    try
                    {
                        IMyFaction tempFaction = MyAPIGateway.Session.Factions.TryGetFactionByTag(Tag);
                        int playerCount = tempFaction.Members.Count;

                        var pair = new KeyValuePair<string, int>(Tag, playerCount);

                        pairs.Add(pair);
                    }
                    catch (Exception)
                    {
                        Plugin.Logger.Warn(Tag + " doesn't exist!");
                    }

                }

                var minFactionPair = pairs.MinBy(e => e.Value);

                // Put player in faction

                IMyFaction targetFaction = MyAPIGateway.Session.Factions.TryGetFactionByTag(minFactionPair.Key);

                if (targetFaction.AutoAcceptMember)
                {
                    MyAPIGateway.Session.Factions.SendJoinRequest(targetFaction.FactionId, Context.Player.IdentityId); // Adds player to faction
                    MyAPIGateway.Session.Factions.AcceptJoin(targetFaction.FactionId, Context.Player.IdentityId); // Accepts player to faction

                    // Report back to player
                    Context.Respond("Added to faction: " + minFactionPair.Key);
                    Plugin.Logger.Info("Assigned " + Context.Player.DisplayName + " to faction " + targetFaction.Tag);
                }
                else
                {
                    Context.Respond("Unable to assign faction, not set to accept everyone");
                    Plugin.Logger.Warn(targetFaction.Tag + " not set to accept everyone");
                }

               


            }
            else
            {
                Context.Respond("Only a player can run this command!");
            }
        }

        [Command("reload", "This command will reload the config for Faction Assigner")]
        [Permission(MyPromoteLevel.Admin)]
        public void FactionReload()
        {
            // Reload config
            try
            {
                Plugin.ReloadConfig();
                Context.Respond("Config reloaded!");
            }
            catch (Exception e)
            {
                Context.Respond("Failed to reload config, check log for error");
                Plugin.Logger.Warn(e);
            }

        }

        [Command("kick", "This command will kick the specified player from their current faction")]
        [Permission(MyPromoteLevel.Admin)]
        public void FactionKick(string PlayerName)
        {
            long id = 0;

            foreach (var identity in MySession.Static.Players.GetAllIdentities())
            {
                if (identity.DisplayName == PlayerName)
                {
                    id = identity.IdentityId;
                }
            }

            var targetFaction = MyAPIGateway.Session.Factions.TryGetPlayerFaction(Context.Player.IdentityId);

            MyAPIGateway.Session.Factions.KickMember(targetFaction.FactionId, id);

            Context.Respond("Kicked player from faction");
        }

        [Command("check","checks if the factions set in the config are able to be joined")]
        [Permission(MyPromoteLevel.Admin)]
        public void FactionCheck()
        {
            // Work out which faction to assign player (one with least amount of players)
            var pairs = new List<KeyValuePair<string, bool>>();

            foreach (string Tag in Plugin.Config.FactionTags)
            {
                try
                {
                    IMyFaction tempFaction = MyAPIGateway.Session.Factions.TryGetFactionByTag(Tag);

                    var pair = new KeyValuePair<string, bool>(Tag, tempFaction.AutoAcceptMember);

                    pairs.Add(pair);
                }
                catch(Exception e)
                {
                    Context.Respond("Faction :: " + Tag + " :: doesn't exist!");
                }

            }

            foreach(var pair in pairs)
            {
                if (pair.Value)
                    Context.Respond(pair.Key + " :: READY");
                else
                    Context.Respond(pair.Key + " :: NOT READY");

            }
        }

        [Command("add","add new faction to list of assignable factions")]
        [Permission(MyPromoteLevel.Admin)]
        public void FactionAdd(string Tag)
        {
            try
            {
                IMyFaction tempFaction = MyAPIGateway.Session.Factions.TryGetFactionByTag(Tag);
                Plugin.Config.FactionTags.Add(Tag);
                Plugin.Save();
                Context.Respond("Faction added");
            }
            catch(Exception e)
            {
                Context.Respond("Faction :: " + Tag + " :: doesn't exist!");
            }
        }

        [Command("remove","remove a faction from the list of assignable factions")]
        [Permission(MyPromoteLevel.Admin)]
        public void FactionRemove(string Tag)
        {
            try
            {
                IMyFaction tempFaction = MyAPIGateway.Session.Factions.TryGetFactionByTag(Tag);
                Plugin.Config.FactionTags.Remove(Tag);
                Plugin.Save();
                Context.Respond("Faction removed");
            }
            catch(Exception e)
            {
                Context.Respond("Faction :: " + Tag + " :: doesn't exist!");
            }
        }

    }
}
