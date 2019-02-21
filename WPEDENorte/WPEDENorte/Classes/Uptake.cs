using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPEDENorte.Classes
{
    public class Uptake
    {
        public class RequestApi
        {
            public RequestApi()
            {
                User = Utilities.CorrespondentId;
                Session = Utilities.Session;
            }

            public int Session { get; set; }
            public int User { get; set; }
            public object Data { get; set; }
        }

        public class ResponseApi
        {
            public int CodeError { get; set; }
            public string Message { get; set; }
            public object Data { get; set; }
        }

        public class Response
        {
            public bool IsSuccess { get; set; }
            public string Message { get; set; }
            public object Result { get; set; }
        }

    }
}
