using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace KDUInfoApp
{
    class DBAccess
    {

        SqlConnection conn;
        public DBAccess()
        {
            conn = ConnectionManager.getConnection();
        }

        public List<Cadet> getAllCadet()
        {
            List<Cadet> list = new List<Cadet>();
            try
            {
                if (conn.State.ToString() == "Closed")
                { conn.Open(); }
                SqlCommand newCmd = conn.CreateCommand();
                newCmd.Connection = conn;
                newCmd.CommandType = CommandType.Text;
                newCmd.CommandText = "select SNO,name,intake from Cadet";
                SqlDataReader dr = newCmd.ExecuteReader();
                while (dr.Read())
                {
                    list.Add(new Cadet { SNO = dr["SNO"].ToString(), name = dr["name"].ToString(), intake = dr["intake"].ToString() });
                }
                conn.Close();
            }
            catch (InvalidExpressionException)
            { }
            return list;
        }


        //For Login (Get user type according to UserName and Password)
        public List<User> getUser(String userName)
        {
            List<User> list = new List<User>();

            if (conn.State.ToString() == "Closed")
            { conn.Open(); }
            SqlCommand newCmd = conn.CreateCommand();
            newCmd.Connection = conn;
            newCmd.CommandType = CommandType.Text;
            newCmd.CommandText = "select userName,userType,pwd from KDUuser where userName='" + userName + "'";
            SqlDataReader dr = newCmd.ExecuteReader();
            while (dr.Read())
            {
                list.Add(new User { name = dr["userName"].ToString(), userType = dr["userType"].ToString(), password = dr["pwd"].ToString() });
            }
            conn.Close();

            return list;
        }


        //Add data to data
        public bool addCadet(string SNO, string name, string NIC, string date1, string gender, string birth, string intake, string rank, string race,
                                string addr, string nextOfKin, string rela, string occu, string addrFml, string height, string weight, string chest,
                                string neck, string hat, string boot, string blood, string waist, string bankAcc, string police, string civilEp, string school,
                                string yrSchool, string sport, string OLyr, string OLsheet, string ALyr, string ALsheet, byte[] imgOL, byte[] imgAL, byte[] imgPro)
        {

            bool status;
            try
            {
                if (conn.State.ToString() == "Closed")
                {
                    conn.Open();
                }
                SqlCommand newCmd = conn.CreateCommand();
                newCmd.Connection = conn;
                newCmd.CommandType = CommandType.Text;
                newCmd.CommandText = "insert into Cadet values ('" + SNO + "', '" + name + "','" + NIC + "','" + date1 + "','" + gender + "','" + birth + "','" + intake + "','" + rank + "','" + race + "','" + addr + "','" + nextOfKin + "' ,'" + rela + "','" + occu + "','" + addrFml + "','" + height + "','" + weight + "','" + chest + "','" + neck + "','" + hat + "','" + boot + "','" + blood + "','" + waist + "','" + bankAcc + "','" + police + "','" + civilEp + "','" + school + "','" + yrSchool + "','" + sport + "','" + OLyr + "','" + OLsheet + "','" + ALyr + "','" + ALsheet + "',@imgOL, @imgAL, @imgPro)";
                //we can .....Add(new SqlParameter("@imgOL", imgOL)) use this method when we use one parameter. 
                newCmd.Parameters.AddWithValue("@imgOL", imgOL);
                newCmd.Parameters.AddWithValue("@imgAL", imgAL);
                newCmd.Parameters.AddWithValue("@imgPro", imgPro);
                newCmd.ExecuteNonQuery();
                conn.Close();
                status = true;
            }
            catch (SqlException e)
            {
                Console.WriteLine("DB error :" + e);
                status = false;
            }
            return status;
        }


        public bool addWorkAss(string SNO, string troop, string date, string drill, string pt, string wTrn, string gCondct,
                               string file, string total, byte[] imgHD)
        {

            bool status;
            try
            {
                if (conn.State.ToString() == "Closed") { conn.Open(); }
                SqlCommand newCmd = conn.CreateCommand();
                newCmd.Connection = conn;
                newCmd.CommandType = CommandType.Text;
                newCmd.CommandText = "insert into WorkAss ([SNO] ,[troop] ,[date]  ,[drill]  ,[pt]  ,[wTraning] ,[goodConduct]  ,[fileBook] ,[totalMrk] ,[hdImg]) values ('" + SNO + "', '" + troop + "','" + date + "','" + drill + "','" + pt + "','" + wTrn + "','" + gCondct + "','" + file + "','" + total + "', @hdImg)";
                newCmd.Parameters.Add(new SqlParameter("@hdImg", imgHD));
                newCmd.ExecuteNonQuery();
                status = true;
            }
            catch (SqlException e)
            {
                Console.WriteLine("DB error :" + e);
                status = false;
            }
            return status;
        }


        public bool addSickChrt(string SNO, string rank, string troop, string yr, string sickness, string categ, string date, byte[] imgHD)
        {

            bool status;
            try
            {
                if (conn.State.ToString() == "Closed") { conn.Open(); }
                SqlCommand newCmd = conn.CreateCommand();
                newCmd.Connection = conn;
                newCmd.CommandType = CommandType.Text;
                newCmd.CommandText = "insert into SickChart ([SNO] ,[rank],[troop] ,[year]  ,[sickness]  ,[category]  ,[date] ,[hdImg]) values ('" + SNO + "', '" + rank + "','" + troop + "','" + yr + "','" + sickness + "','" + categ + "','" + date + "',@hdImg)";
                newCmd.Parameters.Add(new SqlParameter("@hdImg", imgHD));
                newCmd.ExecuteNonQuery();
                status = true;
            }
            catch (SqlException e)
            {
                Console.WriteLine("DB error :" + e);
                status = false;
            }
            return status;
        }


        public bool addPTtest(string SNO, string rank, string troop, string tstnme, string marks, string sts, string date, byte[] imgHD)
        {

            bool status;
            try
            {
                if (conn.State.ToString() == "Closed") { conn.Open(); }
                SqlCommand newCmd = conn.CreateCommand();
                newCmd.Connection = conn;
                newCmd.CommandType = CommandType.Text;
                newCmd.CommandText = "INSERT INTO PTtest ([SNO] ,[rank] ,[troop] ,[testName] ,[marks] ,[status]  ,[date] ,[hdImg]) values ('" + SNO + "', '" + rank + "','" + troop + "','" + tstnme + "','" + marks + "','" + sts + "','" + date + "',@hdImg)";
                newCmd.Parameters.Add(new SqlParameter("@hdImg", imgHD));
                newCmd.ExecuteNonQuery();
                status = true;
            }
            catch (SqlException e)
            {
                Console.WriteLine("DB error :" + e);
                status = false;
            }
            return status;
        }


        public bool addIncidentDry(string SNO, string troop, string yr, string seNO, string date, string cInci, string remark, byte[] imgHD)
        {

            bool status;
            try
            {
                if (conn.State.ToString() == "Closed") { conn.Open(); }
                SqlCommand newCmd = conn.CreateCommand();
                newCmd.Connection = conn;
                newCmd.CommandType = CommandType.Text;
                newCmd.CommandText = "INSERT INTO IncidentDiary ([SNO] ,[troop] ,[year] ,[serialNO] ,[date] ,[cIncident],[remarks],[hdImg]) values ('" + SNO + "', '" + troop + "','" + yr + "','" + seNO + "','" + date + "','" + cInci + "','" + remark + "',@hdImg)";
                newCmd.Parameters.Add(new SqlParameter("@hdImg", imgHD));
                newCmd.ExecuteNonQuery();
                status = true;
            }
            catch (SqlException e)
            {
                Console.WriteLine("DB error :" + e);
                status = false;
            }
            return status;
        }


        public bool addGoodCndct(string SNO, string rnk, string date, string mrks, string reason, byte[] imgHD)
        {

            bool status;
            try
            {
                if (conn.State.ToString() == "Closed") { conn.Open(); }
                SqlCommand newCmd = conn.CreateCommand();
                newCmd.Connection = conn;
                newCmd.CommandType = CommandType.Text;
                newCmd.CommandText = "INSERT INTO GoodConduct ([SNO] ,[rank] ,[date] ,[marks] ,[reason] ,[hdImg]) values ('" + SNO + "', '" + rnk + "','" + date + "','" + mrks + "','" + reason + "',@hdImg)";
                newCmd.Parameters.Add(new SqlParameter("@hdImg", imgHD));
                newCmd.ExecuteNonQuery();
                status = true;
            }
            catch (SqlException e)
            {
                Console.WriteLine("DB error :" + e);
                status = false;
            }
            return status;
        }

        public bool addChargeSheet(string SNO, string rnk, string trp, string date, string offnc, string no, string witn, string push, byte[] imgHD)
        {

            bool status;
            try
            {
                if (conn.State.ToString() == "Closed") { conn.Open(); }
                SqlCommand newCmd = conn.CreateCommand();
                newCmd.Connection = conn;
                newCmd.CommandType = CommandType.Text;
                newCmd.CommandText = "INSERT INTO ChargeSheet ([SNO] ,[rank] ,[troop] ,[dateOfOffence] ,[offence] ,[noOfWit],[witness],[punish],[hdImg]) values ('" + SNO + "', '" + rnk + "','" + trp + "','" + date + "','" + offnc + "','" + no + "','" + witn + "','" + push + "',@hdImg)";
                newCmd.Parameters.Add(new SqlParameter("@hdImg", imgHD));
                newCmd.ExecuteNonQuery();
                status = true;
            }
            catch (SqlException e)
            {
                Console.WriteLine("DB error :" + e);
                status = false;
            }
            return status;
        }


        public bool addBlackBook(string SNO, string trp, string yr, string seNo, string off, string punish, string mrks, byte[] imgHD)
        {

            bool status;
            try
            {
                if (conn.State.ToString() == "Closed") { conn.Open(); }
                SqlCommand newCmd = conn.CreateCommand();
                newCmd.Connection = conn;
                newCmd.CommandType = CommandType.Text;
                newCmd.CommandText = "INSERT INTO BlackBook ([SNO] ,[troop] ,[year] ,[serialNO] ,[offence] ,[punishment],[marksDeducted],[hdImg]) values ('" + SNO + "', '" + trp + "','" + yr + "','" + seNo + "','" + off + "','" + punish + "','" + mrks + "',@hdImg)";
                newCmd.Parameters.Add(new SqlParameter("@hdImg", imgHD));
                newCmd.ExecuteNonQuery();
                status = true;
            }
            catch (SqlException e)
            {
                Console.WriteLine("DB error :" + e);
                status = false;
            }
            return status;
        }

        public bool addAttend(string SNO, string rnk, string trp, string militry, string acd, byte[] imgHD)
        {

            bool status;
            try
            {
                if (conn.State.ToString() == "Closed") { conn.Open(); }
                SqlCommand newCmd = conn.CreateCommand();
                newCmd.Connection = conn;
                newCmd.CommandType = CommandType.Text;
                newCmd.CommandText = "INSERT INTO Attendance ([SNO] ,[rank] ,[troop] ,[military] ,[academic], [hdImg]) values ('" + SNO + "', '" + rnk + "','" + trp + "','" + militry + "','" + acd + "',@hdImg )";
                newCmd.Parameters.Add(new SqlParameter("@hdImg", imgHD));
                newCmd.ExecuteNonQuery();
                status = true;
            }
            catch (SqlException e)
            {
                Console.WriteLine("DB error :" + e);
                status = false;
            }
            return status;
        }

        public bool addUser(string name, string type, string rank, string phone, string email, string Uname, string correctPwd, byte[] imgPrf)
        {

            bool status;
            try
            {
                if (conn.State.ToString() == "Closed") { conn.Open(); }
                SqlCommand newCmd = conn.CreateCommand();
                newCmd.Connection = conn;
                newCmd.CommandType = CommandType.Text;
                newCmd.CommandText = "INSERT INTO KDUuser ([name] ,[userType],[rank],[phone] ,[email],[userName] ,[pwd],[imgPrf]) values ('" + name + "', '" + type + "','" + rank + "','" + phone + "','" + email + "','" + Uname + "','" + correctPwd + "',@imgPrf)";
                newCmd.Parameters.Add(new SqlParameter("@imgPrf", imgPrf));
                newCmd.ExecuteNonQuery();
                status = true;
            }
            catch (SqlException e)
            {
                Console.WriteLine("DB error :" + e);
                status = false;
            }
            return status;
        }




        //-----------------------------------------------------------------------------------------------
        //Get data from db

        //Get Cadet details
        public DataSet getAllCadetsForGridView()
        {
            if (conn.State.ToString() == "Closed") { conn.Open(); }
            SqlCommand newCmd = conn.CreateCommand();
            newCmd.Connection = conn;
            newCmd.CommandType = CommandType.Text;
            newCmd.CommandText = "select SNO,name,NIC,dateOfEnlisment,gender,dateOfBirth,intake,rank,race,address,nextOfKin,relation,occupation,addressOfFamily,height,weight,chest,neck,hatSize,bootSize,bloodGroup,waist,bankAcc,polise,civilEmp,school,sport_other from Cadet";
            SqlDataAdapter da = new SqlDataAdapter(newCmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "cadetsOfKDU");
            conn.Close();
            return ds;
        }

        public DataSet getCadetsFromSNOForGridView(String SNO)
        {
            if (conn.State.ToString() == "Closed") { conn.Open(); }
            SqlCommand newCmd = conn.CreateCommand();
            newCmd.Connection = conn;
            newCmd.CommandType = CommandType.Text;
            newCmd.CommandText = "select SNO,name,NIC,dateOfEnlisment,gender,dateOfBirth,intake,rank,race,address,nextOfKin,relation,occupation,addressOfFamily,height,weight,chest,neck,hatSize,bootSize,bloodGroup,waist,bankAcc,polise,civilEmp,school,sport_other from Cadet where SNO LIKE '%" + SNO + "%' ";
            SqlDataAdapter da = new SqlDataAdapter(newCmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "SNO_cadetsOfKDU");
            conn.Close();
            return ds;
        }

        public DataSet getCadetsFromNameForGridView(String name)
        {
            if (conn.State.ToString() == "Closed") { conn.Open(); }
            SqlCommand newCmd = conn.CreateCommand();
            newCmd.Connection = conn;
            newCmd.CommandType = CommandType.Text;
            newCmd.CommandText = "select SNO,name,NIC,dateOfEnlisment,gender,dateOfBirth,intake,rank,race,address,nextOfKin,relation,occupation,addressOfFamily,height,weight,chest,neck,hatSize,bootSize,bloodGroup,waist,bankAcc,polise,civilEmp,school,sport_other from Cadet where name LIKE '%" + name + "%' ";
            SqlDataAdapter da = new SqlDataAdapter(newCmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "Name_cadetsOfKDU");
            conn.Close();
            return ds;
        }

        public DataSet getCadetsFromIntakeForGridView(String intake)
        {
            if (conn.State.ToString() == "Closed") { conn.Open(); }
            SqlCommand newCmd = conn.CreateCommand();
            newCmd.Connection = conn;
            newCmd.CommandType = CommandType.Text;
            newCmd.CommandText = "select SNO,name,NIC,dateOfEnlisment,gender,dateOfBirth,intake,rank,race,address,nextOfKin,relation,occupation,addressOfFamily,height,weight,chest,neck,hatSize,bootSize,bloodGroup,waist,bankAcc,polise,civilEmp,school,sport_other from Cadet where intake LIKE '%" + intake + "%' ";
            SqlDataAdapter da = new SqlDataAdapter(newCmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "Intake_cadetsOfKDU");
            conn.Close();
            return ds;
        }

        public Image getProImg(String sno)
        {
            Image og = null;
            try
            {
                if (conn.State.ToString() == "Closed") { conn.Open(); }
                SqlCommand newCmd = conn.CreateCommand();
                newCmd.Connection = conn;
                newCmd.CommandType = CommandType.Text;
                newCmd.CommandText = " select imgPro from Cadet where SNO='" + sno + "' ";
                SqlDataReader dr = newCmd.ExecuteReader();
                byte[] byteArrayIn;

                while (dr.Read())
                {
                    byteArrayIn = dr["imgPro"] as byte[] ?? null;
                    if (byteArrayIn != null)
                    {
                        using (MemoryStream ms = new MemoryStream(byteArrayIn))
                        {
                            return Image.FromStream(ms);

                        }
                    }
                }
            }
            catch (Exception ) { }
            conn.Close();
            return og;
        }

        public Image getOLImg(String sno)
        {
            Image og = null;
            try
            {
                if (conn.State.ToString() == "Closed") { conn.Open(); }
                SqlCommand newCmd = conn.CreateCommand();
                newCmd.Connection = conn;
                newCmd.CommandType = CommandType.Text;
                newCmd.CommandText = " select imgOL2 from Cadet where SNO='" + sno + "' ";
                SqlDataReader dr = newCmd.ExecuteReader();
                byte[] byteArrayIn;

                while (dr.Read())
                {
                    byteArrayIn = dr["imgOL2"] as byte[] ?? null;
                    if (byteArrayIn != null)
                    {
                        using (MemoryStream ms = new MemoryStream(byteArrayIn))
                        {
                            return Image.FromStream(ms);

                        }
                    }
                }
            }
            catch (Exception e) { }
            conn.Close();
            return og;
        }

        public Image getALImg(String sno)
        {
            Image og = null;
            try
            {
                if (conn.State.ToString() == "Closed") { conn.Open(); }
                SqlCommand newCmd = conn.CreateCommand();
                newCmd.Connection = conn;
                newCmd.CommandType = CommandType.Text;
                newCmd.CommandText = " select imgAL2 from Cadet where SNO='" + sno + "' ";
                SqlDataReader dr = newCmd.ExecuteReader();
                byte[] byteArrayIn;

                while (dr.Read())
                {
                    byteArrayIn = dr["imgAL2"] as byte[] ?? null;
                    if (byteArrayIn != null)
                    {
                        using (MemoryStream ms = new MemoryStream(byteArrayIn))
                        {
                            return Image.FromStream(ms);

                        }
                    }
                }
            }
            catch (Exception e) { }
            conn.Close();
            return og;
        }

        //Get User details
        public DataSet getAllUser()
        {
            if (conn.State.ToString() == "Closed") { conn.Open(); }
            SqlCommand newCmd = conn.CreateCommand();
            newCmd.Connection = conn;
            newCmd.CommandType = CommandType.Text;
            newCmd.CommandText = " select ID,name,userType,rank,phone,email,userName,pwd from KDUuser ";
            SqlDataAdapter da = new SqlDataAdapter(newCmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "UserOfKDU");
            conn.Close();
            return ds;
        }

        public Image getUserProImg(String id)
        {
            Image og = null;
            try
            {
                if (conn.State.ToString() == "Closed") { conn.Open(); }
                SqlCommand newCmd = conn.CreateCommand();
                newCmd.Connection = conn;
                newCmd.CommandType = CommandType.Text;
                newCmd.CommandText = "select imgPrf from KDUuser where ID='" + id + "' ";
                SqlDataReader dr = newCmd.ExecuteReader();
                byte[] byteArrayIn;

                while (dr.Read())
                {
                    byteArrayIn = dr["imgPrf"] as byte[] ?? null;
                    if (byteArrayIn != null)
                    {
                        using (MemoryStream ms = new MemoryStream(byteArrayIn))
                        {
                            return Image.FromStream(ms);

                        }
                    }
                }
            }
            catch (Exception e) { }
            conn.Close();
            return og;
        }

        //Update User
        public bool updateUser(string id, string Name, string type, string rank, string phone, string email, string Uname, string pwd, byte[] imgPrf)
        {
            bool status = false;
            try
            {
                if (conn.State.ToString() == "Closed") { conn.Open(); }
                SqlCommand newCmd3 = conn.CreateCommand();
                newCmd3.Connection = conn;
                newCmd3.CommandType = CommandType.Text;
                newCmd3.CommandText = "update KDUuser set name='" + Name + "' ,userType='" + type + "',rank='" + rank + "',phone='" + phone + "',email='" + email + "',userName='" + Uname + "',pwd='" + pwd + "',imgPrf=@imgPrf where ID='" + id + "' ";
                newCmd3.Parameters.Add(new SqlParameter("@imgPrf", imgPrf));
                newCmd3.ExecuteNonQuery();
                status = true;
            }
            catch (SqlException e)
            {
                Console.WriteLine("DB error :" + e);
                status = false;
            }
            return status;
        }

        //Delete User
        public bool deleteUser(String Id)
        {
            bool status = false;
            if (conn.State.ToString() == "Closed") { conn.Open(); }
            SqlCommand newCmd = conn.CreateCommand();
            newCmd.Connection = conn;
            newCmd.CommandType = CommandType.Text;
            newCmd.CommandText = "delete from KDUuser where ID='" + Id + "' ";
            newCmd.ExecuteNonQuery();
            status = true;
            conn.Close();
            return status;
        }

        //Delete Cadet
        public bool deleteCadet(String Id)
        {
            bool status = false;
            if (conn.State.ToString() == "Closed") { conn.Open(); }
            SqlCommand newCmd = conn.CreateCommand();
            newCmd.Connection = conn;
            newCmd.CommandType = CommandType.Text;
            newCmd.CommandText = "delete from Cadet where SNO='" + Id + "' ";
            newCmd.ExecuteNonQuery();
            status = true;
            conn.Close();
            return status;
        }




        //-------------Attendence details 
        public DataSet getAttendance()
        {
            if (conn.State.ToString() == "Closed") { conn.Open(); }
            SqlCommand newCmd = conn.CreateCommand();
            newCmd.Connection = conn;
            newCmd.CommandType = CommandType.Text;
            newCmd.CommandText = " select SNO,ID,rank,troop,military,academic from Attendance ";
            SqlDataAdapter da = new SqlDataAdapter(newCmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "AttendanceOfKDU");
            conn.Close();
            return ds;
        }

        public DataSet getAttendanceSNO(String SNO)
        {
            if (conn.State.ToString() == "Closed") { conn.Open(); }
            SqlCommand newCmd = conn.CreateCommand();
            newCmd.Connection = conn;
            newCmd.CommandType = CommandType.Text;
            newCmd.CommandText = " select SNO,ID,rank,troop,military,academic from Attendance where SNO LIKE '%" + SNO + "%' ";
            SqlDataAdapter da = new SqlDataAdapter(newCmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "SNOAttendanceOfKDU");
            conn.Close();
            return ds;
        }

        public DataSet getAttendanceFromIntake(String intake)
        {
            if (conn.State.ToString() == "Closed") { conn.Open(); }
            SqlCommand newCmd = conn.CreateCommand();
            newCmd.Connection = conn;
            newCmd.CommandType = CommandType.Text;
            newCmd.CommandText = " select a.SNO,a.ID,a.rank,a.troop,a.military,a.academic from Attendance a,Cadet c where a.SNO=c.SNO AND c.intake LIKE '%" + intake + "%' ";
            SqlDataAdapter da = new SqlDataAdapter(newCmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "Intake_AttendanceOfKDU");
            conn.Close();
            return ds;
        }

        public Image getAttendImg(String id)
        {
            Image og = null;
            try
            {
                if (conn.State.ToString() == "Closed") { conn.Open(); }
                SqlCommand newCmd = conn.CreateCommand();
                newCmd.Connection = conn;
                newCmd.CommandType = CommandType.Text;
                newCmd.CommandText = "select hdImg from Attendance where ID='" + id + "'";
                SqlDataReader dr = newCmd.ExecuteReader();
                byte[] byteArrayIn;

                while (dr.Read())
                {
                    byteArrayIn = dr["hdImg"] as byte[] ?? null;
                    if (byteArrayIn != null)
                    {
                        using (MemoryStream ms = new MemoryStream(byteArrayIn))
                        {
                            return Image.FromStream(ms);

                        }
                    }
                }
            }
            catch (Exception e) { }
            conn.Close();
            return og;
        }

        public bool deleteAttendance(String Id)
        {
            bool status = false;
            if (conn.State.ToString() == "Closed") { conn.Open(); }
            SqlCommand newCmd = conn.CreateCommand();
            newCmd.Connection = conn;
            newCmd.CommandType = CommandType.Text;
            newCmd.CommandText = "delete from Attendance where ID='" + Id + "' ";
            newCmd.ExecuteNonQuery();
            status = true;
            conn.Close();
            return status;
        }

        public bool updateAtendance(string id, string rank, string troop, string military, string academic, byte[] imgHD)
        {
            bool status = false;
            try { 
            if (conn.State.ToString() == "Closed") { conn.Open(); }
            SqlCommand newCmd3 = conn.CreateCommand();
            newCmd3.Connection = conn;
            newCmd3.CommandType = CommandType.Text;
            newCmd3.CommandText = "update Attendance set rank='" + rank + "' ,troop='" + troop + "',military='" + military + "',academic='" + academic + "',hdImg=@imgHD where ID='" + id + "' ";
            newCmd3.Parameters.Add(new SqlParameter("@imgHD", imgHD));
            newCmd3.ExecuteNonQuery();
            status = true;
        }
            catch (SqlException e)
            {
                Console.WriteLine("DB error :" + e);
                status = false;
            }
            return status;
        }


        //------------Black Book details
        public DataSet getBlackBook()
        {
            if (conn.State.ToString() == "Closed") { conn.Open(); }
            SqlCommand newCmd = conn.CreateCommand();
            newCmd.Connection = conn;
            newCmd.CommandType = CommandType.Text;
            newCmd.CommandText = " select SNO,bookID,troop,year,serialNo,offence,punishment,marksDeducted from BlackBook ";
            SqlDataAdapter da = new SqlDataAdapter(newCmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "BlackBookOfKDU");
            conn.Close();
            return ds;
        }

        public DataSet getBlackBookSNO(String SNO)
        {
            if (conn.State.ToString() == "Closed") { conn.Open(); }
            SqlCommand newCmd = conn.CreateCommand();
            newCmd.Connection = conn;
            newCmd.CommandType = CommandType.Text;
            newCmd.CommandText = " select SNO,bookID,troop,year,serialNo,offence,punishment,marksDeducted from BlackBook where SNO LIKE '%" + SNO + "%' ";
            SqlDataAdapter da = new SqlDataAdapter(newCmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "SNOBlackBookOfKDU");
            conn.Close();
            return ds;
        }

        public DataSet getBlackBookFromIntake(String intake)
        {
            if (conn.State.ToString() == "Closed") { conn.Open(); }
            SqlCommand newCmd = conn.CreateCommand();
            newCmd.Connection = conn;
            newCmd.CommandType = CommandType.Text;
            newCmd.CommandText = "select b.SNO,b.bookID,b.troop,b.year,b.serialNo,b.offence,b.punishment,b.marksDeducted from BlackBook b,Cadet c where b.SNO=c.SNO AND c.intake LIKE '%" + intake + "%'";
            SqlDataAdapter da = new SqlDataAdapter(newCmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "intake_BlackBookOfKDU");
            conn.Close();
            return ds;
        }

        public Image getBlackBookImg(String id)
        {
            Image og = null;
            try
            {
                if (conn.State.ToString() == "Closed") { conn.Open(); }
                SqlCommand newCmd = conn.CreateCommand();
                newCmd.Connection = conn;
                newCmd.CommandType = CommandType.Text;
                newCmd.CommandText = "select hdImg from BlackBook where bookID='" + id + "'";
                SqlDataReader dr = newCmd.ExecuteReader();
                byte[] byteArrayIn;

                while (dr.Read())
                {
                    byteArrayIn = dr["hdImg"] as byte[] ?? null;
                    if (byteArrayIn != null)
                    {
                        using (MemoryStream ms = new MemoryStream(byteArrayIn))
                        {
                            return Image.FromStream(ms);

                        }
                    }
                }
            }
            catch (Exception e) { }
            conn.Close();
            return og;
        }

        public bool deleteBlackBook(String Id)
        {
            bool status = false;
            if (conn.State.ToString() == "Closed") { conn.Open(); }
            SqlCommand newCmd = conn.CreateCommand();
            newCmd.Connection = conn;
            newCmd.CommandType = CommandType.Text;
            newCmd.CommandText = "delete from BlackBook where bookID='" + Id + "' ";
            newCmd.ExecuteNonQuery();
            status = true;
            return status;
        }

        public bool updateBlackBook(string id, string trp, string yr, string no, string offe, string punish, string mrk, byte[] imgHD)
        {
            bool status = false;
            try { 
            if (conn.State.ToString() == "Closed") { conn.Open(); }
            SqlCommand newCmd3 = conn.CreateCommand();
            newCmd3.Connection = conn;
            newCmd3.CommandType = CommandType.Text;
            newCmd3.CommandText = "update BlackBook set troop='" + trp + "' ,year='" + yr + "',serialNo='" + no + "',offence='" + offe + "' ,punishment='" + punish + "',marksDeducted='" + mrk + "',hdImg=@imgHD where bookID='" + id + "' ";
            newCmd3.Parameters.Add(new SqlParameter("@imgHD", imgHD));
            newCmd3.ExecuteNonQuery();
            status = true;
        }
            catch (SqlException e)
            {
                Console.WriteLine("DB error :" + e);
                status = false;
            }
            return status;
        }


        //------------Charge Sheet details
        public DataSet getChargeSheet()
        {
            if (conn.State.ToString() == "Closed") { conn.Open(); }
            SqlCommand newCmd = conn.CreateCommand();
            newCmd.Connection = conn;
            newCmd.CommandType = CommandType.Text;
            newCmd.CommandText = " SELECT SNO,cSheetID,rank,troop,dateOfOffence,offence,noOfWit,witness,punish FROM ChargeSheet ";
            SqlDataAdapter da = new SqlDataAdapter(newCmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "ChargeSheetOfKDU");
            conn.Close();
            return ds;
        }

        public DataSet getChargeSheetSNO(String SNO)
        {
            if (conn.State.ToString() == "Closed") { conn.Open(); }
            SqlCommand newCmd = conn.CreateCommand();
            newCmd.Connection = conn;
            newCmd.CommandType = CommandType.Text;
            newCmd.CommandText = " SELECT SNO,cSheetID,rank,troop,dateOfOffence,offence,noOfWit,witness,punish FROM ChargeSheet where SNO LIKE '%" + SNO + "%' ";
            SqlDataAdapter da = new SqlDataAdapter(newCmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "SNOChargeSheetOfKDU");
            conn.Close();
            return ds;
        }

        public DataSet getChargeSheetFromIntake(String intake)
        {
            if (conn.State.ToString() == "Closed") { conn.Open(); }
            SqlCommand newCmd = conn.CreateCommand();
            newCmd.Connection = conn;
            newCmd.CommandType = CommandType.Text;
            newCmd.CommandText = " SELECT s.SNO,s.cSheetID,s.rank,s.troop,s.dateOfOffence,s.offence,s.noOfWit,s.witness,s.punish FROM ChargeSheet s,Cadet c where s.SNO=c.SNO AND c.intake LIKE '%" + intake + "%' ";
            SqlDataAdapter da = new SqlDataAdapter(newCmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "Intake_ChargeSheetOfKDU");
            conn.Close();
            return ds;
        }

        public Image getChrgSheetImg(String id)
        {
            Image og = null;
            try
            {
                if (conn.State.ToString() == "Closed") { conn.Open(); }
                SqlCommand newCmd = conn.CreateCommand();
                newCmd.Connection = conn;
                newCmd.CommandType = CommandType.Text;
                newCmd.CommandText = "select hdImg from ChargeSheet where cSheetID='" + id + "'";
                SqlDataReader dr = newCmd.ExecuteReader();
                byte[] byteArrayIn;

                while (dr.Read())
                {
                    byteArrayIn = dr["hdImg"] as byte[] ?? null;
                    if (byteArrayIn != null)
                    {
                        using (MemoryStream ms = new MemoryStream(byteArrayIn))
                        {
                            return Image.FromStream(ms);

                        }
                    }
                }
            }
            catch (Exception e) { }
            conn.Close();
            return og;
        }

        public bool deleteChargeSheet(String Id)
        {
            bool status = false;
            if (conn.State.ToString() == "Closed") { conn.Open(); }
            SqlCommand newCmd = conn.CreateCommand();
            newCmd.Connection = conn;
            newCmd.CommandType = CommandType.Text;
            newCmd.CommandText = "delete from ChargeSheet where cSheetID='" + Id + "' ";
            newCmd.ExecuteNonQuery();
            status = true;
            conn.Close();
            return status;
        }

        public bool updateChargeSheet(string id, string rank, string trp, string date, string offe, string noOfWit, string wit, string punish, byte[] imgHD)
        {
            bool status = false;
            try { 
            if (conn.State.ToString() == "Closed") { conn.Open(); }
            SqlCommand newCmd3 = conn.CreateCommand();
            newCmd3.Connection = conn;
            newCmd3.CommandType = CommandType.Text;
            newCmd3.CommandText = "update ChargeSheet set rank='" + rank + "' ,troop='" + trp + "',dateOfOffence='" + date + "',offence='" + offe + "' ,noOfWit='" + noOfWit + "',witness='" + wit + "',punish='" + punish + "',hdImg=@imgHD where cSheetID='" + id + "' ";
                newCmd3.Parameters.Add(new SqlParameter("@imgHD", imgHD));
                newCmd3.ExecuteNonQuery();
                status = true;
            }
            catch (SqlException e)
            {
                Console.WriteLine("DB error :" + e);
                status = false;
            }
            return status;
        }


        //------------Good Conduct details
        public DataSet getGdConduct()
        {
            if (conn.State.ToString() == "Closed") { conn.Open(); }
            SqlCommand newCmd = conn.CreateCommand();
            newCmd.Connection = conn;
            newCmd.CommandType = CommandType.Text;
            newCmd.CommandText = "select SNO,ID,rank,date,marks,reason From GoodConduct ";
            SqlDataAdapter da = new SqlDataAdapter(newCmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "GoodConductOfKDU");
            conn.Close();
            return ds;
        }

        public DataSet getGdConductSNO(String SNO)
        {
            if (conn.State.ToString() == "Closed") { conn.Open(); }
            SqlCommand newCmd = conn.CreateCommand();
            newCmd.Connection = conn;
            newCmd.CommandType = CommandType.Text;
            newCmd.CommandText = "select SNO,ID,rank,date,marks,reason From GoodConduct where SNO LIKE '%" + SNO + "%' ";
            SqlDataAdapter da = new SqlDataAdapter(newCmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "SNOGoodConductOfKDU");
            conn.Close();
            return ds;
        }

        public DataSet getGdConductFromIntake(String intake)
        {
            if (conn.State.ToString() == "Closed") { conn.Open(); }
            SqlCommand newCmd = conn.CreateCommand();
            newCmd.Connection = conn;
            newCmd.CommandType = CommandType.Text;
            newCmd.CommandText = "select g.SNO,g.ID,g.rank,g.date,g.marks,g.reason From GoodConduct g,Cadet c where g.SNO=c.SNO AND c.intake LIKE '%" + intake + "%' ";
            SqlDataAdapter da = new SqlDataAdapter(newCmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "Intake_GoodConductOfKDU");
            conn.Close();
            return ds;
        }

        public Image getGdConductImg(String id)
        {
            Image og = null;
            try
            {
                if (conn.State.ToString() == "Closed") { conn.Open(); }
                SqlCommand newCmd = conn.CreateCommand();
                newCmd.Connection = conn;
                newCmd.CommandType = CommandType.Text;
                newCmd.CommandText = "select hdImg from GoodConduct where ID='" + id + "'";
                SqlDataReader dr = newCmd.ExecuteReader();
                byte[] byteArrayIn;

                while (dr.Read())
                {
                    byteArrayIn = dr["hdImg"] as byte[] ?? null;
                    if (byteArrayIn != null)
                    {
                        using (MemoryStream ms = new MemoryStream(byteArrayIn))
                        {
                            return Image.FromStream(ms);

                        }
                    }
                }
            }
            catch (Exception e) { }
            conn.Close();
            return og;
        }

        public bool deleteGdConduct(String Id)
        {
            bool status = false;
            if (conn.State.ToString() == "Closed") { conn.Open(); }
            SqlCommand newCmd = conn.CreateCommand();
            newCmd.Connection = conn;
            newCmd.CommandType = CommandType.Text;
            newCmd.CommandText = "delete from GoodConduct where ID='" + Id + "' ";
            newCmd.ExecuteNonQuery();
            status = true;
            conn.Close();
            return status;
        }

        public bool updateGdConduct(string id, string rnk, string date, string mrks, string reason, byte[] imgHD)
        {
            bool status = false;
            try { 
            if (conn.State.ToString() == "Closed") { conn.Open(); }
            SqlCommand newCmd3 = conn.CreateCommand();
            newCmd3.Connection = conn;
            newCmd3.CommandType = CommandType.Text;
            newCmd3.CommandText = "update GoodConduct set rank='" + rnk + "' ,date='" + date + "',marks='" + mrks + "',reason='" + reason + "',hdImg=@imgHD  where ID='" + id + "' ";
            newCmd3.Parameters.Add(new SqlParameter("@imgHD", imgHD));
            newCmd3.ExecuteNonQuery();
            status = true;
        }
            catch (SqlException e)
            {
                Console.WriteLine("DB error :" + e);
                status = false;
            }
            return status;
        }


        //------------Incident Diary details
        public DataSet getInciDry()
        {
            if (conn.State.ToString() == "Closed") { conn.Open(); }
            SqlCommand newCmd = conn.CreateCommand();
            newCmd.Connection = conn;
            newCmd.CommandType = CommandType.Text;
            newCmd.CommandText = "select SNO,diaryID,troop,year,serialNO,date,cIncident,remarks from IncidentDiary ";
            SqlDataAdapter da = new SqlDataAdapter(newCmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "IncidentDiaryOfKDU");
            conn.Close();
            return ds;
        }

        public DataSet getInciDrySNO(String SNO)
        {
            if (conn.State.ToString() == "Closed") { conn.Open(); }
            SqlCommand newCmd = conn.CreateCommand();
            newCmd.Connection = conn;
            newCmd.CommandType = CommandType.Text;
            newCmd.CommandText = "select SNO,diaryID,troop,year,serialNO,date,cIncident,remarks from IncidentDiary where SNO LIKE '%" + SNO + "%' ";
            SqlDataAdapter da = new SqlDataAdapter(newCmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "SNOIncidentDiaryOfKDU");
            conn.Close();
            return ds;
        }

        public DataSet getInciDryFromIntake(String intake)
        {
            if (conn.State.ToString() == "Closed") { conn.Open(); }
            SqlCommand newCmd = conn.CreateCommand();
            newCmd.Connection = conn;
            newCmd.CommandType = CommandType.Text;
            newCmd.CommandText = "select i.SNO,i.diaryID,i.troop,i.year,i.serialNO,i.date,i.cIncident,i.remarks from IncidentDiary i,Cadet c where i.SNO=c.SNO AND c.intake LIKE '%" + intake + "%' ";
            SqlDataAdapter da = new SqlDataAdapter(newCmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "Intake_IncidentDiaryOfKDU");
            conn.Close();
            return ds;
        }

        public Image getInciDryImg(String id)
        {
            Image og = null;
            try
            {
                if (conn.State.ToString() == "Closed") { conn.Open(); }
                SqlCommand newCmd = conn.CreateCommand();
                newCmd.Connection = conn;
                newCmd.CommandType = CommandType.Text;
                newCmd.CommandText = "Select hdImg from IncidentDiary where diaryID='" + id + "'";
                SqlDataReader dr = newCmd.ExecuteReader();
                byte[] byteArrayIn;

                while (dr.Read())
                {
                    byteArrayIn = dr["hdImg"] as byte[] ?? null;
                    if (byteArrayIn != null)
                    {
                        using (MemoryStream ms = new MemoryStream(byteArrayIn))
                        {
                            return Image.FromStream(ms);

                        }
                    }
                }
            }
            catch (Exception e) { }
            conn.Close();
            return og;
        }

        public bool deleteInciDry(String Id)
        {
            bool status = false;
            if (conn.State.ToString() == "Closed") { conn.Open(); }
            SqlCommand newCmd = conn.CreateCommand();
            newCmd.Connection = conn;
            newCmd.CommandType = CommandType.Text;
            newCmd.CommandText = "delete from IncidentDiary where diaryID='" + Id + "' ";
            newCmd.ExecuteNonQuery();
            status = true;
            conn.Close();
            return status;
        }

        public bool updateInciDry(string id, string trp, string yr, string seNO, string date, string cInci, string mrks, byte[] imgHD)
        {
            bool status = false;
            try { 
            if (conn.State.ToString() == "Closed") { conn.Open(); }
            SqlCommand newCmd3 = conn.CreateCommand();
            newCmd3.Connection = conn;
            newCmd3.CommandType = CommandType.Text;
            newCmd3.CommandText = "update IncidentDiary set troop='" + trp + "' ,year='" + yr + "',serialNO='" + seNO + "',date='" + date + "',cIncident='" + cInci + "',remarks='" + mrks + "',hdImg=@imgHD  where diaryID='" + id + "' ";
            newCmd3.Parameters.Add(new SqlParameter("@imgHD", imgHD));
            newCmd3.ExecuteNonQuery();
            status = true;
        }
            catch (SqlException e)
            {
                Console.WriteLine("DB error :" + e);
                status = false;
            }
            return status;
        }



        //------------PT Test details
        public DataSet getPTtest()
        {
            if (conn.State.ToString() == "Closed") { conn.Open(); }
            SqlCommand newCmd = conn.CreateCommand();
            newCmd.Connection = conn;
            newCmd.CommandType = CommandType.Text;
            newCmd.CommandText = "select SNO,PTtestID,rank,troop,testName,marks,status,date from PTtest ";
            SqlDataAdapter da = new SqlDataAdapter(newCmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "PTtestOfKDU");
            conn.Close();
            return ds;
        }

        public DataSet getPTtestSNO(String SNO)
        {
            if (conn.State.ToString() == "Closed") { conn.Open(); }
            SqlCommand newCmd = conn.CreateCommand();
            newCmd.Connection = conn;
            newCmd.CommandType = CommandType.Text;
            newCmd.CommandText = "select SNO,PTtestID,rank,troop,testName,marks,status,date from PTtest where SNO LIKE '%" + SNO + "%' ";
            SqlDataAdapter da = new SqlDataAdapter(newCmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "SNOPTtestOfKDU");
            conn.Close();
            return ds;
        }

        public DataSet getPTtestFromIntake(String intake)
        {
            if (conn.State.ToString() == "Closed") { conn.Open(); }
            SqlCommand newCmd = conn.CreateCommand();
            newCmd.Connection = conn;
            newCmd.CommandType = CommandType.Text;
            newCmd.CommandText = "select p.SNO,p.PTtestID,p.rank,p.troop,p.testName,p.marks,p.status,p.date from PTtest p,Cadet c where p.SNO=c.SNO AND c.intake LIKE '%" + intake + "%' ";
            SqlDataAdapter da = new SqlDataAdapter(newCmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "Intake_PTtestOfKDU");
            conn.Close();
            return ds;
        }

        public Image getPTtestImg(String id)
        {
            Image og = null;
            try
            {
                if (conn.State.ToString() == "Closed") { conn.Open(); }
                SqlCommand newCmd = conn.CreateCommand();
                newCmd.Connection = conn;
                newCmd.CommandType = CommandType.Text;
                newCmd.CommandText = "Select hdImg from PTtest where PTtestID='" + id + "'";
                SqlDataReader dr = newCmd.ExecuteReader();
                byte[] byteArrayIn;

                while (dr.Read())
                {
                    byteArrayIn = dr["hdImg"] as byte[] ?? null;
                    if (byteArrayIn != null)
                    {
                        using (MemoryStream ms = new MemoryStream(byteArrayIn))
                        {
                            return Image.FromStream(ms);

                        }
                    }
                }
            }
            catch (Exception e) { }
            conn.Close();
            return og;
        }

        public bool deletePTtest(String Id)
        {
            bool status = false;
            if (conn.State.ToString() == "Closed") { conn.Open(); }
            SqlCommand newCmd = conn.CreateCommand();
            newCmd.Connection = conn;
            newCmd.CommandType = CommandType.Text;
            newCmd.CommandText = "delete from PTtest where PTtestID='" + Id + "' ";
            newCmd.ExecuteNonQuery();
            status = true;
            conn.Close();
            return status;
        }

        public bool updatePTtest(string id, string rnk, string trp, string test, string mrks, string state, string date, byte[] imgHD)
        { 
            bool status = false;
            try
            {
                if (conn.State.ToString() == "Closed") { conn.Open(); }
            SqlCommand newCmd3 = conn.CreateCommand();
            newCmd3.Connection = conn;
            newCmd3.CommandType = CommandType.Text;
            newCmd3.CommandText = "update PTtest set rank='" + rnk + "' ,troop='" + trp + "',testName='" + test + "',marks='" + mrks + "',status='" + state + "',date='" + date + "',hdImg=@imgHD  where PTtestID='" + id + "' ";
            newCmd3.Parameters.Add(new SqlParameter("@imgHD", imgHD));
            newCmd3.ExecuteNonQuery();
            status = true;
        }
            catch (SqlException e)
            {
                Console.WriteLine("DB error :" + e);
                status = false;
            }
            return status;
        }



        //------------Sick Chart details
        public DataSet getSickChrt()
        {
            if (conn.State.ToString() == "Closed") { conn.Open(); }
            SqlCommand newCmd = conn.CreateCommand();
            newCmd.Connection = conn;
            newCmd.CommandType = CommandType.Text;
            newCmd.CommandText = " select SNO,chartID,rank,troop,year,sickness,category,date from SickChart ";
            SqlDataAdapter da = new SqlDataAdapter(newCmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "SickChartOfKDU");
            conn.Close();
            return ds;
        }

        public DataSet getSickChrtSNO(String SNO)
        {
            if (conn.State.ToString() == "Closed") { conn.Open(); }
            SqlCommand newCmd = conn.CreateCommand();
            newCmd.Connection = conn;
            newCmd.CommandType = CommandType.Text;
            newCmd.CommandText = " select SNO,chartID,rank,troop,year,sickness,category,date from SickChart where SNO LIKE '%" + SNO + "%' ";
            SqlDataAdapter da = new SqlDataAdapter(newCmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "SNOSickChartOfKDU");
            conn.Close();
            return ds;
        }

        public DataSet getSickChrtFromIntake(String intake)
        {
            if (conn.State.ToString() == "Closed") { conn.Open(); }
            SqlCommand newCmd = conn.CreateCommand();
            newCmd.Connection = conn;
            newCmd.CommandType = CommandType.Text;
            newCmd.CommandText = " select s.SNO,s.chartID,s.rank,s.troop,s.year,s.sickness,s.category,s.date from SickChart s,Cadet c where s.SNO=c.SNO AND c.intake LIKE '%" + intake + "%' ";
            SqlDataAdapter da = new SqlDataAdapter(newCmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "Intake_SickChartOfKDU");
            conn.Close();
            return ds;
        }

        public Image getSickImg(String id)
        {
            Image og = null;
            try
            {
                if (conn.State.ToString() == "Closed") { conn.Open(); }
                SqlCommand newCmd = conn.CreateCommand();
                newCmd.Connection = conn;
                newCmd.CommandType = CommandType.Text;
                newCmd.CommandText = "select hdImg from SickChart where ChartID='" + id + "'";
                SqlDataReader dr = newCmd.ExecuteReader();
                byte[] byteArrayIn;

                while (dr.Read())
                {
                    byteArrayIn = dr["hdImg"] as byte[] ?? null;
                    if (byteArrayIn != null)
                    {
                        using (MemoryStream ms = new MemoryStream(byteArrayIn))
                        {
                            return Image.FromStream(ms);

                        }
                    }
                }
            }
            catch (Exception e) { }
            conn.Close();
            return og;
        }

        public bool deleteSickChrt(String Id)
        {
            bool status = false;
            if (conn.State.ToString() == "Closed") { conn.Open(); }
            SqlCommand newCmd = conn.CreateCommand();
            newCmd.Connection = conn;
            newCmd.CommandType = CommandType.Text;
            newCmd.CommandText = "delete from SickChart where chartID='" + Id + "' ";
            newCmd.ExecuteNonQuery();
            status = true;
            conn.Close();
            return status;
        }

        public bool updateSickChrt(string id, string rnk, string trp, string yr, string sick, string cat, string date, byte[] imgHD)
        {
            bool status = false;
            try { 
            if (conn.State.ToString() == "Closed") { conn.Open(); }
            SqlCommand newCmd3 = conn.CreateCommand();
            newCmd3.Connection = conn;
            newCmd3.CommandType = CommandType.Text;
            newCmd3.CommandText = "update SickChart set rank='" + rnk + "' ,troop='" + trp + "',year='" + yr + "',sickness='" + sick + "',category='" + cat + "',date='" + date + "',hdImg=@imgHD  where chartID='" + id + "' ";
            newCmd3.Parameters.Add(new SqlParameter("@imgHD", imgHD));
            newCmd3.ExecuteNonQuery();
            status = true;
        }
            catch (SqlException e)
            {
                Console.WriteLine("DB error :" + e);
                status = false;
            }
            return status;
        }


        //------------Work Ass details
        public DataSet getWorkAss()
        {
            if (conn.State.ToString() == "Closed") { conn.Open(); }
            SqlCommand newCmd = conn.CreateCommand();
            newCmd.Connection = conn;
            newCmd.CommandType = CommandType.Text;
            newCmd.CommandText = " Select SNO,workAssID,troop,date,drill,pt,wTraning,goodConduct,fileBook,totalMrk from WorkAss";
            SqlDataAdapter da = new SqlDataAdapter(newCmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "WorkAssOfKDU");
            conn.Close();
            return ds;
        }

        public DataSet getWorkAssSNO(String SNO)
        {
            if (conn.State.ToString() == "Closed") { conn.Open(); }
            SqlCommand newCmd = conn.CreateCommand();
            newCmd.Connection = conn;
            newCmd.CommandType = CommandType.Text;
            newCmd.CommandText = "Select SNO,workAssID,troop,date,drill,pt,wTraning,goodConduct,fileBook,totalMrk from WorkAss where SNO LIKE '%" + SNO + "%' ";
            SqlDataAdapter da = new SqlDataAdapter(newCmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "SNOWorkAssOfKDU");
            conn.Close();
            return ds;
        }

        public DataSet getWorkAssFromIntake(String intake)
        {
            if (conn.State.ToString() == "Closed") { conn.Open(); }
            SqlCommand newCmd = conn.CreateCommand();
            newCmd.Connection = conn;
            newCmd.CommandType = CommandType.Text;
            newCmd.CommandText = "select w.SNO,w.workAssID,w.troop,w.date,w.drill,w.pt,w.wTraning,w.goodConduct,w.fileBook,w.totalMrk from WorkAss w,Cadet c where w.SNO=c.SNO AND c.intake LIKE '%"+intake+"%' ";
            SqlDataAdapter da = new SqlDataAdapter(newCmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "Intake_WorkAssOfKDU");
            conn.Close();
            return ds;
        }

        public Image getWorkAssImg(String id)
        {
            Image og = null;
            try
            {
                if (conn.State.ToString() == "Closed") { conn.Open(); }
                SqlCommand newCmd = conn.CreateCommand();
                newCmd.Connection = conn;
                newCmd.CommandType = CommandType.Text;
                newCmd.CommandText = "select hdImg from WorkAss where workAssID='" + id + "'";
                SqlDataReader dr = newCmd.ExecuteReader();
                byte[] byteArrayIn;

                while (dr.Read())
                {
                    byteArrayIn = dr["hdImg"] as byte[] ?? null;
                    if (byteArrayIn != null)
                    {
                        using (MemoryStream ms = new MemoryStream(byteArrayIn))
                        {
                            return Image.FromStream(ms);

                        }
                    }
                }
            }
            catch (Exception e) { }
            conn.Close();
            return og;
        }

        public bool deleteWorkAss(String Id)
        {
            bool status = false;
            if (conn.State.ToString() == "Closed") { conn.Open(); }
            SqlCommand newCmd = conn.CreateCommand();
            newCmd.Connection = conn;
            newCmd.CommandType = CommandType.Text;
            newCmd.CommandText = "delete from WorkAss where workAssID='" + Id + "' ";
            newCmd.ExecuteNonQuery();
            status = true;
            conn.Close();
            return status;
        }

        public bool updateWorkAss(string id, string trp, string date, string drill, string pt, string wt, string gc, string fb, string tm, byte[] imgHD)
        {
            bool status = false;
            try {
                if (conn.State.ToString() == "Closed") { conn.Open(); }
                SqlCommand newCmd3 = conn.CreateCommand();
                newCmd3.Connection = conn;
                newCmd3.CommandType = CommandType.Text;
                newCmd3.CommandText = "update WorkAss set troop='" + trp + "' ,date='" + date + "',drill='" + drill + "',pt='" + pt + "',wTraning='" + wt + "',goodConduct='" + gc + "',fileBook='" + fb + "',totalMrk='" + tm + "', hdImg=@imgHD  where workAssID='" + id + "' ";
                newCmd3.Parameters.Add(new SqlParameter("@imgHD", imgHD));
                newCmd3.ExecuteNonQuery();
                status = true;
            }
            catch (SqlException e)
            {
                Console.WriteLine("DB error :" + e);
                status = false;
            }
            return status;
        }
    
    


    }
}
