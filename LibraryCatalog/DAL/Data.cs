namespace LibraryCatalog.DAL
{
    public class Data
    {//כתובת החיבור
        private string _connectionString = "SERVER =PINCHAS\\SA;initial catalog = LibraryCatalog ; user id = SA;" +
            "PASSWORD = 1234;TrustServerCertificate = true";
        //משתנה חדש וסטטי של הדאטא
        private static Data _mydata;
        //משתנה חדש שמתאים לדאטאבייס
        private DataLayer mydatalayer;
        //קונסטרקטור שמייצר מסמך  דאטאטבייס בכל ריצה
        private Data()
        {
            mydatalayer = new DataLayer(_connectionString);
        }
        //פונקציה שדררכה נוכל לגשת למסמך עם חיבור
        public static DataLayer Get
        {//במידה ולא קיים חיבור ניצור אותו
            get
            {
                if (_mydata == null)
                {
                    _mydata = new Data();
                }//ככל וכבר קיים חיבור נחזיר את המסמך שכבר קיבל אותו למשתמש
                return _mydata.mydatalayer;
            }
        }
    }
}