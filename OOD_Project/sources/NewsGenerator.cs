using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_Project
{
    public class NewsGenerator
    {
        public List<IMediable> Medias { get; set; }
        public List<IReportable> Reportable { get; set; }

        public int i = 0; //index for Medias
        public int j = 0; //index for Reportable
        public NewsGenerator(List<IMediable> media, List<IReportable> reports)
        {
            this.Medias = media;
            this.Reportable = reports;
        }
        public void GenerateNextNews()
        {
            if(IsNewsAvailable())
            {
                Console.WriteLine(Reportable[j].Report(Medias[i]));
            }
            else
            {
                Console.WriteLine("No more data to report!");
            }

            CalculateNext();   
            

        }        
        public bool IsNewsAvailable()
        {
            return i < Medias.Count && j < Reportable.Count;
        }

        void CalculateNext()
        {
            i++;
            if(i == Medias.Count)
            {
                j++;
                i = 0;
            }
        }
    }
}
