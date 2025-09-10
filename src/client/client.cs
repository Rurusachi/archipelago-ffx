using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.Enums;
using Archipelago.MultiClient.Net.Helpers;
using Archipelago.MultiClient.Net.MessageLog.Messages;
using Archipelago.MultiClient.Net.MessageLog.Parts;
using Archipelago.MultiClient.Net.Models;
using Fahrenheit.Core;
using Fahrenheit.Core.FFX;
using Fahrenheit.Modules.ArchipelagoFFX.GUI;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static Fahrenheit.Modules.ArchipelagoFFX.delegates;

namespace Fahrenheit.Modules.ArchipelagoFFX.Client;
public class ArchipelagoClient {
    public  static          ArchipelagoSession? session;
    private static          int                 received_items = 0;
    public  static readonly List<long>          local_checked_locations = [];
    public  static          bool                local_locations_updated = false;
    public ArchipelagoClient() {
        
    }
    
    public static PlayerInfo? active_player => session?.Players.ActivePlayer;
    public static bool is_connected => session != null;

    public static void Connect(string server, string user, string password) {
        LoginResult login_result;

        try {
            session = ArchipelagoSessionFactory.CreateSession(server);
            connectHandlers();
            login_result = session.TryConnectAndLogin("Final Fantasy X", user, ItemsHandlingFlags.IncludeStartingInventory, Version.Parse("0.5.1"), null, null, password, true);
        }
        catch (Exception e) {
            login_result = new LoginFailure(e.GetBaseException().Message);
        }

        if (!login_result.Successful) {
            LoginFailure failure = (LoginFailure)login_result;
            string errorMessage = $"Failed to Connect to {server} as {user}:";
            foreach (string error in failure.Errors) {
                errorMessage += $"\n    {error}";
            }
            foreach (ConnectionRefusedError error in failure.ErrorCodes) {
                errorMessage += $"\n    {error}";
            }
            session = null;
            ArchipelagoFFXModule.logger.Error(errorMessage);
            return; // Did not connect, show the user the contents of `errorMessage`
        }
        var loginSuccess = (LoginSuccessful)login_result;

        // TODO: Get region states if they exist. Probably separate Key for each region to minimize network traffic?
        //session.DataStorage[Scope.Slot, "region_states"];
    }

    private static void connectHandlers() {
        session!.MessageLog.OnMessageReceived += MessageLog_OnMessageReceived;
    }

    public unsafe static void update() {
        /*
        foreach (ItemInfo item in session.Items.AllItemsReceived) {
            ArchipelagoFFXModule.logger.Debug($"received_item: {item.ItemName}");
        }
         */
        if (session == null) {
            return;
        }

        if (Globals.save_data->current_room_id == 23) {
            return;
        }

        if (session.Items.AllItemsReceived.Count > received_items) {
            ArchipelagoFFXModule.logger.Debug("New items received");
            foreach (ItemInfo item in session.Items.AllItemsReceived.Skip(received_items)) {
                ArchipelagoFFXModule.logger.Debug($"received_item: {item.ItemName}");
                ArchipelagoFFXModule.obtain_item((uint)item.ItemId, 1);
                received_items++;
            }
        }


        if (local_locations_updated) {
            var local_only = local_checked_locations.Except(session.Locations.AllLocationsChecked);
            if (local_only.Any()) {
                session.Locations.CompleteLocationChecks(local_only.ToArray());
                ArchipelagoFFXModule.logger.Debug($"Sent: {string.Join(",", local_only)}");
            }
            local_locations_updated = false;
        }

        /*
        session.DataStorage[Scope.Slot, "region_states"] = JObject.FromObject(ArchipelagoModule.region_states);

        var remote_state_json = session.DataStorage[Scope.Slot, "region_states"].To<JObject>();
        if (remote_state_json != null) {
            Dictionary<string, ArchipelagoData.ArchipelagoRegion> remote_state = remote_state_json.ToObject<Dictionary<string, ArchipelagoData.ArchipelagoRegion>>();
        }
         */

        /*
        var remote_only = session.Locations.AllLocationsChecked.Except(local_checked_locations);
        if (remote_only.Any()) {
            ArchipelagoFFXModule.logger.Debug($"Received Remote Locations: {string.Join(",", remote_only)}");
            foreach (var location in remote_only) {
                // TODO: Distinguish between location types
                var treasure_id = (location - 0x14)/4;
                ArchipelagoFFXModule.logger.Debug($"Location id: {Convert.ToInt32(treasure_id)}");
                ArchipelagoModule.receive_treasure(Convert.ToInt32(treasure_id));
                local_checked_locations.Add(location);
            }
        }
         */


    }

    private static void MessageLog_OnMessageReceived(LogMessage message) {
        var parts = message.Parts;
        List<(string, Color)> messageParts = parts.Select((part) => {
            Color color = part.Color;
            if (part.IsBackgroundColor) {
                color = Color.White;
            }
            else if (part.Color == Color.Black) {
                color = new(128, 128, 128);
            }
            return (part.Text, color);
            }).ToList();
        ArchipelagoGUI.client_log.Add(messageParts);
        ArchipelagoGUI.client_log_updated = true;
    }

    /*
public unsafe static void connectHandlers() {
   session.Locations.CheckedLocationsUpdated += (newCheckedLocations) => {
       if (Globals.save_data->current_room_id == 23) return;

       var remote_only = newCheckedLocations.Except(local_checked_locations);
       if (remote_only.Any()) {
           ArchipelagoFFXModule.logger.Debug($"Received Remote Locations: {string.Join(",", remote_only)}");
           foreach (var location in remote_only) {
               ArchipelagoFFXModule.logger.Debug($"Location id: {Convert.ToInt32(location)}");
               ArchipelagoModule.receive_treasure(Convert.ToInt32(location));
               local_checked_locations.Add(location);
           }
       }
   };
}
*/

    public enum ArchipelagoLocationType: int {
        Treasure = 0x1000,
        Boss = 0x2000,
        PartyMember = 0x3000,
        Overdrive = 0x4000,
        OverdriveMode = 0x5000,
        Other = 0x6000,
        SphereGrid = 0x7000,
    }

    public static void sendTreasureLocation(long treasureId) {
        //var locationId = 0x14 + treasureId * 4;
        long locationId = treasureId | (long)ArchipelagoLocationType.Treasure;
        ArchipelagoGUI.lastTreasure = treasureId;
        sendLocation(locationId, 0);
    }

    public static void sendLocation(long locationId, ArchipelagoLocationType locationType) {
        var absoluteId = locationId | (long)locationType;
        sendLocation(absoluteId);
    }
    private static void sendLocation(long locationId) {
        local_checked_locations.Add(locationId);
        local_locations_updated = true;
        if (is_connected) {
            ArchipelagoFFXModule.logger.Debug(session!.Locations.GetLocationNameFromId(locationId) ?? $"Location: {locationId}");
        }
    }

}

