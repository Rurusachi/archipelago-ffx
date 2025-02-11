using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.Enums;
using Archipelago.MultiClient.Net.Helpers;
using Archipelago.MultiClient.Net.MessageLog.Messages;
using Archipelago.MultiClient.Net.MessageLog.Parts;
using Archipelago.MultiClient.Net.Models;
using Fahrenheit.Core;
using Fahrenheit.Core.FFX;
using Fahrenheit.Modules.Archipelago.GUI;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static Fahrenheit.Modules.Archipelago.delegates;

namespace Fahrenheit.Modules.Archipelago.Client;
public class ArchipelagoClient {
    public static ArchipelagoSession? session;
    private static int received_items = 0;
    private static List<long> local_checked_locations = new List<long>();
    private static bool local_locations_updated = false;
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
            FhLog.Error(errorMessage);
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
            FhLog.Debug($"received_item: {item.ItemName}");
        }
         */
        if (session == null) {
            return;
        }

        if (Globals.save_data->current_room_id == 23) {
            return;
        }

        if (session.Items.AllItemsReceived.Count > received_items) {
            FhLog.Debug("New items received");
            foreach (ItemInfo item in session.Items.AllItemsReceived.Skip(received_items)) {
                FhLog.Debug($"received_item: {item.ItemName}");
                /*
                if ((item.ItemId & 0xA000) == 0xA000) {
                    // Key Item
                    var bit_index = (int)(item.ItemId & 0xFF);
                    FhLog.Debug($"Received Key item: {item.ItemName} ({bit_index})");
                    if (bit_index < 32) {
                        Globals.save_data->ptr_important_bin.set_bit(bit_index, true);
                    } else {
                        (*(&Globals.save_data->ptr_important_bin + 1)).set_bit(bit_index - 32, true);
                    }
                } else if ((item.ItemId & 0x2000) == 0x2000){
                    // Normal Item
                    FhLog.Debug($"Received Normal item: {item.ItemName}");
                    for (int i = 0; i < 70; i++) {
                        var id = Globals.save_data->inventory_ids[i];
                        if (item.ItemId == id) {
                            Globals.save_data->inventory_counts[i] += 1;
                            break;
                        }
                        if (id == 255) {
                            Globals.save_data->inventory_ids[i] = ((ushort)item.ItemId);
                            Globals.save_data->inventory_counts[i] = 1;
                            break;
                        }
                    }
                } else if (item.ItemId == 0x1000) {
                    // 1000 Gil
                    FhLog.Debug($"Received 1000 gil");
                    Globals.save_data->gil += 1000;
                } else if ( item.ItemId <= 0x55) {
                    // Weapon
                    FhLog.Debug($"Received Weapon: {item.ItemName}");
                    // TODO: Implement weapon receiving
                } else {
                    FhLog.Error($"Received unhandled item: {item.ItemName} ({item.ItemId})");
                }
                 */
                ArchipelagoModule.obtain_item((uint)item.ItemId, 1);
                received_items++;
            }
        }


        if (local_locations_updated) {
            var local_only = local_checked_locations.Except(session.Locations.AllLocationsChecked);
            if (local_only.Any()) {
                session.Locations.CompleteLocationChecks(local_only.ToArray());
                FhLog.Debug($"Sent: {string.Join(",", local_only)}");
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
            FhLog.Debug($"Received Remote Locations: {string.Join(",", remote_only)}");
            foreach (var location in remote_only) {
                // TODO: Distinguish between location types
                var treasure_id = (location - 0x14)/4;
                FhLog.Debug($"Location id: {Convert.ToInt32(treasure_id)}");
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
           FhLog.Debug($"Received Remote Locations: {string.Join(",", remote_only)}");
           foreach (var location in remote_only) {
               FhLog.Debug($"Location id: {Convert.ToInt32(location)}");
               ArchipelagoModule.receive_treasure(Convert.ToInt32(location));
               local_checked_locations.Add(location);
           }
       }
   };
}
*/

    public static void sendTreasureLocation(long treasureId) {
        var locationId = 0x14 + treasureId * 4;
        ArchipelagoGUI.lastTreasure = treasureId;
        sendLocation(locationId);
    }

    public static void sendLocation(long locationId) {
        local_checked_locations.Add(locationId);
        local_locations_updated = true;
        if (is_connected) {
            FhLog.Debug(session!.Locations.GetLocationNameFromId(locationId) ?? $"Location: {locationId}");
        }
    }
    
}

