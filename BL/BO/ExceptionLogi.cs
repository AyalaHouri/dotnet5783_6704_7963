
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    [Serializable]
    public class ExceptionLogi : Exception
    {
        public ExceptionLogi() { }
        public ExceptionLogi(string message) : base(message) { }
        public ExceptionLogi(string message, ExceptionLogi inner) : base(message, inner) { }
        protected ExceptionLogi(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

}

