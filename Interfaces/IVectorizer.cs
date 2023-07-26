using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Data.Analysis;

namespace RecommendationSystem.Interfaces
{
    internal interface IVectorizer<T>
    {
        float[] Vectorize(T data, DataFrame dataFrame);
    }


    interface IRowVectorizer
    {
        float[] VectorizeRow();
    }


    interface IUserVectorizer
    {
        float[] VectorizeUser(User user, DataFrame dataFrame);
    }


    interface IUserAndRowVectorizer : IUserVectorizer, IRowVectorizer { }
}
