using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrzychodniaApp.Exceptions
{
    [Serializable]
    public class UnequalCollectionsException : Exception
    {
        public UnequalCollectionsException() { }
        public UnequalCollectionsException(string message) : base(message) { }
        public UnequalCollectionsException(string message, Exception inner) : base(message, inner) { }
        protected UnequalCollectionsException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
