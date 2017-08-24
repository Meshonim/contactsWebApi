using ContactApp.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactApp.Mappers
{
    public static class ContactMapper
    {

        public static void GetParametersFromContact(MySqlCommand command, Contact contact)
        {
            command.Parameters.AddWithValue("@first", contact.First);
            command.Parameters.AddWithValue("@last", contact.Last);
            command.Parameters.AddWithValue("@dob", contact.Dob.ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@Phone", contact.Phone);
            command.Parameters.AddWithValue("@gender", contact.Gender);
            command.Parameters.AddWithValue("@rel", contact.Rel);
            command.Parameters.AddWithValue("@des", contact.Des ?? string.Empty);
        }

        public static Contact GetContactFromReader(MySqlDataReader reader)
        {
            int id = (int)reader["id"];
            bool isFavorite = (bool)reader["isFavorite"];
            string first = (string)reader["first"];
            string last = (string)reader["last"];
            DateTime dob = (DateTime)reader["dob"];
            string phone = (string)reader["phone"];
            bool gender = (bool)reader["gender"];
            string rel = (string)reader["rel"];
            string des = (string)reader["des"];
            var contact =
                new Contact
                {
                    Id = id,
                    IsFavorite = isFavorite,
                    First = first,
                    Last = last,
                    Dob = dob,
                    Phone = phone,
                    Gender = gender,
                    Rel = rel,
                    Des = des
                };
            return contact;
        }
    }
}