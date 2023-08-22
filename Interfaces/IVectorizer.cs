using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RecommendationSystemInterface.Interfaces
{
    internal interface IVectorizer<T>
    {
        float[] Vectorize(T data);
    }


    interface IRowVectorizer
    {
        float[] VectorizeRow();
    }


    interface IUserVectorizer
    {
        float[] VectorizeUser(User user);
    }


    interface IUserAndRowVectorizer : IUserVectorizer, IRowVectorizer { }
}
