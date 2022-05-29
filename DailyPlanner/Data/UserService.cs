using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Text.RegularExpressions;
using System.Net.Mail;
using DailyPlanner.Models;
using System.Data;

namespace DailyPlanner.Data
{
    class UserService
    {
        public static void AddUser(User user)
        {
            using (DailyPlannerDB plannerDB = new DailyPlannerDB())
            {
                plannerDB.Users.Add(user);
                plannerDB.SaveChanges();
            }
        }

        public static User GetUserbyLoginPassword(string login, string password)
        {
            using (DailyPlannerDB plannerDB = new DailyPlannerDB())
            {
                DailyPlannerDB.CurrentUser = plannerDB.Users.First(u => u.Login == login && u.Password == password);
                return DailyPlannerDB.CurrentUser;
            }
        }

        public static User GetCurrentUser()
        {
            using (DailyPlannerDB dB = new DailyPlannerDB())
            {

                return dB.Users.Where(u => u.Id == DailyPlannerDB.CurrentUser.Id).FirstOrDefault();
            }
        }


        //public static void UpdateUser(User user, string login)
        //{
        //    DailyPlannerDB dB = new DailyPlannerDB();
        //    var temp = dB.Users.SingleOrDefault(u => u.Login == login);
        //    if (temp != null)
        //    {
        //        temp.Login = user.Login;
        //        temp.Password = user.Password;
        //        temp.Email = user.Email;
        //        //temp.PasswordConfirmation = user.PasswordConfirmation;
        //        dB.SaveChanges();
        //    }
        //}

        //public void GetUserforValidation(User user)
        //{
        //    try
        //    {
        //        if (user.Login == String.Empty || user.Login == null || Regex.IsMatch(user.Login, @"\p{IsCyrillic}"))
        //        {
        //            //throw new EmptyCyrilicLoginException();
        //        }
        //    }
        //    catch (EmptyCyrilicLoginException err)
        //    {
        //        EmptyCyrilicLoginExceptionFault ex = new EmptyCyrilicLoginExceptionFault();
        //        ex.Result = false;
        //        ex.Message = err.Message;
        //        ex.Description = "Htos' naplyguv, ajajaj ((((";

        //        throw new FaultException<EmptyCyrilicLoginExceptionFault>(ex, new FaultReason(" field empty or has cyrilyc characters"));
        //    }

        //    try
        //    {

        //        if (user.Password != user.PasswordConfirmation)
        //        {
        //            throw new PasswordConfirmationException();
        //        }
        //    }
        //    catch (PasswordConfirmationException err)
        //    {
        //        PasswordConfirmationExceptionFault ex = new PasswordConfirmationExceptionFault();
        //        ex.Result = false;
        //        ex.Message = err.Message;
        //        ex.Description = "Htos' naplyguv, ajajaj ((((";

        //        throw new FaultException<PasswordConfirmationExceptionFault>(ex, new FaultReason(" pass!=pass conf"));
        //    }

        //    try
        //    {
        //        if (user.Password.Length >= 6)
        //        {
        //            throw new PasswordIndexOutOfRangeException();
        //        }
        //    }
        //    catch (PasswordIndexOutOfRangeException err)
        //    {
        //        PasswordIndexOutOfRangeExceptionFault ex = new PasswordIndexOutOfRangeExceptionFault();
        //        ex.Result = false;
        //        ex.Message = err.Message;
        //        ex.Description = "Htos' naplyguv, ajajaj ((((";

        //        throw new FaultException<PasswordIndexOutOfRangeExceptionFault>(ex, new FaultReason(" over 6 characters in pass"));
        //    }

        //    try
        //    {
        //        if (Regex.IsMatch(user.Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$"))
        //        {
        //            throw new PasswordSpecificCharactersException();
        //        }
        //    }
        //    catch (PasswordSpecificCharactersException err)
        //    {
        //        PasswordSpecificCharactersExceptionFault ex = new PasswordSpecificCharactersExceptionFault();
        //        ex.Result = false;
        //        ex.Message = err.Message;
        //        ex.Description = "Htos' naplyguv, ajajaj ((((";

        //        throw new FaultException<PasswordSpecificCharactersExceptionFault>(ex, new FaultReason(" specific characters in pass"));
        //    }

        //    try
        //    {
        //        if (user.Email == null || String.IsNullOrEmpty(user.Email))
        //        {
        //            MailAddress m = new MailAddress(user.Email);
        //            throw new EmailFormatException();
        //        }
        //    }
        //    catch (EmailFormatException err)
        //    {
        //        EmailFormatExceptionFault ex = new EmailFormatExceptionFault();
        //        ex.Result = false;
        //        ex.Message = err.Message;
        //        ex.Description = "Htos' naplyguv, ajajaj ((((";

        //        throw new FaultException<EmailFormatExceptionFault>(ex, new FaultReason(" wrond email format"));
        //    }

        //    try
        //    {
        //        if (String.IsNullOrEmpty(user.Telephone))
        //        {
        //            var r = new Regex(@"^\(?([0-9]{3})\)?[-.●]?([0-9]{3})[-.●]?([0-9]{4})$");
        //            r.IsMatch(user.Telephone);
        //            throw new PhoneFormatException();
        //        }

        //    }
        //    catch (PhoneFormatException err)
        //    {
        //        PhoneFormatExceptionFault ex = new PhoneFormatExceptionFault();
        //        ex.Result = false;
        //        ex.Message = err.Message;
        //        ex.Description = "Htos' naplyguv, ajajaj ((((";

        //        throw new FaultException<PhoneFormatExceptionFault>(ex, new FaultReason(" wrong phone format"));
        //    }

        //}

        //public void GetLoginUserforValidation(string login, string password)
        //{
        //    using (DailyPlannerDB dB = new DailyPlannerDB())
        //    {
        //        try
        //        {
        //            var t = dB.Users.FirstOrDefault(u => u.Login == login);
        //            if (t == null)
        //            {
        //                throw new UserExistsInDatabaseException();
        //            }
        //        }
        //        catch (UserExistsInDatabaseException err)
        //        {
        //            MyExceptionFault ex = new MyExceptionFault();
        //            ex.Result = false;
        //            ex.Message = err.Message;
        //            ex.Description = "Htos' naplyguv, ajajaj ((((";
        //            throw new FaultException<MyExceptionFault>(ex, new FaultReason(" user not exists in database"));
        //        }

        //        try
        //        {
        //            foreach (var item in dB.Users)
        //            {
        //                if (item.Password == password)
        //                {
        //                    throw new PasswordEqualsInDataBaseException();
        //                }
        //            }
        //        }
        //        catch (PasswordEqualsInDataBaseException err)
        //        {

        //            PasswordEqualsInDataBaseExceptionFault ex = new PasswordEqualsInDataBaseExceptionFault();
        //            ex.Result = false;
        //            ex.Message = err.Message;
        //            ex.Description = "Htos' naplyguv, ajajaj ((((";
        //            throw new FaultException<PasswordEqualsInDataBaseExceptionFault>(ex, new FaultReason(" passwords not equals"));
        //        }

        //    }
        //}

    }
}