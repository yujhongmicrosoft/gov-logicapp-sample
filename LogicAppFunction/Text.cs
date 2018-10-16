using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicAppFunction
{
    public class DocumentsRequest
    {
        public Text [] documents { get; set; }
    }

    public class DocumentsResponse
    {
        public KeyWords [] documents { get; set; }
    }

    public class Text
    {
        public string language { get; set; }
        public string id { get; set; }
        public string text { get; set; }
    }

    public class KeyWords
    {
        public string[] keyPhrases { get; set; }
        public string id { get; set; }
    }
}
