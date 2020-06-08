# Event Teams
Torch Plugin to auto assign factions for events, recommended use is on event servers.
Stores a list of the assignable factions in a List. Factions have to be set to accept everyone for this plugin to work
## Plugin Commands
1. !teams join
    * Permission: Everyone
    * Auto join a faction from a list of factions
2. !teams reload
    * Permission: Admin
    * Reloads the config file
3. !teams kick [PlayerName]
    * Permission: Admin
    * Kicks the specified player from their faction
4. !teams add [FactionTag]
    * Permission: Admin
    * Adds the faction with the specified tag to the list of assignable factions
5. !teams remove [FactionTag]
    * Permission: Admin
    * Removes the faction with the specified tag from the list of assignable factions
6. !teams empty [FactionTag]
    * Permission: Admin
    * Removes all members but the founder from the specified faction
7. !teams count
    * Permission: Admin
    * Provides a member count for each assignable faction 
