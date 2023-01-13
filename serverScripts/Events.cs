using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Threading;
using Newtonsoft.Json;
using System.Net;
using System.IO;

namespace ServerSide
{
    public class Events : Script
    {
        Random rndm = new Random();
        static List<string> badwords = new List<String>()
        {
            // 1. без капса 2. первая буква капс 3. все буквы капс 4. доп. запретки
            "мать", "матерь", "матери", "мама", "маме", "маму", "мамы", "папа", "папы", "папе", "батя", "бати", "бате", "сын", "сыновья", "сынок", "сыну", "сына", "сыновей", "гей", "гея", "геи", "гею", "лгбт", "лезби", "лезбиянки", "лезбиянка", "лезбиянке", "бля", "блять", "блядина", "блядь",
            "бляди", "сучёнок", "сученок", "шлюха", "шлюхи", "шлюхой", "шлюхе", "шлюха", "вагина", "вагину", "вагине", "сосать", "трахать", "дрочить", "кончать", "ебани", "ебану", "ниггер", "нигер", "нига", "nigger", "niga", "пиздун", "хуй",
            "ass", "dick", "deck", "dildack", "mother", "father", "mommy", "daddy", "даун", "дауну", "дауны", "сиська", "сиськи", "титьки", "дебил", "дебилу", "дебилы", "куколд", "хохол", "galaxy dm", "galaxy deathmatch", "dynamic deathmatch", "dynamic dm", "динамик дм", "украина", "украинец", "украинцам", "гелекси дм", "гэлекси дм", "украинцу", "донбас", "россия", "русский",
            "порно", "секс", "конча", "сперма", "cum", "cumming", "гомик", "гомосек", "негр", "negr", "хач", "хачей", "чурка", "чурки", "еврей", "дебилоид", "девственник", "девственик", "путин", "зеленский", "путина", "путину", "зеленскому", "зеленского", "анал", "anal", "putin", "zelenskiy",
            "Мать", "Матерь", "Матери", "Мама", "Маме", "Маму", "Мамы", "Папа", "Папы", "Папе", "Батя", "Бати", "Бате", "Сын", "Сыновья", "Сынок", "Сыну", "Сына", "Сыновей", "Гей", "Гея", "Геи", "Гею", "Лгбт", "Лезби", "Лезбиянки", "Лезбиянка", "Лезбиянке", "Бля", "Блять", "Блядина", "Блядь",
            "Бляди", "Сука", "Сучёнок", "Сученок", "Шлюха", "Шлюхи", "Шлюхой", "Шлюхе", "Шлюха", "Вагина", "Вагину", "Вагине", "Ебаный", "Ёбаный", "Ебать", "Сосать", "Трахать", "Дрочить", "Кончать", "Ебани", "Ебану", "Ниггер", "Нигер", "Нига", "Nigger", "Niga", "Пизда", "Пиздец", "Пиздун", "Похуй", "Хуй", "Поебать", "Нахуй", "Нахер",
            "Хер", "Ass", "Dick", "Deck", "Dildack", "Mother", "Father", "Mommy", "Daddy", "Даун", "Дауну", "Дауны", "Сиська", "Сиськи", "Титьки", "Дебил", "Дебилу", "Дебилы", "Куколд", "Хохол", "Galaxy dm", "Galaxy deathmatch", "Dynamic deathmatch", "Dynamic dm", "Динамик дм", "Украина", "Украинец", "Украинцам", "Гелекси дм", "гэлекси дм", "Украинцу", "Донбас", "Россия", "Русский",
            "Порно", "Секс", "Конча", "Сперма", "Cum", "Cumming", "Гомик", "Гомосек", "Негр", "Negr", "Хач", "Хачей", "Чурка", "Чурки", "Еврей", "Дебилоид", "Девственник", "Девственик", "Путин", "Зеленский", "Путина", "Путину", "Зеленскому", "Зеленского", "Анал", "Anal", "Putin", "Zelenskiy",
            "МАТЬ", "МАТЕРЬ", "МАТЕРИ", "МАМА", "МАМЕ", "МАМУ", "МАМЫ", "ПАПА", "ПАПЫ", "ПАПЕ", "БАТЯ", "БАТИ", "БАТЕ", "СЫН", "СЫНОВЬЯ", "СЫНОК", "СЫНУ", "СЫНА", "СЫНОВЕЙ", "ГЕЙ", "ГЕЯ", "ГЕИ", "ГЕЮ", "ЛГБТ", "ЛЕЗБИ", "ЛЕЗБИЯНКИ", "ЛЕЗБИЯНКА", "ЛЕЗБИЯНКЕ", "БЛЯ", "БЛЯТЬ", "БЛЯДИНА", "БЯЛДЬ",
            "БЛЯДИ", "СУКА", "СУЧЁНОК", "СУЧЕНОК", "ШЛЮХА", "ШЛЮХИ", "ШЛЮХОЙ", "ШЛЮХЕ", "ШЛЮХА", "ВАГИНА", "ВАГИНУ", "ВАГИНЕ", "ЕБАНЫЙ", "ЁБАНЫЙ", "ЕБАТЬ", "СОСАТЬ", "ТРАХАТЬ", "ДРОЧИТЬ", "КОНЧАТЬ", "ЕБАНИ", "ЕБАНУ", "НИГГЕР", "НИГЕР", "НИГА", "NIGGER", "NIGA", "ПИЗДА", "ПИЗДЕЦ", "ПИЗДУН", "ПОХУЙ", "ХУЙ", "ПОЕБАТЬ", "НАХУЙ", "НАХЕР",
            "ХЕР", "ASS", "DICK", "DECK", "DILDACK", "MOTHER", "FATHER", "MOMMY", "DADDY", "ДАУН", "ДАУНУ", "ДАУНЫ", "СИСЬКА", "СИСЬКИ", "ТИТЬКИ", "ДЕБИЛ", "ДЕБИЛУ", "ДЕБИЛЫ", "КУКОЛД", "ХОХОЛ", "GALAXY DM", "GALAXY DEATHMATCH", "DYNAMIC DEATHMATCH", "DYNAMIC DM", "ДИНАМИК ДМ", "УКРАИНА", "УКРАИНЕЦ", "УКРАИНЦАМ", "ГЕЛЕКСИ ДМ", "ГЭЛЕКСИ ДМ", "УКРАИНЦУ", "ДОНБАС", "РОССИЯ", "РУССКИЙ",
            "ПОРНО", "СЕКС", "КОНЧА", "СПЕРМА", "CUM", "CUMMING", "ГОМИК", "ГОМОСЕК", "НЕГР", "NEGR", "ХАЧ", "ХАЧЕЙ", "ЧУРКА", "ЧУРКИ", "ЕВРЕЙ", "ДЕБИЛОИД", "ДЕВСТВЕННИК", "ДЕВСТВЕНИК", "ПУТИН", "ЗЕЛЕНСКИЙ", "ПУТИНА", "ПУТИНУ", "ЗЕЛЕНСКОМУ", "ЗЕЛЕНСКОГО", "АНАЛ", "ANAL", "PUTIN", "ZELENSKIY",
            "Galaxy DM", "Dynamic DM",
        };

        public DateTime date = new DateTime();

       [ServerEvent(Event.PlayerDisconnected)]
       public void OnPlayerDisconnected(Player player, DisconnectionType type, string reason)
       {
            if (Utils.players.Contains(player)) Utils.players.Remove(player);
       }

        [ServerEvent(Event.PlayerConnected)]
        public void OnPlayerConnected(Player player)
        {
            player.SendChatMessage("~o~Добро пожаловать на My Server!");
            player.SendChatMessage("~o~Меню персонажа и прочее: ~w~M");
            Utils.players.Add(player);

            player.TriggerEvent("Connect");
            if(MySQL.IsAccountExist(player.Name))
            {
                player.SendChatMessage("~r~Ваш аккаунт уже зарегистрирован на сервере! Используйте ~w~/login");
            }
            else
            {
                player.SendChatMessage("~r~Ваш аккаунт не зарегистрирован на сервере! Используйте ~w~/register");
            }
        }
        
        [ServerEvent(Event.PlayerSpawn)]
        private void OnPlayerSpawn(Player player)
        {
            if (player.GetSharedData<bool>("isFirstSpawn"))
            {
                Utils.AntiCheatDeactivator(player);

                NAPI.Task.Run(() =>
                {
                    player.Position = new Vector3(-1039.1537, -2740.9243, 20.16929);
                    player.SendChatMessage("~g~Вы успешно появились в аэропорту Лос-Сантоса");
                    player.SetSharedData("isFirstSpawn", false);

                }, delayTime: 500); // 5 секунд     
            }
        }
        [ServerEvent(Event.PlayerDeath)]
        public void OnPlayerDeath(Player player, Player killer, uint reason)
        {
            Accounts target_account = player.GetData<Accounts>(Accounts._accountKey);
            if(killer != null && killer != player)
            {
                    Accounts killer_account = player.GetData<Accounts>(Accounts._accountKey);

                    var target_level = target_account._adminlevel;
                    var admin_level = killer_account._adminlevel;

                    if (admin_level <= 1 && target_level == 0)
                    {
                        var detecting = killer.GetSharedData<int>("AntiCheat:Admin:KillDetect");
                        if (detecting != 2)
                        {
                            killer.SetSharedData("AntiCheat:Admin:KillDetect", ++detecting);
                        }
                        if(detecting == 2)
                        {
                            Utils.AntiCheatMessage($"~r~Администратор {killer.Name} убил игрока 3 раза!", (int)Accounts.AdminRanks.Helper, player);
                            NAPI.Util.ConsoleOutput($"{killer.Name} убил игрока 3 раза!");
                            Logs(2, $"Администратор {killer.Name} {admin_level} LVL", $"[WARNING] {killer.Name} убил игрока {player.Name}! <@&1037683982785073264> <@&1037713843293536296>");
                            killer.Kick();
                            NAPI.Player.SpawnPlayer(player, player.Position);
                            return;
                        }
                    }
                
            }

            if (!player.GetSharedData<bool>("ArenaSpawn"))
            {
                player.TriggerEvent("SERVER:CLIENT:PlayerDeathFX");
                player.SendNotification("~r~Вы погибли!");
                player.TriggerEvent("disableNoclip");
                player.TriggerEvent("SERVER:CLIENT:fivesecondsFX");
                if (killer != null && killer != player) killer.SendNotification("~g~Вы убили игрока ~w~" + player.Name);
                NAPI.Task.Run(() =>
                {
                    Utils.AntiCheatDeactivator(player);
                    NAPI.Task.Run(() =>
                    {
                        NAPI.Player.SpawnPlayer(player, new Vector3(185.46068, -906.3381, 31.185102));
                        player.SendChatMessage("~r~Вы погибли и вы попали в больницу!");
                        player.Health = 45;

                    }, delayTime: 500); // 5 секунд     
                    

                }, delayTime: 5000); // 5 секунд
            }
        }

        [ServerEvent(Event.ResourceStart)]
        public void OnResourceStarted()
        {
            try
            {
                NAPI.World.SetWeather(Weather.XMAS);
                NAPI.Server.SetAutoRespawnAfterDeath(false);
                NAPI.Server.SetCommandErrorMessage("~r~[Ошибка] ~w~Команда не найдена.");

                MySQL.InitConnection();

                NAPI.Server.SetGlobalServerChat(false);

                date = new DateTime();
                date = DateTime.Now;
                NAPI.World.SetTime(date.Hour, date.Minute, date.Second);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            
        }

        [ServerEvent(Event.ChatMessage)]
        private void OnPlayerChat(Player player, string message)
        {
            Accounts account = player.GetData<Accounts>(Accounts._accountKey);
            //чат для игроков
            if(message.Contains("{") || message.Contains("}") || message.Contains("~"))
            {
                player.SendChatMessage("~o~Сервер: ~r~Использование HEX цветов запрещено!");
                return;
            }
            foreach (string badword in badwords)
            {
                if (message.Contains(" " + badword))
                {
                    player.SendChatMessage("~o~Сервер: ~r~Запрещено использование этого слова!");
                    return;
                }
            }
            if(!message.Contains(" ") && message.Length > 49)
            {
                player.SendChatMessage("~r~Сообщение должно содержать минимум 1 пробел!");
                return;
            }
            message.PadLeft(49);
            string newmessage = player.Name + "[" + Utils.GetPlayerID(player.Name) + "]: !{FFFFFF}" + message;
            Utils.SendRadiusMessage(newmessage, 10, player);
        }


        #region Discord
        static List<string> colors = new List<string>()
        {
            "5763719", // green
            "8464385", // red
            "3447003", // blue
            "16776960", // yellow
            "15105570", // orange
            "16777215", // white
            "2303786", // black
        };
        private void Logs(int webId, string title, string description)
        {
            try
            {
                string token = " ";
                switch (webId)
                {
                    case 0: // server logs
                        token = "";
                        break;
                    case 1: // player logs
                        token = "";
                        break;
                    case 2: // admin logs
                        token = "";
                        break;
                    case 3: // kill logs
                        token = "";
                        break;
                    case 4: // ban logs
                        token = "";
                        break;
                    case 5: // money logs
                        token = "";
                        break;
                    case 6: // events in discord
                        token = "";
                        break;
                }

                string name = "Наталья Морская Пехота";
                if (webId == 6) name = "My RolePlay";


                WebRequest wr = (HttpWebRequest)WebRequest.Create(token);

                wr.ContentType = "application/json";

                wr.Method = "POST";

                using (var sw = new StreamWriter(wr.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(new
                    {
                        username = name,
                        embeds = new[]
                        {
                        new
                        {
                            description = description,
                            title = title,
                            color = colors[rndm.Next(colors.Count)]
                        }
                    }

                    });
                    sw.Write(json);
                }
                var responce = (HttpWebResponse)wr.GetResponse();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        #endregion 
    }

}
