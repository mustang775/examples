using System;
using RAGE;
using RAGE.Elements;

namespace ClientSide
{
    public class AntiCheat : Events.Script
    {
        private const float _maxSpeed = 90f;
        static List<uint> weapons = new List<uint>()
        {
            0x476BF155,
            //and many others
        };
        static List<uint> cars = new List<uint>()
        {
            0x34B82784,
            //and many others
        };
        public AntiCheat()
        {
            Events.OnPlayerEnterVehicle += PlayerEnterVehice;
            Events.Add("StartACheat", StartACheat);
            Events.OnPlayerWeaponShot += PlayerShot;
        }

        private void PlayerShot(Vector3 targetPos, Player target, Events.CancelEventArgs cancel)
        {
            var weaponHash = target.GetSelectedWeapon();
            foreach (uint weapon in weapons)
            {
                if(weaponHash == weapon)
                {
                    Events.CallRemote("AntiCheat:Detecting", "Gun");
                    cancel.Cancel = true;
                    return;
                }
            }
        }

        public void StartACheat(object[] args)
        {
            CheckTeleport();
            CheckInvisible();
            CheckSpeed();
        }

        private void CheckSpeed()
        {
            if (Player.LocalPlayer.Vehicle == null)
            {
                Task.Run(() =>
                {
                    CheckSpeed();
                    return;
                }, delayTime: 2000); // 2 секунды
            }
            else
            {
                Task.Run(() =>
                {
                    var getSpeed = Player.LocalPlayer.Vehicle.GetSpeed();
                    if (getSpeed >= _maxSpeed)
                    {
                        Events.CallRemote("AntiCheat:Detecting", "Speed");
                    }
                    CheckSpeed();
                }, delayTime: 2000); // 2 секунды
            }
        }

        public void CheckTeleport()
        {
            var playerPos = Player.LocalPlayer.Position;
            Task.Run(() =>
            {
                var newPosZ = playerPos.Z + 100;
                var newPosX = playerPos.X + 100;
                var newPosY = playerPos.Y + 100;
                var newPositionZ = playerPos.Z - 100;
                var newPositionX = playerPos.X - 100;
                var newPositionY = playerPos.Y - 100;
                if(!Player.LocalPlayer._GetSharedData<bool>("Anti-Cheat:deactivated"))
                {
                    if (Player.LocalPlayer.Position.X > newPosX || Player.LocalPlayer.Position.Y > newPosY || Player.LocalPlayer.Position.Z > newPosZ || Player.LocalPlayer.Position.X < newPositionX || Player.LocalPlayer.Position.Z < newPositionZ || Player.LocalPlayer.Position.Y < newPositionY)
                    {
                        Events.CallRemote("AntiCheat:Detecting", "Teleport");
                    }
                }
                CheckTeleport();
            }, delayTime: 1000); // 1 секунд

            
        }

        public void PlayerEnterVehice(Vehicle vehicle, int seatId)
        {
            var veh = Player.LocalPlayer.Vehicle.Model;
            foreach (uint car in cars)
            {
                if(veh == car)
                {
                    Events.CallRemote("AntiCheat:Detecting", "Car");
                    cancel.Cancel = true;
                    return;
                }
            }
        
        }
        public void CheckInvisible()
        {
            var playerPos = Player.LocalPlayer.Position;
            Task.Run(() =>
            {
                if(Player.LocalPlayer.GetAlpha() != 255 && !Player.LocalPlayer._GetSharedData<bool>("Admin"))
                {
                    Events.CallRemote("AntiCheat:Detecting", "Invisible");
                }
                CheckInvisible();
            }, delayTime: 1000); // 1 секунд
        }
    }
}
