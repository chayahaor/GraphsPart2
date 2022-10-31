using System;

namespace GraphsClassProject
{
    //TODO: change into struct
    public class GraphInfo
    {
        public String name { get; set; }
        public bool weight;
        public bool direct;

        public GraphInfo(String name, bool weight, bool direct)
        {
            this.name = name;
            this.weight = weight;
            this.direct = direct;
        }
    }
}