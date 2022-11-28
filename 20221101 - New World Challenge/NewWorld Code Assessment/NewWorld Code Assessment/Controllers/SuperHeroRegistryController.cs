using Microsoft.AspNetCore.Mvc;
using NewWorld_Code_Assessment.DataAccessLayer;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NewWorld_Code_Assessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroRegistryController : ControllerBase
    {
        
        // GET: api/<SuperHeroRegistryController>
        [HttpGet]
        public IEnumerable<Superhero> Get()
        {
            return SuperHeroRepository.Superheroes;
        }

        // GET api/<SuperHeroRegistryController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpGet("NameLike/{comparisonSearch}")]
        public IEnumerable<Superhero> GetByName(string comparisonSearch)
        {
            return SuperHeroRepository.Superheroes.Where(hero =>
                hero.Name.Contains(comparisonSearch, StringComparison.InvariantCultureIgnoreCase));
        }

        [HttpPost("AddAbility/{abilityName}/To/{superheroId:int}")]
        public Superhero? AddNewAbility(int superheroId, string abilityName)
        {
            var superhero = SuperHeroRepository.Superheroes.FirstOrDefault(hero=>hero.ID == superheroId);
            if (superhero == null)
            {
                return null;
            }

            abilityName = abilityName.Trim();
            if (!superhero.Abilities
                    .Any(
                        ability =>
                            ability.Name.Equals(abilityName, StringComparison.InvariantCultureIgnoreCase)))
            {
                superhero.Abilities.Add(new Ability()
                {
                    Name = abilityName
                });
            }
            return superhero;
        }

        // POST api/<SuperHeroRegistryController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<SuperHeroRegistryController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SuperHeroRegistryController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
