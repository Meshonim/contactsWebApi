using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MySql.Data.MySqlClient;
using ContactApp.Models;
using ContactApp.Mappers;

namespace ContactApp.Controllers
{
    public class ContactsController : ApiController
    {
        // GET api/contacts
        public HttpResponseMessage Get()
        {
            List<Contact> list = new List<Contact>();
            try
            {
                var conn = WebApiConfig.GetMySqlConnection();
                var command = conn.CreateCommand();
                command.CommandText = "SELECT * FROM contacts";
                conn.Open();
                try
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var contact = ContactMapper.GetContactFromReader(reader);
                            list.Add(contact);
                        }
                    }
                }
                catch
                {
                    throw;
                }
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK, list);
        }

        // GET api/contacts/5
        public HttpResponseMessage Get(int id)
        {
            Contact contact = new Contact();
            try
            {
                var conn = WebApiConfig.GetMySqlConnection();
                var command = conn.CreateCommand();
                command.CommandText = "SELECT * FROM contacts WHERE id=@id";
                command.Parameters.AddWithValue("@id", id);
                conn.Open();
                try
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                            contact = ContactMapper.GetContactFromReader(reader);
                        else
                            return Request.CreateResponse(HttpStatusCode.NotFound);
                    }
                }
                catch
                {
                    throw;
                }
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK, contact);
        }

        // POST api/contacts
        public HttpResponseMessage Post([FromBody]Contact contact)
        {
            try
            {
                var conn = WebApiConfig.GetMySqlConnection();
                var command = conn.CreateCommand();
                command.CommandText = @"INSERT INTO contacts
                                        (`id`, `isFavorite`, `first`,
                                         `last`, `dob`, `phone`,
                                         `gender`, `rel`, `des`)
                                        VALUES
                                        (NULL, 0, @first,
                                        @last, @dob, @phone,
                                        @gender, @rel, @des)";
                ContactMapper.GetParametersFromContact(command, contact);
                conn.Open();
                try
                {
                    if (command.ExecuteNonQuery() != 1)
                        return Request.CreateResponse(HttpStatusCode.NotFound);
                }
                catch
                {
                    throw;
                }
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // PUT api/contacts/5
        public HttpResponseMessage Put(int id, [FromBody]Contact contact)
        {
            try
            {
                var conn = WebApiConfig.GetMySqlConnection();
                var command = conn.CreateCommand();
                command.CommandText = @"UPDATE contacts
		                                SET first=@first, last=@last,
		                                dob=@dob, phone=@phone, gender=@gender,
		                                rel=@rel, des=@des WHERE id=@id";
                command.Parameters.AddWithValue("@id", id);
                ContactMapper.GetParametersFromContact(command, contact);
                conn.Open();
                try
                {
                    if (command.ExecuteNonQuery() != 1)
                        return Request.CreateResponse(HttpStatusCode.NotFound);
                }
                catch
                {
                    throw;
                }
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // DELETE api/contacts/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var conn = WebApiConfig.GetMySqlConnection();
                var command = conn.CreateCommand();
                command.CommandText = "DELETE FROM contacts WHERE id=@id";
                command.Parameters.AddWithValue("@id", id);
                conn.Open();
                try
                {
                    if (command.ExecuteNonQuery() != 1)
                        return Request.CreateResponse(HttpStatusCode.NotFound);
                }
                catch
                {
                    throw;
                }
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // PATCH api/contacts/5
        [HttpPatch]
        public HttpResponseMessage Patch(int id, [FromBody]PatchData patchData)
        {
            try
            {
                var conn = WebApiConfig.GetMySqlConnection();
                var command = conn.CreateCommand();
                command.CommandText = "UPDATE contacts SET isFavorite=@status WHERE id=@id";
                command.Parameters.AddWithValue("@status", (patchData.IsFavorite ? 1 : 0));
                command.Parameters.AddWithValue("@id", id);
                conn.Open();
                try
                {
                    if (command.ExecuteNonQuery() != 1)
                        return Request.CreateResponse(HttpStatusCode.NotFound);
                }
                catch
                {
                    throw;
                }
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
