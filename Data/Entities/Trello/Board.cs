using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WelcomeASP.Data.Entities.Trello
{
    public class Board
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public String Title { get; set; }
        public List<TrelloTag> Tags { get; set; }
        public List<Column> Columns { get; set; }
        public string ImgUrl { get; set; }
        public byte[] Logo { get; set; }
    }
}
