namespace RecommendationSystemInterface.Interfaces
{
    internal interface IVectorizer<T>
    {
        float[] Vectorize(T data);
    }




    public interface IUserVectorizer
    {
        float[] VectorizeUser(User user, float[][] dataMatrix);
    }


    /// <summary>
    /// Takes subjects rated by user and averages them out into a single vector that should
    /// theoretically revolve around its preferences in the vector space.
    /// </summary>
    class SisUserVectorizer : IUserVectorizer
    {
        public float[] VectorizeUser(User user, float[][] dataMatrix)
        {
            float[] userVector = new float[dataMatrix[0].Length];

            if (user is not SisUser sisUser) { return userVector; }

            foreach (int index in sisUser.Favourites)
            {
                if (index >= dataMatrix.GetLength(0)) { continue; }

                for (int j = 0; j < userVector.Length; j++)
                {
                    userVector[j] += dataMatrix[index][j];
                }
            }

            foreach (int index in sisUser.WishList)
            {
                if (index >= dataMatrix.GetLength(0)) { continue; }

                for (int j = 0; j < userVector.Length; j++)
                {
                    userVector[j] += dataMatrix[index][j];
                }
            }

            int numOfRankedItems = sisUser.Favourites.Length + sisUser.WishList.Length;

            if (numOfRankedItems == 0) { return userVector; }

            for (int i = 0; i < userVector.Length; i++)
            {
                userVector[i] /= numOfRankedItems;
            }

            return userVector;
        }
    }


    /// <summary>
    /// Creates a vector as long as there are known items and fills in the user's ratings.
    /// </summary>
    class MovieDbsUserVectorizer : IUserVectorizer
    {
        public float[] VectorizeUser(User user, float[][] dataMatrix)
        {
            float[] userVector = new float[dataMatrix[0].Length];

            if (user is not MovieDbsUser movieUser) { return userVector; }

            foreach (var itemAndRating in movieUser.UserItemRatings)
            {
                userVector[itemAndRating.Key] = itemAndRating.Value;
            }

            return userVector;
        }
    }
}
