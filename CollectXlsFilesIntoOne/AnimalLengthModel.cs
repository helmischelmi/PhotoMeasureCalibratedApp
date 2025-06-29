using System;

namespace CollectXlsFilesIntoOne
{
    public class AnimalLengthModel
    {
        public string Individuum { get; set; }

        public DateTime Aufnahmedatum { get; set; }

        public double Length { get; set; }

        public int LengthQuality { get; set; }

        public AnimalLengthModel(string individuum, DateTime aufnahmedatum, double length, int lengthQuality)
        {
            Individuum = individuum;
            Aufnahmedatum = aufnahmedatum;
            Length = length;
            LengthQuality = lengthQuality;
        }

        public AnimalLengthModel()
        {
           
        }

        public void AddIndividuum(string ind)
        {
            Individuum=ind;
        }

        public void AddDatum(string datum)
        {
            Aufnahmedatum= Convert.ToDateTime(datum);
        }

        public void AddLength(string length)
        {
            Length= Convert.ToDouble(length);
        }

        public void AddLengthQuality(string length)
        {
            LengthQuality= Convert.ToInt32(LengthQuality);
        }

        public override string ToString()
        {
            return $"{Individuum} - {Aufnahmedatum} - {Length:F2}";
        }
    }
}
