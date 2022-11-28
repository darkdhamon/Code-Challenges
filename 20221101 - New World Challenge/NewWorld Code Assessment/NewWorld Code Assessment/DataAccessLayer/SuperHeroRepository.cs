namespace NewWorld_Code_Assessment.DataAccessLayer
{
    public static class SuperHeroRepository
    {
        static SuperHeroRepository()
        {
            Superheroes.AddRange(new []
            {
                new Superhero()
                {
                    ID = 1,
                    Name = "The Flash - Barry Allen"
                },
                new Superhero()
                {
                    ID = 2,
                    Name = "Green Arrow - Oliver Queen"
                },
                new Superhero()
                {
                    ID = 3,
                    Name = "Super Girl - Kara Danvers"
                }
            });
        }
        public static List<Superhero> Superheroes { get; set; } = new();
    }
}
