using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WelcomeASP.Data.Entities.Trello
{
    public class TrelloTag
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public String Name { get; set; }
        public List<Board> Boards { get; set; }
    }
}
