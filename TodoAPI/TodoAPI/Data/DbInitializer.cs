using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoAPI.Models;

namespace TodoAPI.Data
{
    public class DbInitializer
    {
        public static void Initialize(TodoAPIContext database)
        {
            if (database.Note.Any())
            {
                return;
            }

            database.Note.Add(new Note
            {
                Text = "Brad",
                IsDone = false
            });
            database.Note.Add(new Note
            {
                Text = "Angelina",
                IsDone = false
            });
            database.Note.Add(new Note
            {
                Text = "Will",
                IsDone = true
            });
            database.SaveChanges();
        }
    }
}
