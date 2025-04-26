using Archipelago.MultiClient.Net.Models;
using Fahrenheit.Core;
using Fahrenheit.Core.FFX;
using Fahrenheit.Core.FFX.Atel;
using Fahrenheit.Core.FFX.Battle;
using Fahrenheit.Core.FFX.Ids;
using Fahrenheit.Modules.ArchipelagoFFX.Client;
using Fahrenheit.Core.ImGuiNET;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using static Fahrenheit.Modules.ArchipelagoFFX.ArchipelagoData;
using Color = Archipelago.MultiClient.Net.Models.Color;
using static Fahrenheit.Modules.ArchipelagoFFX.delegates;

namespace Fahrenheit.Modules.ArchipelagoFFX.GUI;
public unsafe static class ArchipelagoGUI {
    public delegate void OnRenderDelegate();

    public static bool enabled = false;
    private static bool show = true;

    public static long lastTreasure = -1;

    private static string client_input_address = "";
    private static string client_input_name = "";
    private static string client_input_password = "";

    private static string client_input_command = "";

    public static List<List<(string text, Color color)>> client_log = [];
    public static bool client_log_updated = false;
    private static float previous_scroll = 1;
    private static float previous_scroll_max = 1;


    private static readonly Vector2 PANE_BUTTON_SIZE = new Vector2(16f);

    private static int grav_mode = 1;
    private static int field_mode = 0;
    private static int motion_type = 0;

    private static int character_model = 0;

    private static int[] MsBtlGetPosParams = [0, 0, 0];
    private static Vector4 MsBtlGetPosResult = new(0, 0, 0, 0);
    private static uint LaunchBattleInput = 0;

    public static void render() {
        //ImGui.ShowDebugLogWindow();
        //ImGui.ShowStyleEditor();
        enabled ^= ImGui.IsKeyPressed(ImGuiKey.F7);
        if (!enabled) return;


        // Setup windows style
        float pane_width = ImGui.GetIO().DisplaySize.X * 0.4f;
        //ImGui.PushStyleColor(ImGuiCol.WindowBg, new Vector4 { X = 0.5f, Y = 0.5f, Z = 0.5f });
        //ImGui.PushStyleVar(ImGuiStyleVar.WindowRounding, 0f);
        //ImGui.PushStyleVar(ImGuiStyleVar.WindowBorderSize, 0f);
        
        render_client();
        //render_pane(pane_width);

        // Reset style
        //ImGui.PopStyleVar(2);
        //ImGui.PopStyleColor();
    }

    private static void render_client() {
        ImGuiStylePtr style = ImGui.GetStyle();

        ImGui.Begin("Archipelago###Archipelago.GUI");

        if (!ArchipelagoClient.is_connected) {
            ImGui.InputText("Address", ref client_input_address, 50);
            ImGui.InputText("Name", ref client_input_name, 50);
            ImGui.InputText("Password", ref client_input_password, 50);
            bool try_connect = ImGui.Button("Connect");
            if (try_connect) {
                ArchipelagoClient.Connect(client_input_address, client_input_name, client_input_password);
            }
        } else {
            ImGui.Text($"Connected as {ArchipelagoClient.active_player!.Name}");
        }
        fixed (uint* ap_mult = &ArchipelagoFFXModule.ap_multiplier) {
            uint step = 1;
            uint step_fast = 10;
            ImGui.InputScalar("AP multiplier", ImGuiDataType.U32, (nint)ap_mult, (nint)(&step), (nint)(&step_fast));
        }

        ImGui.Text($"Current room: {Globals.save_data->current_room_id} ({Marshal.PtrToStringAnsi((isize)ArchipelagoFFXModule.get_event_name(*(uint*)Globals.event_id))!})");
        ImGui.Text($"Current region: {ArchipelagoFFXModule.current_region}");
        ImGui.Text($"Current story progress: {Globals.save_data->story_progress}");

        //ImGui.Text($"Battle State: {Globals.btl->battle_state}");
        if (Globals.btl->battle_state != 0) {
            //ImGui.Text($"Battle End Type: {Globals.btl->battle_end_type}");
            //ImGui.InputScalar("Battle State", ImGuiDataType.U8, (nint)(&Globals.btl->battle_state));
            //ImGui.InputScalar("Battle End Type", ImGuiDataType.U8, (nint)(&Globals.btl->battle_end_type));
            ImGui.Text($"Battle Name: {Marshal.PtrToStringAnsi((isize)FhUtil.ptr_at<char>(0xD2C25A))}");

            if (ImGui.InputInt3("MsBtlGetPosInput", ref MsBtlGetPosParams[0])) {
                fixed (Vector4* out_pos = &MsBtlGetPosResult)
                ArchipelagoFFXModule.h_MsBtlGetPos(0, &Globals.Battle.player_characters[Globals.btl->frontline[0]], MsBtlGetPosParams[0], MsBtlGetPosParams[1], MsBtlGetPosParams[2], out_pos);
            }
            ImGui.Text($"{MsBtlGetPosResult}");
        } else {
            fixed (uint* battle_input = &LaunchBattleInput) {
                uint p_step = 1;
                uint p_step_fast = 10;
                ImGui.InputScalar("launchBattleInput", ImGuiDataType.U32, (nint)battle_input, (nint)(&p_step), (nint)(&p_step_fast), "%x");
            }
            if (ImGui.Button("launchBattleButton")) {
                ArchipelagoFFXModule.h_MsBattleLabelExe(LaunchBattleInput, 1, 1);
            }
        }

        //ImGui.InputInt("IgnoreThisInput", ref ArchipelagoFFXModule.ignore_this);

        /*
        AtelBasicWorker* worker0 = Atel.controllers[0].worker(0);
        Chr * curr_chr = worker0->chr_handle;
        if (curr_chr != null) {
            //ImGui.InputScalar("grav_mode", ImGuiDataType.U8, (nint)(&curr_chr->grav_mode));
            //ImGui.InputScalar("field_mode", ImGuiDataType.U8, (nint)(&curr_chr->field_mode));
            //ImGui.InputScalar("motion_type", ImGuiDataType.U8, (nint)(&curr_chr->motion_type));

            bool load_character = ImGui.InputInt("character_model", ref character_model);

            if (load_character) {
                character_model = Math.Clamp(character_model, 0, 6);
                ArchipelagoModule.set_character_model((PlySaveId)character_model);
            }

            //ImGui.Text($"grav mode: {curr_chr->grav_mode}");
            //ImGui.Text($"field mode: {curr_chr->field_mode}");
            //ImGui.Text($"motion type: {curr_chr->motion_type}");
        }
         */

        string s = "Unlocked regions:";
        ImGui.SetCursorPosX((ImGui.GetWindowWidth() - ImGui.CalcTextSize(s).X) * 0.5f);
        ImGui.Text(s);
        int counter = 0;
        int length = ArchipelagoFFXModule.region_is_unlocked.Count;
        foreach (var (region, i) in ArchipelagoFFXModule.region_is_unlocked.Select((value, i) => (value, i))) {
            ArchipelagoFFXModule.region_is_unlocked[region.Key] ^= ImGui.Button($"{region.Key} unlocked: {region.Value}");
            if (counter < 3 && i < length-1) {
                ImGui.SameLine();
            }
            counter = ++counter % 4;
        }

        s = "Unlocked characters:";
        ImGui.SetCursorPosX((ImGui.GetWindowWidth() - ImGui.CalcTextSize(s).X) * 0.5f);
        ImGui.Text(s);
        counter = 0;
        length = ArchipelagoFFXModule.character_is_unlocked.Count;
        foreach (var (character, i) in ArchipelagoFFXModule.character_is_unlocked.Select((value, i) => (value, i))) {
            ArchipelagoFFXModule.character_is_unlocked[character.Key] ^= ImGui.Button($"{character.Key} unlocked: {character.Value}");
            if (counter < 3 && i < length-1) {
                ImGui.SameLine();
            }
            counter = ++counter % 4;
        }


        ImGui.BeginChild("Archipelago.GUI.Log", new(0, ImGui.GetContentRegionAvail().Y - ImGui.GetTextLineHeight() - 3 * style.ItemSpacing.Y), ImGuiChildFlags.Borders, ImGuiWindowFlags.NoMove);
        //var curr_scroll = ImGui.GetScrollY() / previous_scroll_max;
        foreach (var line in client_log) {
            byte part_counter = 0;
            int part_length = line.Count;
            //ImGui.PushStyleVar(ImGuiStyleVar.ItemSpacing, new Vector2(0,style.ItemSpacing.Y));
            //ImGui.PushTextWrapPos(ImGui.GetCursorPosX() + ImGui.GetWindowWidth() - style.WindowPadding.X);
            var wrap_width = ImGui.GetContentRegionAvail().X;
            var remaining_width = wrap_width;
            foreach (var part in line) {
                var color = new Vector4(part.color.R / 255f, part.color.G / 255f, part.color.B / 255f, 1.0f);
                ImGui.PushStyleColor(ImGuiCol.Text, color);
                foreach (var word in part.text.Split(" ")) {
                    var word_width = ImGui.CalcTextSize($"{word} ").X;
                    if (part_counter > 0 && word_width < remaining_width) {
                        ImGui.SameLine();
                        ImGui.TextUnformatted(word);
                        remaining_width -= word_width;
                    } else {
                        ImGui.TextUnformatted(word);
                        remaining_width = wrap_width - word_width;
                    }
                    part_counter++;
                }
                ImGui.PopStyleColor();
                //ImGui.TextWrapped(line);
            }
            //ImGui.PopTextWrapPos();
            //ImGui.PopStyleVar();
        }
        //previous_scroll_max = ImGui.GetScrollMaxY();
        if (client_log_updated) {
            ImGui.SetScrollHereY();
            client_log_updated = false;
        } else {
            //ImGui.SetScrollY(curr_scroll * ImGui.GetScrollMaxY());
        }
        //ImGui.TextWrapped(string.Join("\n", client_log));
        ImGui.EndChild();

        
        bool process_input = ImGui.InputText("Input", ref client_input_command, 50, ImGuiInputTextFlags.EnterReturnsTrue);

        if (process_input) {
            if (!client_input_command.StartsWith("/")) {
                // Say
                if (ArchipelagoClient.is_connected) {
                    ArchipelagoClient.session!.Say(client_input_command);
                }

            } else {
                // Client-side command
                string[] cmd = client_input_command.Split(" ");

                Action fn = cmd switch {
                    ["/warp", string map, string entrance] => () => {
                        if (int.TryParse(map, out int map_id)) {
                            if (int.TryParse(entrance, out int entrance_id)) {
                                List<(string, Color)> message = [("Warping to ", Color.White), ($"{map_id} (entrance {entrance_id})", Color.Blue)];
                                client_log.Add(message);
                                ArchipelagoFFXModule.call_warp_to_map(map_id, entrance_id);
                            }
                            else {
                                List<(string, Color)> message = [("invalid entrance_id: ", Color.Red), (entrance, Color.Blue)];
                                client_log.Add(message);
                            }
                        }
                        else {
                            List<(string, Color)> message = [("invalid map_id: ", Color.Red), (map, Color.Blue)];
                            client_log.Add(message);
                        }
                    },
                    ["/warp", ..] => () => {
                        List<(string, Color)> message = [("Wrong arguments for /warp: Should be ", Color.Red), ($"/warp map_id entrance_id", Color.Blue)];
                        client_log.Add(message);
                    },
                    ["/resetregion", string regionString] => () => {
                        if (Enum.TryParse(regionString, out RegionEnum region)) {
                            ArchipelagoFFXModule.region_states[region] = region_starting_state[region];
                        } else {
                            List<(string, Color)> message = [("invalid region: ", Color.Red), (regionString, Color.Blue)];
                            client_log.Add(message);
                        }
                    },
                    ["/resetregion", ..] => () => {
                        List<(string, Color)> message = [("Wrong arguments for '/resetregion': Should be ", Color.Red), ($"/resetregion regionName", Color.Blue)];
                        client_log.Add(message);
                    }
                    ,
                    ["/setregion", string regionString, string progressString, string mapString, string entranceString] => () => {
                        if (Enum.TryParse(regionString, out RegionEnum region)) {
                            if (ushort.TryParse(progressString, out ushort progress)) {
                                if (ushort.TryParse(mapString, out ushort map)) {
                                    if (ushort.TryParse(entranceString, out ushort entrance)) {
                                        ArchipelagoRegion r = ArchipelagoFFXModule.region_states[region];
                                        r.story_progress = progress;
                                        r.room_id = map;
                                        r.entrance = entrance;
                                    } else {
                                        List<(string, Color)> message = [("invalid entrance_id: ", Color.Red), (entranceString, Color.Blue)];
                                        client_log.Add(message);
                                    }
                                }
                                else{
                                    List<(string, Color)> message = [("invalid map_id: ", Color.Red), (mapString, Color.Blue)];
                                    client_log.Add(message);
                                }

                            } else {
                                List<(string, Color)> message = [("invalid story_progress: ", Color.Red), (progressString, Color.Blue)];
                                client_log.Add(message);
                            }

                        }
                        else {
                            List<(string, Color)> message = [("invalid region: ", Color.Red), (regionString, Color.Blue)];
                            client_log.Add(message);
                        }
                    }
                    ,
                    ["/setregion", ..] => () => {
                        List<(string, Color)> message = [("Wrong arguments for '/setregion': Should be ", Color.Red), ($"/setregion regionName story_progress map_id entrance_id", Color.Blue)];
                        client_log.Add(message);
                    }
                    ,
                    ["/clear"] => () => {client_log.Clear(); },
                    _ => () => {
                        List<(string, Color)> message = [("unknown command: ", Color.Red), (client_input_command, Color.Blue)];
                        client_log.Add(message);
                    }
                };
                fn();
                client_log_updated = true;
            }
            client_input_command = "";
        }

        ImGui.End();

    }

}
