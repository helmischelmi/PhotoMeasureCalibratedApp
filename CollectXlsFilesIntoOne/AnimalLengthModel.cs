namespace CollectXlsFilesIntoOne
{
    public class AnimalLengthModel
    {
        public string Individuum { get; set; }

        public DateTime Aufnahmedatum { get; set; }

        public double Length { get; set; }

        public AnimalLengthModel(string individuum, DateTime aufnahmedatum, double length)
        {
            Individuum = individuum;
            Aufnahmedatum = aufnahmedatum;
            Length = length;
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


        public override string ToString()
        {
            return $"{Individuum} - {Aufnahmedatum} - {Length:F2}";
        }
    }
}
