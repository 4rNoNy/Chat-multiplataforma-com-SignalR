using Microsoft.ML.Data;

namespace ReactMXHApi6.MLDataStructures
{
    public class PostData
    {
        [LoadColumn(0)]
        public string Id;

        [LoadColumn(1)]
        public string Category; 

        [LoadColumn(2)]
        public string NoiDung;
    }
}
