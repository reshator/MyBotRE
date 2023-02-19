using MyBotRE.Adapters;
using System.Reflection.PortableExecutable;
using Telegram.Bot;

namespace MyBotRE.Utils
{
    internal class Sender
    {
        public static async Task SendMessageToEveryone(ITelegramBotClient botClient)
        {
            var selection = new SqlAdapter().SelectAllUserId().Result;
            using var sqlReader = selection.ExecuteReader();
            while (sqlReader.Read())   // построчно считываем данные
            {
                var id = sqlReader.GetInt64(0);
                var username = sqlReader.GetString(1);
                await botClient.SendTextMessageAsync(id,
               $"Любезный/ая @{username} пользователь робота-поправляльщика " +
               "\"Корни Русского\"!\r\n\r\nСоздан поток с прибаутками - @korni_rus. " +
               "Подписывайтесь\r\n\r\nРазработка и пополнение словаря робота требуют БОЛЬШОГО количества времени и труда. " +
               "Равно как и размещение робота на узлодержках во всесети." +
               " Посему, как разработчик поправляльщика, прошу ваших посильных благодеяний , чтобы и дальше развивать робота и корнесловие.\r\n\r\nНе откажите в посильной поддержке.\r\n" +
               "Плоска Сбера: 2202 2004 9802 2514.\r\nИли заграничная плоска (Казахстан) 4400 4302 5241 0342\r\nИли заверьте ежемесячное пожертвование тут: boosty.to/korni_rus. " +
               "\r\n\r\n(Робот в данную пору работает с перебоями. Вопрос решается)\r\n\r\nСпасибо, что уделили внимание!");
            }
              
        }
    }
}
