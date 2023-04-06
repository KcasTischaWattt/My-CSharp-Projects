using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text.Json;
using System.Threading.Tasks;
using WebPeer.Models;

namespace WebPeer.Controllers
{
    /// <summary>
    /// Главный контроллер
    /// </summary>
    [Route("/api/[controller]")]
    public class UsersController : Controller
    {
        /// <summary>
        /// Список пользователей
        /// </summary>
        static List<User> users = new();
        /// <summary>
        /// Список сообщений
        /// </summary>
        static List<Mail> mails = new();

        /// <summary>
        /// Посмотреть всех пользователей
        /// </summary>
        /// <returns>Список всех пользователей</returns>
        [HttpGet]
        public IEnumerable<User> GetAll() => users;

        /// <summary>
        /// Посмотреть всех пользователей с настройкой
        /// </summary>
        /// <param name="offset"> порядковый номер</param>
        /// <param name="limit">Количество пользователей</param>
        /// <returns></returns>
        [HttpGet("Users{offset}/Users{limit}")]
        public IActionResult GetAllWithParams(int offset, int limit)
        {
            try
            {
                if (limit <= 0 || offset < 0)
                    return BadRequest("Limit & offset should be positive or non-negative!");
                List<User> okUsers = new();
                if (offset < users.Count)
                    for (int i = offset; i < (limit + offset >= users.Count ? users.Count : limit + offset); i++)
                    {
                        okUsers.Add(users[i]);
                    }
                return Ok(okUsers);
            }
            catch (Exception)
            {

                return BadRequest("Упсс.. Что то пошло не так");
            }

        }

        /// <summary>
        /// Загрузить данные из json файла
        /// </summary>
        /// <returns>Данные из json файла</returns>
        [HttpPost("PostFromJson")]
        public IActionResult PostFromJson()
        {
            try
            {
                if ((System.IO.File.Exists("UserList.json")) && (System.IO.File.Exists("MailList.json")))
                {
                    using (FileStream fs = new FileStream("UserList.json", FileMode.Open))
                    {
                        var formatter = new DataContractJsonSerializer(typeof(List<User>));
                        users = (List<User>)formatter.ReadObject(fs);
                    }
                    using (FileStream fs = new FileStream("MailList.json", FileMode.Open))
                    {
                        var formatter = new DataContractJsonSerializer(typeof(List<Mail>));
                        mails = (List<Mail>)formatter.ReadObject(fs);
                    }
                }
                else
                {
                    return BadRequest("Файлы не найдены");
                }
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest("Упсс.. Что то пошло не так");
            }

        }

        /// <summary>
        /// Найти пользователя по Email
        /// </summary>
        /// <param name="email">Email адрес</param>
        /// <returns>Имя пользователя с указанным Email</returns>
        [HttpGet("Email/{email}")]
        public IActionResult GetByEmail(string email)
        {
            try
            {
                foreach (var user in users)
                    if (user.Email == email)
                        return Ok(user);
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest("Упсс.. Что то пошло не так");
            }
        }

        /// <summary>
        /// Найти сообщения по ID отправителя и получателя
        /// </summary>
        /// <param name="recieverId">ID отправителя</param>
        /// <param name="senderId">ID получателя</param>
        /// <returns>Список сообщений</returns>
        [HttpGet("reciverEmail/{recieverId}, senderEmail/{senderId}")]
        public IActionResult GetByBoth(string recieverId, string senderId)
        {
            try
            {
                List<Mail> okMails = new();
                foreach (var m in mails)
                {
                    if (m.ReceiverId == recieverId && m.SenderId == senderId)
                        okMails.Add(m);
                }
                return Ok(okMails);
            }
            catch (Exception)
            {
                return BadRequest("Упсс.. Что то пошло не так");
            }
        }

        /// <summary>
        /// Найти сообщения по ID отправителя
        /// </summary>
        /// <param name="senderId">ID отправитиля</param>
        /// <returns>Список сообщений</returns>
        [HttpGet("senderEmail/{senderId}")]
        public IActionResult GetBySenderID(string senderId)
        {
            try
            {
                List<Mail> okMails = new();
                foreach (var m in mails)
                {
                    if (m.SenderId == senderId)
                        okMails.Add(m);
                }
                return Ok(okMails);
            }
            catch (Exception)
            {
                return BadRequest("Упсс.. Что то пошло не так");
            }
        }

        /// <summary>
        /// Найти сообщения по ID получателя
        /// </summary>
        /// <param name="recieverId">ID получателя</param>
        /// <returns>Список сообщений</returns>
        [HttpGet("reciverEmail/{recieverId}")]
        public IActionResult GetByRecieverID(string recieverId)
        {
            try
            {
                List<Mail> okMails = new();
                foreach (var m in mails)
                {
                    if (m.ReceiverId == recieverId)
                        okMails.Add(m);
                }
                return Ok(okMails);
            }
            catch (Exception)
            {
                return BadRequest("Упсс.. Что то пошло не так");
            }
        }

        /// <summary>
        /// Рандомная генерация данных
        /// </summary>
        [HttpPost]
        public IActionResult Post()
        {
            try
            {
                users = new();
                mails = new();
                var generator = new Random();
                var usersCount = generator.Next(3, 10);
                var mailsCount = generator.Next(1, usersCount * 5);
                for (int i = 0; i < usersCount; i++)
                {
                    var user = new User();
                    user.Email = CreateRandomEmail(generator.Next(7, 13));
                    user.UserName = CreateRandomString(generator.Next(5, 10));
                    users.Add(user);
                }
                users.Sort();
                for (int i = 0; i < mailsCount; i++)
                {
                    var mail = new Mail();
                    mail.Message = CreateRandomString(generator.Next(5, 20));
                    mail.Subject = CreateRandomString(generator.Next(3, 10));
                    mail.ReceiverId = users[generator.Next(usersCount)].Email;
                    do
                    {
                        mail.SenderId = users[generator.Next(usersCount)].Email;
                    } while (mail.SenderId == mail.ReceiverId);
                    mails.Add(mail);
                }
                SerializeAll();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Упсс.. Что то пошло не так");
            }
        }

        /// <summary>
        /// Сериализация данных в файл
        /// </summary>
        private void SerializeAll()
        {
            System.IO.File.Delete("UserList.json");
            System.IO.File.Delete("MailList.json");
            var jsonUser = JsonSerializer.Serialize(users);
            var jsonMail = JsonSerializer.Serialize(mails);
            System.IO.File.AppendAllText("UserList.json", jsonUser);
            System.IO.File.AppendAllText("MailList.json", jsonMail);
        }

        /// <summary>
        /// Генерация случайного Email
        /// </summary>
        /// <param name="length">Длина email</param>
        /// <returns>Рандомный email</returns>
        private string CreateRandomEmail(int length)
        {
            string resultEmail;
            do
            {
                resultEmail = CreateRandomString(length);
            } while (EmailIsExist(resultEmail));
            return resultEmail;
        }

        /// <summary>
        /// Проверка на существования email
        /// </summary>
        /// <param name="resultEmail">email, который надо проверять его на существование</param>
        /// <returns></returns>
        private bool EmailIsExist(string resultEmail)
        {
            foreach (var user in users)
            {
                if (user.Email == resultEmail)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Создание рандомной строки
        /// </summary>
        /// <param name="length">Длина строки</param>
        /// <returns>Рандомная строка</returns>
        private string CreateRandomString(int length)
        {
            string result = "";
            var generator = new Random();
            for (int i = 0; i < length; i++)
            {
                int letter;
                do
                {
                    letter = generator.Next('0', 'z');
                } while ((letter > '9' && letter < 'A') || (letter > 'Z' && letter < 'a'));
                result += (char)letter;
            }
            return result;
        }

        /// <summary>
        /// Создание нового пользователя
        /// </summary>
        /// <param name="email">Email адрес</param>
        /// <param name="userName">Имя пользователя</param>
        /// <returns>Новый пользователь</returns>
        [HttpPost("/{email}, /{userName}")]
        public IActionResult PostNewUser(string email, string userName)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (EmailIsExist(email))
                    return BadRequest("Email уже зарегестрирован!");
                var newUser = new User();
                newUser.Email = email;
                newUser.UserName = userName;
                users.Add(newUser);
                users.Sort();
                SerializeAll();
                return Ok(newUser);
            }
            catch (Exception)
            {
                return BadRequest("Упсс.. Что то пошло не так");
            }
        }

        /// <summary>
        /// Отправка нового сообщения
        /// </summary>
        /// <param name="senderId">Отправитель</param>
        /// <param name="recieverId">Получатель</param>
        /// <param name="subject">Тема</param>
        /// <param name="message">Содержание</param>
        /// <returns>Новое письмо</returns>
        [HttpPost("/{senderId}, /{recieverId}, /{subject}, /{message}")]
        public IActionResult PostNewMessage(string senderId, string recieverId, string subject, string message)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (!EmailIsExist(senderId) || !EmailIsExist(recieverId))
                    return BadRequest("Пользователь не найден!");
                var newMail = new Mail();
                newMail.ReceiverId = recieverId;
                newMail.SenderId = senderId;
                newMail.Subject = subject;
                newMail.Message = message;
                mails.Add(newMail);
                SerializeAll();
                return Ok(newMail);
            }
            catch (Exception)
            {
                return BadRequest("Упсс.. Что то пошло не так");
            }
        }
    }
}
