using System;
using System.Collections.Generic;
using System.Text;
using RAGE;
using RAGE.Elements;

namespace ClientSide
{
    public class GunDamage : Events.Script
    {
        static List<UInt64> explosions = new List<UInt64>()
        {
            0, 1, 2, 3, 4, 5, 19, 20, 32, 33, 36, 44, 45, 49, 50, 62, 61, 64, 65, 66, 67, 68, 70 
        };
        public GunDamage()
        {
            Events.OnIncomingDamage += OnIncomingDamage;
            Events.OnOutgoingDamage += OutgoingDamage;
            Events.OnExplosion += OnExplosion;
        }

        private void OnExplosion(Player sourcePlayer, uint explosionType, Vector3 position, Events.CancelEventArgs cancel)
        {
            if(explosions.Contains(explosionType))
            {
                sourcePlayer.SetInvincible(true);
                cancel.Cancel = true;
                sourcePlayer.SetHealth(100);
                return;
            }
        }

        private void OnIncomingDamage(Player sourcePlayer, Entity sourceEntity, Entity targetEntity, ulong weaponHash, ulong boneIdx, int damage, Events.CancelEventArgs cancel)
        {
            var player = Player.LocalPlayer;
            var realbone = damage;
            
                if (weaponHash == 0x3AABBBAA) // сайга
                {
                    var playerCoords = Player.LocalPlayer.Position;
                    var killerCoords = sourcePlayer.Position;
                    var distance = RAGE.Vector3.Distance(killerCoords, playerCoords);
                    if (Player.LocalPlayer.GetHealth()! > 10)
                    {
                        cancel.Cancel = true;
                    }
                    if (realbone == 20) // голова
                    {
                        if(distance > 11)
                        {
                            Player.LocalPlayer.ApplyDamageTo(8, true);
                        }
                        if(distance > 5)
                        {
                            Player.LocalPlayer.ApplyDamageTo(17, true);
                        }
                        if(distance > 3)
                        {
                            Player.LocalPlayer.ApplyDamageTo(30, true);
                        }
                    }
                    if (realbone == 5 || realbone == 6) // ноги и стопы
                    {
                        if (distance < 30)
                        {
                            Player.LocalPlayer.ApplyDamageTo(4, true);
                        }
                        if (distance < 12.5)
                        {
                            Player.LocalPlayer.ApplyDamageTo(9, true);
                        }
                        if (distance < 6)
                        {
                            Player.LocalPlayer.ApplyDamageTo(16, true);
                        }
                    }
                    if (realbone != 5 && realbone != 20 && realbone != 6) // другое(тело)
                    {
                        if (distance < 30)
                        {
                            Player.LocalPlayer.ApplyDamageTo(6, true);
                        }
                        if (distance < 12.5)
                        {
                            Player.LocalPlayer.ApplyDamageTo(15, true);
                        }
                        if (distance < 5)
                        {
                            Player.LocalPlayer.ApplyDamageTo(25, true);
                        }
                    }
                }

        }
    }
}
        
