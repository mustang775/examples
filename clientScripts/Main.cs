using RAGE;
using RAGE.NUI;
using RAGE.Elements;

namespace ClientSide
{
    public class Main : Events.Script
    {
        public Main()
        {
            #region Events
            Events.Tick += TickVoid;
            Events.Add("Discord", Discord);
            Events.Add("SERVER:CLIENT:LSCNative", LSCNative);
            Events.Add("START", START);
            Events.Add("IPLLOADER", IPLLOADER);
            #endregion Events

            #region Binds
            RAGE.Input.Bind(RAGE.Ui.VirtualKeys.F10, true, () =>
            {
                if (Player.LocalPlayer._GetSharedData<bool>("Admin"))
                {
                    Events.CallLocal("DisableCEF", false);
                    Events.CallRemote("AHELPSTART");
                    return;
                }
            });
            RAGE.Input.Bind(RAGE.Ui.VirtualKeys.F6, true, () =>
            {
                if(RAGE.Game.Ui.IsRadarHidden() == false)
                {
                    RAGE.Game.Ui.DisplayRadar(false);
                }
                else
                {
                    RAGE.Game.Ui.DisplayRadar(true);
                }
            });
            RAGE.Input.Bind(RAGE.Ui.VirtualKeys.F2, true, () =>
            {
                if(RAGE.Ui.Cursor.Visible == true)
                {
                    RAGE.Ui.Cursor.Visible = false;
                }
                else
                {
                    RAGE.Ui.Cursor.Visible = true;
                }
            });
#endregion Binds
        }

        public void TickVoid(List<Events.TickNametagData> nametags)
        {
            var gun = Player.LocalPlayer.GetSelectedWeapon();
            if (gun != 0xA2719263)
            {
                RAGE.Game.Pad.DisableControlAction(32, 142, true); //отключение урона от приклада
            }

            RAGE.Game.Pad.DisableControlAction(32, 36, true); //отключение стелс-режима
            RAGE.Game.Pad.DisableControlAction(32, 140, true); 
            RAGE.Game.Pad.DisableControlAction(32, 141, true); 

            if (RAGE.Elements.Player.LocalPlayer._GetSharedData<bool>("GM"))
            {
                RAGE.Elements.Player.LocalPlayer.SetInvincible(true); //админ гм
            }
            else
            {
                RAGE.Elements.Player.LocalPlayer.SetInvincible(false); //админ гм(отключение)
            }

            if (RAGE.Elements.Player.LocalPlayer._GetSharedData<bool>("InZZ"))
            {
                //отключение биндов атаки в ЗЗ
                RAGE.Game.Pad.DisableControlAction(0, 24, true); 
                RAGE.Game.Pad.DisableControlAction(0, 257, true);
                RAGE.Game.Pad.DisableControlAction(0, 69, true);
                RAGE.Game.Pad.DisableControlAction(0, 70, true);
                RAGE.Game.Pad.DisableControlAction(0, 92, true);
            }
            else
            {
                //включение биндов атаки если не в ЗЗ
                RAGE.Game.Pad.EnableControlAction(0, 24, true);
                RAGE.Game.Pad.EnableControlAction(0, 257, true);
                RAGE.Game.Pad.EnableControlAction(0, 69, true);
                RAGE.Game.Pad.EnableControlAction(0, 70, true);
                RAGE.Game.Pad.EnableControlAction(0, 92, true);
            } 
        }
        private void Discord(object[] args)
        {
            RAGE.Discord.Update(args[0], "На My Server!");
        }
        private void IPLLOADER(object[] args)
        {
            //подгрузка IPL
            RAGE.Game.Streaming.RequestIpl("ex_sm_15_office_02b");
            RAGE.Game.Streaming.RequestIpl("ex_dt1_11_office_02c");
            RAGE.Game.Streaming.RequestIpl("apa_v_mp_h_01_a");
            RAGE.Game.Streaming.RequestIpl("apa_v_mp_h_02_c");
            RAGE.Game.Streaming.RequestIpl("apa_v_mp_h_03_b");
            RAGE.Game.Streaming.RequestIpl("apa_v_mp_h_04_a");
            RAGE.Game.Streaming.RequestIpl("apa_v_mp_h_05_c");
            RAGE.Game.Streaming.RequestIpl("apa_v_mp_h_06_b");
            RAGE.Game.Streaming.RequestIpl("apa_v_mp_h_07_a");
            RAGE.Game.Streaming.RequestIpl("apa_v_mp_h_08_c");
            RAGE.Game.Streaming.RequestIpl("bkr_biker_interior_placement_interior_0_biker_dlc_int_01_milo");
            RAGE.Game.Streaming.RequestIpl("bkr_bi_hw1_13_int");

            RAGE.Game.Streaming.RemoveIpl("vw_dlc_casino_door");
            RAGE.Game.Streaming.RequestIpl("vw_casino_carpark");
            RAGE.Game.Streaming.RequestIpl("vw_dlc_casino_carpark");
            RAGE.Game.Streaming.RequestIpl("vw_casino_garage");
            RAGE.Game.Streaming.RequestIpl("vw_dlc_casino_garage");
            RAGE.Game.Streaming.RequestIpl("vw_casino_main");
            RAGE.Game.Streaming.RequestIpl("vw_casino_penthouse");
            RAGE.Game.Streaming.RequestIpl("vw_dlc_casino_apart");
            RAGE.Game.Streaming.RequestIpl("ch_dlc_casino_hotel");
            RAGE.Game.Streaming.RequestIpl("hei_dlc_windows_casino");
            RAGE.Game.Streaming.RequestIpl("hei_dlc_vw_roofdoors_locked");
            RAGE.Game.Streaming.RequestIpl("vw_dlc_casino_main");
            RAGE.Game.Streaming.RequestIpl("vw_int_placement_vw_interior_0_dlc_casino_main_milo_");
            RAGE.Game.Streaming.RequestIpl("hei_dlc_casino_aircon");
            RAGE.Game.Streaming.RequestIpl("hei_vw_dlc_casino_door_replay");
            RAGE.Game.Streaming.RequestIpl("ch_h3_casino_cameras");
            RAGE.Game.Streaming.RequestIpl("vw_casino_billboard");
            RAGE.Game.Streaming.RequestIpl("ch_int_placement_ch_interior_9_dlc_casino_shaft_milo_");
            RAGE.Game.Streaming.RequestIpl("ch_dlc_casino_shaft");
            RAGE.Game.Streaming.RequestIpl("ch_int_placement_ch_interior_7_dlc_casino_utility_milo_");
            RAGE.Game.Streaming.RequestIpl("ch_int_placement_ch_interior_7_dlc_casino_utility_milo_");
            RAGE.Game.Streaming.RequestIpl("ch_dlc_casino_utility");
            RAGE.Game.Streaming.RequestIpl("ch_int_placement_ch_interior_6_dlc_casino_vault_milo_");
            RAGE.Game.Streaming.RequestIpl("ch_dlc_casino_vault");
            RAGE.Game.Streaming.RequestIpl("ch_int_placement_ch_interior_5_dlc_casino_loading_milo_");
            RAGE.Game.Streaming.RequestIpl("ch_dlc_casino_loading");
            RAGE.Game.Streaming.RequestIpl("ch_dlc_casino_hotel");
            RAGE.Game.Streaming.RequestIpl("ch_int_placement_ch_interior_3_dlc_casino_back_milo_");
            RAGE.Game.Streaming.RequestIpl("ch_dlc_casino_back");
            RAGE.Game.Streaming.RequestIpl("ch_int_placement_ch_interior_0_dlc_casino_heist_milo_");
            RAGE.Game.Streaming.RequestIpl("ch_dlc_casino_heist");

            RAGE.Game.Streaming.RequestIpl("smboat");
            RAGE.Game.Streaming.RequestIpl("ch_int_placement_ch_interior_1_dlc_arcade_milo_");
            RAGE.Game.Streaming.RemoveIpl("garage_door_locked");
            RAGE.Game.Streaming.RequestIpl("hei_bi_hw1_13_door");
            RAGE.Game.Streaming.RequestIpl("ex_exec_warehouse_placement_interior_0_int_warehouse_m_dlc_milo_");
            RAGE.Game.Streaming.RequestIpl("ex_exec_warehouse_placement_interior_1_int_warehouse_s_dlc_milo_");
            RAGE.Game.Streaming.RequestIpl("sf_dlc_fixer_hanger_door");
            RAGE.Game.Streaming.RequestIpl("sf_fixeroffice_hw1_08");
            RAGE.Game.Streaming.RequestIpl("sf_yacht_01_int");
        }

        private void LSCNative(object[] args)
        {
            Events.CallLocal("DisableCEF", false);
            Chat.Show(false);
            RAGE.Ui.Cursor.Visible = true;
            RAGE.Ui.Cursor.Visible = false;

            MenuPool mPool = new MenuPool();

            UIMenu LSCMenu = new UIMenu("Los-Santos Customs", "Модернизируйте своё авто на ваш вкус!");

            mPool.Add(LSCMenu);

            UIMenuItem RepairButton = new UIMenuItem("Починить!");
            LSCMenu.AddItem(RepairButton);


            RepairButton.Activated += (sender, item) =>
            {
                if (sender == LSCMenu)
                {
                    if (item == RepairButton)
                    {
                        Events.CallLocal("DisableCEF", true);
                        Events.CallRemote("SERVER:CLIENT:LSCRepair");
                        Chat.Show(true);
                        LSCMenu.FreezeAllInput = false;
                        RAGE.Ui.Cursor.Visible = false;
                        LSCMenu.Visible = false;
                    }
                }
            };

            LSCMenu.Visible = true;
            LSCMenu.FreezeAllInput = true;

            LSCMenu.RefreshIndex();

            Events.Tick += (name) =>
            {
                mPool.ProcessMenus();
            };

            LSCMenu.OnMenuClose += (sender) =>
            {
                if (sender == LSCMenu)
                {
                    Events.CallLocal("DisableCEF", true);
                    Chat.Show(true);
                    LSCMenu.FreezeAllInput = false;
                    RAGE.Ui.Cursor.Visible = false;
                    LSCMenu.Visible = false;
                }
            };
        }

    }
}
