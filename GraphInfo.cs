using System;

namespace GraphsClassProject
{
    //TODO: change into struct
    public class GraphInfo
    {
        public String Name { get; set; }
        public bool Weight;
        public bool Direct;

        public GraphInfo(String name, bool weight, bool direct)
        {
            this.Name = name;
            this.Weight = weight;
            this.Direct = direct;
        }

        public override string ToString()
        {
            return Name + " is " + Weight + " weighted and " + Direct + " directed";
        }
    }
}