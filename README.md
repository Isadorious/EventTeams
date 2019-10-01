# Event Teams
Torch Plugin to auto assign factions for events, recommended use is on event servers.
Stores a list of the assignable factions in a List. Factions have to be set to accept everyone for this plugin to work
## Plugin Commands
1. !et join
    * Permission: Everyone
    * Auto join a faction from a list of factions
2. !et reload
    * Permission: Admin
    * Reloads the config file
3. !et kick [PlayerName]
    * Permission: Admin
    * Kicks the specified player from their faction
4. !et add [FactionTag]
    * Permission: Admin
    * Adds the faction with the specified tag to the list of assignable factions
5. !et remove [FactionTag]
    * Permission: Admin
    * Removes the faction with the specified tag from the list of assignable factions
6. !et empty [FactionTag]
    * Permission: Admin
    * Removes all members but the founder from the specified faction
7. !et pc
    * Permission: Admin
    * Provides a member count for each assignable faction 
