using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBook.Models
{
    public class Contact
    {
        /// <summary>
        /// Идентификатор контакта
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        public string Surname { get; set; }
        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime Birthday { get; set; }
        /// <summary>
        /// Комания
        /// </summary>
        public string Company { get; set; }
        /// <summary>
        /// E-Mail
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Телефон +7 (***) ***-**-**
        /// </summary>        
        public string Phone { get; set; }
        /// <summary>
        /// Избранный контакт
        /// </summary>
        public bool Favorite { get; set; }
    }
}
