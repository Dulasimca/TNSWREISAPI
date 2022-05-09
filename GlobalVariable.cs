using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TNSWREISAPI
{
    public class GlobalVariable
    {
        //Testing connection
        public const string ConnectionString = "data source=180.179.49.72;initial catalog=TNSWREIS;user id = sqladmin; password =sql@svc&ac!72;";
        public const string ConnectionStringBiometric = "data source=180.179.49.72;initial catalog=etimetracklite1;user id = sqladmin; password =sql@svc&ac!72;";
        public const string ConnectionStringForFaceReadingPostgreSQL = "data source=180.179.49.72;initial catalog=etimetracklite1;user id = sqladmin; password =sql@svc&ac!72;";
        public const string FolderPath = "E://Angular//TNSWREIS//src//assets//layout//";

       //Live connection
       // public const string ConnectionString = "data source=localhost;initial catalog=TNSWREIS;user id =sqladmin; password =sql@svc&ac!72;";
       // public const string ConnectionStringBiometric = "data source=localhost;initial catalog=etimetracklite1;user id = sqladmin; password =sql@svc&ac!72;";
       // public const string ConnectionStringForFaceReadingPostgreSQL = "data source=localhost;initial catalog=etimetracklite1;user id = sqladmin; password =sql@svc&ac!72;";
      //  public const string FolderPath = "C://LocalRepository//TestingSite//TNADWUI//assets//layout//";
  
    }
}
