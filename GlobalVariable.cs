using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TNSWREISAPI
{
    public class GlobalVariable
    {
        //Testing connection
        public const string ConnectionString = "data source=183.82.34.129;initial catalog=TNSWREIS;user id = sqladmin; password =sql@svc&ac!147;";
        public const string ConnectionStringBiometric = "data source=183.82.34.129;initial catalog=etimetracklite1;user id = sqladmin; password =sql@svc&ac!147;";
        public const string ConnectionStringForFaceReadingPostgreSQL = "data source=183.82.34.129;initial catalog=etimetracklite1;user id = sqladmin; password =sql@svc&ac!147;";
        public const string FolderPath = "E://TestandVerification//Projects//TNSWREIS//WebAPP//src//assets//layout//";

        //Live connection
        //public const string ConnectionString = "data source=localhost;initial catalog=TNADWHMS;user id =sa; password =Tryme@43$#7&!2;";
        //public const string ConnectionStringBiometric = "data source=localhost;initial catalog=etimetracklite1;user id = sa; password =Tryme@43$#7&!2;";
        //public const string ConnectionStringForFaceReadingPostgreSQL = "data source=localhost;initial catalog=etimetracklite1;user id = sa; password =Tryme@43$#7&!2;";
        //public const string FolderPath = "D://Respository//Live//TNADWUI//assets//layout//";
    }
}
