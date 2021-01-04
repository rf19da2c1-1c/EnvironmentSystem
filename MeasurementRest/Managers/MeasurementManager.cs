using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using EnvironmentModelLib.model;

namespace MeasurementRest.Managers
{
    public class MeasurementManager
    {
        private const string connectionString =
            @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=PeleDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


        private const String GET_ALL = "select * from Measurement";
        public IEnumerable<Measurement> GetAll()
        {
            List<Measurement> liste = new List<Measurement>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(GET_ALL, conn))
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Measurement item = ReadNextElement(reader);
                    liste.Add(item);
                }
                reader.Close();
            }
            return liste;

        }


        private const String GET_BY_ID = "select * from Measurement where id=@ID";
        public Measurement GetById(int id)
        {
            Measurement meas = new Measurement();

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(GET_BY_ID, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@ID", id);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    meas = ReadNextElement(reader);
                }
                else
                {
                    throw new KeyNotFoundException("Id not found was " + id);
                }
                reader.Close();
            }
            
            return meas;
        }


        private const String INSERT = "insert into Measurement(temp, pres, hum) Values(@temp, @pres,@hum) ";
        public void Add(Measurement value)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(INSERT, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@temp", value.Temperature);
                cmd.Parameters.AddWithValue("@pres", value.Pressure);
                cmd.Parameters.AddWithValue("@hum", value.Humidity);

                int rowsAffected = cmd.ExecuteNonQuery();
                // evt. return rowsAffected == 1 => true if inserted otherwise false
            }

            // evt. hente sidste måling og sende tilbage
        }


        private const String DELETE_BY_ID = "Delete from Measurement where id=@id";
        public Measurement DeleteById(int id)
        {
            Measurement meas = GetById(id); 

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(DELETE_BY_ID, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@id", id);
                
                int rowsAffected = cmd.ExecuteNonQuery();
                // evt. return rowsAffected == 1 => true if inserted otherwise false
            }

            return meas;
        }

        private const String UPDATE = "UPDATE Measurement set temp=@TEMP, pres=@PRES, hum=@HUM, MTime=@MTIME where id=@ID";
        public void Update(int id, Measurement measurement)
        {
            Measurement meas = GetById(id);

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(UPDATE, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@TEMP", measurement.Temperature);
                cmd.Parameters.AddWithValue("@PRES", measurement.Pressure);
                cmd.Parameters.AddWithValue("@HUM", measurement.Humidity);
                cmd.Parameters.AddWithValue("@MTIME", measurement.TimeStamp);

                int rowsAffected = cmd.ExecuteNonQuery();
                // evt. return rowsAffected == 1 => true if inserted otherwise false

                if (rowsAffected != 1)
                {
                    throw new KeyNotFoundException("Id not found was " + id);
                }
            }

            
        }


        private Measurement ReadNextElement(SqlDataReader reader)
        {
            Measurement measurement = new Measurement();

            //todo ....
            measurement.Id = reader.GetInt32(0);
            measurement.Temperature = reader.GetDouble(1);
            measurement.Pressure = reader.GetDouble(2);
            measurement.Humidity = reader.GetDouble(3);
            measurement.TimeStamp = reader.GetDateTime(4);



            return measurement;
        }
    }
}
