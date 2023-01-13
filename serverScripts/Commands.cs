using GTANetworkAPI;

namespace ServerSide
{
    public class Commands : Script
    {
        Random rndm = new Random();

        [Command("veh", "/veh hash color1 color2", Alias = "vehicle")]
        private void Cmd_veh(Player player, string vehname, int color1 = 13, int color2 = 13)
        {
            try
            {
                Accounts account = player.GetData<Accounts>(Accounts._accountKey);
                if (account == null) return;

                if (!account.IsPlayerHasAdminLevel((int)Accounts.AdminRanks.Administrator))
                {
                    player.SendNotification("~r~У вас нет доступа к этой команде");
                    return;
                }
                uint vhash = NAPI.Util.GetHashKey(vehname);
                Vehicle veh = NAPI.Vehicle.CreateVehicle(vhash, player.Position, player.Heading, color1, color2);
                veh.NumberPlate = "Legion";
                veh.Locked = false;
                veh.EngineStatus = true;
                player.SetIntoVehicle(veh, (int)VehicleSeat.Driver);
                player.SendNotification("~g~Вы успешно заспавнили: " + vehname);
                Logs(2, $"Администратор {player.Name} {account._adminlevel} LVL", $"Использовал команду /veh и создал: {vehname}");
            }
            catch
            {

            }
        }

        [Command("hp", Alias = "health")]
        private void SetHealth(Player player, int id, int count)
        {
            try
            {
                Accounts account = player.GetData<Accounts>(Accounts._accountKey);
                if (!account.IsPlayerHasAdminLevel((int)Accounts.AdminRanks.Helper))
                {
                    player.SendNotification("~r~У вас нет доступа к этой команде");
                    return;
                }
                Player target = Utils.GetPlayerObject(id);
                if (target == null || !Accounts.IsPlayerLoggedIn(target))
                {
                    player.SendNotification("~r~Игрок не найден");
                    return;
                }
                if (count > 100)
                {
                    player.SendNotification("~r~Количество здоровья должно быть не больше 100");
                    return;
                }

                NAPI.Player.SetPlayerHealth(target, count);
                player.SendNotification("~g~Вы успешно выдали " + count + "HP игроку " + target.Name + "!");
                Logs(2, $"Администратор {player.Name} {account._adminlevel} LVL", $"Использовал команду /hp и выдал hp: {count} игроку {target.Name}");
            }
            catch
            {

            }
        }

        [Command("teleportxyz", Alias = "tpxyz")]
        private void TeleportPlayer(Player player, float x, float y, float z)
        {
            try
            {
                Accounts account = player.GetData<Accounts>(Accounts._accountKey);
                if (!account.IsPlayerHasAdminLevel((int)Accounts.AdminRanks.Helper))
                {
                    player.SendNotification("~r~У вас нет доступа к этой команде");
                    return;
                }
                Utils.AntiCheatDeactivator(player);
                player.TriggerEvent("disableNoclip");
                player.Position = new Vector3(x, y, z);
                player.SendNotification("~g~Вы успешно телепортировались!");
                Logs(2, $"Администратор {player.Name} {account._adminlevel} LVL", $"Телепортировался, используя команду /tpxyz");
            }
            catch
            {

            }
        }
    }
}