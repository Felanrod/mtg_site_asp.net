using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Data.SqlClient;

namespace BusinessRules
{
    public class CPlayer
    {
        //configure the SQL connection
        SqlConnection objConn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["strConn"].ConnectionString);

        private int nPlayerID;
        private string nPlayerName;
        private string nProvinceID;
        private int nCityID;
        private int nPlayedID;
        private int nStoreID;
        private string nEmail;
        private int nShowEmail;
        private string nAbout;
        private string nRole;

        //function to get the userid from selected row in the gridview
        public int PlayerID
        {
            get { return nPlayerID; }
            set { nPlayerID = value; }
        }

        public string PlayerName
        {
            get { return nPlayerName; }
            set { nPlayerName = value; }
        }

        public string ProvinceID
        {
            get { return nProvinceID; }
            set { nProvinceID = value; }
        }

        public int CityID
        {
            get { return nCityID; }
            set { nCityID = value; }
        }

        public int StoreID
        {
            get { return nStoreID; }
            set { nStoreID = value; }
        }

        public int PlayedID
        {
            get { return nPlayedID; }
            set { nPlayedID = value; }
        }

        public string Email
        {
            get { return nEmail; }
            set { nEmail = value; }
        }

        public int ShowEmail
        {
            get { return nShowEmail; }
            set { nShowEmail = value; }
        }

        public string About
        {
            get { return nAbout; }
            set { nAbout = value; }
        }

        public string Role
        {
            get { return nRole; }
            set { nRole = value; }
        }

        /*c# code for hashing and salting - credit Dino Esposito at http://devproconnections.com/aspnet/aspnet-web-security-protect-user-passwords-hashing-and-salt?page=2 */

        public string hashPassword(string password, string salt)
        {
            var combinedPassword = String.Concat(password, salt);
            var sha256 = new SHA256Managed();
            var bytes = UTF8Encoding.UTF8.GetBytes(combinedPassword);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        public string getRandomSalt(Int32 size = 12)
        {
            var random = new RNGCryptoServiceProvider();
            var salt = new Byte[size];
            random.GetBytes(salt);
            return Convert.ToBase64String(salt);
        }

        public bool validatePassword(string enteredPassword, string storedHash, string storedSalt)
        {
            // Consider this function as an internal function where parameters like
            // storedHash and storedSalt are read from the database and then passed.

            var hash = hashPassword(enteredPassword, storedSalt);
            return String.Equals(storedHash, hash);
        }


        //the method for registering a new user
        public bool register(string playerName, string provinceID, int cityID, int storeID, int playedID, string email, int showEmail, string about, string password, string role)
        {
            //create a unique salt value and combine it to the hash of the main password
            string salt = getRandomSalt(20);
            string hashedPassword = hashPassword(password, salt);
            int userCount;

            //create the userExists variable and set it to true until proven otherwise
            bool userExists = true;

            //connect to the db
            objConn.Open();

            //set up a select count query to find out if there is already a username in the table
            //that's the same as the one entered on the register form
            string strSQLCount = "SELECT COUNT(*) FROM Players WHERE PlayerName = '" + playerName + "'";
            SqlCommand objCmd = new SqlCommand(strSQLCount, objConn);
            userCount = Convert.ToInt32(objCmd.ExecuteScalar());

            //if there is no other username in the table that's the same as the one entered
            //then create an insert statement and insert the new user into the Users Table
            if (userCount == 0)
            {
                string strSQL = "INSERT INTO Players (PlayerName, ProvinceID, CityID, StoreID, PlayedID, Email, ShowEmail, About, Password, SaltString, Role) VALUES ('" +
                    playerName + "', '" + provinceID + "', " + cityID + ", " + storeID + ", " + playedID + ", '" + email + "', " +
                    showEmail + ", '" + about + "', '" + hashedPassword + "', '" + salt + "', '" + role + "')";

                objCmd = new SqlCommand(strSQL, objConn);
                objCmd.ExecuteNonQuery();
                //set userExists to false
                userExists = false;
            }

            //close up
            objCmd.Dispose();
            objConn.Close();

            //return whether a user exists or not to the register page
            return userExists;
        }

        //the login function used to validate the credentials entered and return their role
        //to the login page
        public string login(string playerName, string password)
        {
            string role = "";

            //check the db for this user
            objConn.Open();
            string strSQL = "SELECT * FROM Players WHERE PlayerName = '" + playerName + "'";

            SqlCommand objCmd = new SqlCommand(strSQL, objConn);
            SqlDataReader objRdr = objCmd.ExecuteReader();

            //salt and hash entered password and compare to db ones
            while (objRdr.Read())
            {
                string hashpass = objRdr.GetString(9);
                string salty = objRdr.GetString(10);
                if (validatePassword(password, objRdr.GetString(9), objRdr.GetString(10)))
                {
                    //if we found a match
                    role = objRdr.GetString(11);
                }
            }

            //clean up
            objRdr.Close();
            objCmd.Dispose();
            objConn.Close();

            //return the user's role to the login page
            return role;
        }

        //queries the Provinces table and returns the results
        public SqlDataReader getProvinces()
        {
            objConn.Open();

            SqlCommand objCmd = new SqlCommand("SELECT * FROM Provinces ORDER BY ProvinceName", objConn);

            SqlDataReader objRdr;

            objRdr = objCmd.ExecuteReader();

            return objRdr;

        }

        //queries the Cities table and returns the results
        public SqlDataReader getCities(string provinceID)
        {
            objConn.Open();

            SqlCommand objCmd;

            if (provinceID == "ZZ")
            {
                objCmd = new SqlCommand("SELECT * FROM City ORDER BY CityName", objConn);
            }
            else
            {
                objCmd = new SqlCommand("SELECT * FROM City WHERE ProvinceID = '" + provinceID + "' ORDER BY CityName", objConn);
            }
            SqlDataReader objRdr;

            objRdr = objCmd.ExecuteReader();

            objCmd.Dispose();

            return objRdr;

        }

        //queries the Cities table and returns the results
        public SqlDataReader getStores(int cityID)
        {
            objConn.Open();

            SqlCommand objCmd;

            if (cityID == 0)
            {
                objCmd = new SqlCommand("SELECT * FROM Stores ORDER BY StoreName", objConn);
            }
            else
            {
                //SELECT * FROM Stores JOIN City ON Stores.CityID = City.CityID WHERE CityID = " + cityID + " ORDER BY StoreName", objConn
                objCmd = new SqlCommand("SELECT * FROM Stores WHERE CityID = " + cityID + " ORDER BY StoreName", objConn);
            }
            SqlDataReader objRdr;

            objRdr = objCmd.ExecuteReader();

            objCmd.Dispose();

            return objRdr;

        }

        //queries the Provinces table and returns the results
        public SqlDataReader getPlayed()
        {
            objConn.Open();

            SqlCommand objCmd = new SqlCommand("SELECT * FROM PlayedFor ORDER BY PlayedID", objConn);

            SqlDataReader objRdr;

            objRdr = objCmd.ExecuteReader();

            return objRdr;

        }

        //the getPlayers function returns a query of PlayerID, PlayerName, and Role from the Players table
        //to the Players page
        public SqlDataReader getPlayers()
        {
            objConn.Open();
            SqlCommand objCmd = new SqlCommand("SELECT PlayerID, PlayerName, Role FROM Players ORDER BY PlayerName", objConn);
            SqlDataReader objRdr = objCmd.ExecuteReader();
            return objRdr;
        }

        public SqlDataReader getPlayer()
        {
            objConn.Open();

            SqlCommand objCmd = new SqlCommand("SELECT * FROM Players WHERE PlayerID = " + PlayerID.ToString(), objConn);

            SqlDataReader objRdr;

            objRdr = objCmd.ExecuteReader();

            while (objRdr.Read())
            {
                PlayerName = objRdr.GetString(1);
                ProvinceID = objRdr.GetString(2);
                CityID = objRdr.GetInt32(3);
                StoreID = objRdr.GetInt32(4);
                PlayedID = objRdr.GetInt32(5);
                Email = objRdr.GetString(6);
                ShowEmail = objRdr.GetInt32(7);
                About = objRdr.GetString(8);
                Role = objRdr.GetString(11);
            }

            objRdr.Close();
            objCmd.Dispose();
            objConn.Close();
            return objRdr;
        }

        public SqlDataReader getPlayersGV()
        {
            objConn.Open();

            SqlCommand objCmd = new SqlCommand("SELECT PlayerID, PlayerName, Provinces.ProvinceName, City.CityName, Stores.StoreName, PlayedFor.TimePlayed, Email, About, Role  FROM Players JOIN Provinces ON Players.ProvinceID = Provinces.ProvinceID JOIN City ON Players.CityID = City.CityID JOIN Stores ON Players.StoreID = Stores.StoreID JOIN PlayedFor ON Players.PlayedID = PlayedFor.PlayedID ORDER BY PlayerName", objConn);

            SqlDataReader objRdr;

            objRdr = objCmd.ExecuteReader();

            return objRdr;

        }

        public SqlDataReader getStoresGV()
        {
            objConn.Open();

            SqlCommand objCmd = new SqlCommand("SELECT StoreID, StoreName, City.CityName, StoreAddress, PhoneNumber FROM Stores JOIN City ON Stores.CityID = City.CityID ORDER BY StoreName", objConn);

            SqlDataReader objRdr;

            objRdr = objCmd.ExecuteReader();

            return objRdr;

        }

        public void savePlayer(string playerName, string provinceID, int cityID, int storeID, int playedID, string email, int showEmail, string about, string role)
        {
            objConn.Open();

            SqlCommand objCmd = new SqlCommand("UPDATE Players SET PlayerName = '" + playerName + "', ProvinceID = '" + provinceID +
                    "', CityID = " + cityID + ", StoreID = " + storeID + ", PlayedID = " + playedID + ", Email = '" + Email +
                    "', ShowEmail = " + showEmail + ", About = '" + about + "', Role = '" + role + "' WHERE PlayerID = " + PlayerID.ToString(), objConn);

            objCmd.ExecuteNonQuery();

            //close up
            objCmd.Dispose();
            objConn.Close();
        }

        //delete selected Player from the Players page from the Players table
        public void deletePlayer()
        {
            objConn.Open();
            SqlCommand objCmd = new SqlCommand("DELETE FROM Players WHERE PlayerID = " + PlayerID.ToString(), objConn);
            objCmd.ExecuteNonQuery();

            //close up
            objCmd.Dispose();
            objConn.Close();
        }

        //delete selected Player from the Players page from the Players table
        public void deleteStore()
        {
            objConn.Open();
            SqlCommand objCmd = new SqlCommand("DELETE FROM Stores WHERE StoreID = " + StoreID.ToString(), objConn);
            objCmd.ExecuteNonQuery();

            //close up
            objCmd.Dispose();
            objConn.Close();
        }

        public int getPlayerID(string logPlayer)
        {
            objConn.Open();

            SqlCommand objCmd = new SqlCommand("SELECT PlayerID FROM Players WHERE PlayerName = '" + logPlayer + "'", objConn);

            //SqlDataReader objRdr;

            //objRdr = objCmd.ExecuteReader();

            int playerID = Convert.ToInt32(objCmd.ExecuteScalar());

            //close up
            objCmd.Dispose();
            objConn.Close();

            return playerID;

        }
    }
}
