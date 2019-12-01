using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Vimachem.Hackathon
{
    public class Tube
    {


        //public Tube(int tubeID, double length)
        //{
        //    TubeID = tubeID;
        //    Length = length;
        //    Cuts = 0;
        //}
        public Tube(int tubeID, double length, double outerDiameter, double distortion)
        {
            TubeID = tubeID;
            Length = length;
            Distortion = distortion;
            OuterDiameter = outerDiameter;
            Cuts = 0;
        }

        public int TubeID { get; set; }

        public double Length { get; set; }
        public int Cuts { get; set; }
        public double Distortion { get; set; }
        public double OuterDiameter { get; set; }


        public void Cut()
        {
            Cuts++;
            switch (Cuts)
            {
                case 1:
                    Length -= 300 / 1000.0;
                    break;
                case 2:
                    Length -= 200 / 1000.0;
                    break;
                case 3:
                    Length -= 150 / 1000.0;
                    break;
            }
        }
        public bool Inspect()
        {
           
            return (Production.Random.NextDouble() < 0.5);
        }

    }
}
