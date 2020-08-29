using ColossalFramework.PlatformServices;
using Insights.Logging;

namespace Insights.Game.Extensions
{
    /// <remarks>
    /// Some of these events are unlikely to apply to this game. However, handlers
    /// are configured for each one in case the functionality is implemented.
    /// </remarks>
    public class PlatformServiceHandler
    {
        protected InsightsLogger Logger { get; } = new InsightsLogger(typeof(PlatformServiceHandler));

        public void Subscribe()
        {
            PlatformService.eventAuthTicketReceived += AuthTicketReceived;
            PlatformService.eventGameLobbyJoinRequested += GameLobbyJoinRequested;
            PlatformService.eventGameOverlayActivated += GameOverlayActivated;
            PlatformService.eventGameServerChangeRequested += GameServerChangeRequested;
            PlatformService.eventPersonaStateChange += PersonaStateChange;
            PlatformService.eventPlatformServiceInit += PlatformServiceInit;
            PlatformService.eventPlatformServiceShutdown += PlatformServiceShutdown;
            PlatformService.eventSteamControllerInit += SteamControllerInit;
            PlatformService.eventSteamGamepadInputDismissed += SteamGamepadInputDismissed;
        }

        public void Unsubscribe()
        {
            PlatformService.eventAuthTicketReceived -= AuthTicketReceived;
            PlatformService.eventGameLobbyJoinRequested -= GameLobbyJoinRequested;
            PlatformService.eventGameOverlayActivated -= GameOverlayActivated;
            PlatformService.eventGameServerChangeRequested -= GameServerChangeRequested;
            PlatformService.eventPersonaStateChange -= PersonaStateChange;
            PlatformService.eventPlatformServiceInit -= PlatformServiceInit;
            PlatformService.eventPlatformServiceShutdown -= PlatformServiceShutdown;
            PlatformService.eventSteamControllerInit -= SteamControllerInit;
            PlatformService.eventSteamGamepadInputDismissed -= SteamGamepadInputDismissed;
        }

        private void AuthTicketReceived(Result result, string ticket, uint handle)
        {
            Logger.LogDebug($"AuthTicketReceived > Result: {result} | Ticket: {ticket} | Handle: {handle}");
        }

        private void GameLobbyJoinRequested(ulong lobbyID, ulong friendInviteID)
        {
            Logger.LogDebug($"GameLobbyJoinRequested > Lobby: {lobbyID} | Invite: {friendInviteID}");
        }

        private void GameOverlayActivated(bool active)
        {
            Logger.LogDebug($"GameOverlayActivated > Active: {active}");
        }

        private void GameServerChangeRequested(string server, string password)
        {
            Logger.LogDebug($"GameServerChangeRequested > Server: {server}");
        }

        /// <remarks>
        /// This can generate a large number of events when joining a group chat.
        /// </remarks>
        private void PersonaStateChange(UserID id, PersonaChange flags)
        {
            Logger.LogDebug($"PersonaStateChange > UserId: {id.AsUInt64} | Change: {flags}");
        }

        private void PlatformServiceInit()
        {
            Logger.LogDebug("PlatformServiceInit");
        }

        private void PlatformServiceShutdown()
        {
            Logger.LogDebug("PlatformServiceShutdown");
        }

        private void SteamControllerInit()
        {
            Logger.LogDebug("SteamControllerInit");
        }

        private void SteamGamepadInputDismissed(string text)
        {
            Logger.LogDebug($"SteamGamepadInputDismissed > Text: {text}");
        }
    }
}
