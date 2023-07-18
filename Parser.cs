using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationSystem
{
    abstract class Parser
    {
        public abstract void Parse();
    }




    abstract class FileParser : Parser
    {
        public string Path { get; set; }
    }


    class JsonParser : FileParser
    {
        public override void Parse()
        {

        }
    }

    class NotesParser : FileParser
    {
        public override void Parse()
        {

        }
    }




    abstract class WebParser : Parser
    {
        public string Url { get; set; }
    }


    class SisSubjectParser : WebParser
    {
        public override void Parse()
        {

        }
    }

    class KosSubjectParser : WebParser
    {
        public override void Parse()
        {

        }
    }
}
