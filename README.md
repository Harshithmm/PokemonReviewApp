if (args.Length == 1 && args[0].ToLower() == "seeddata")
    SeedData(app);

void SeedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<Seed>();
        service.SeedDataContext();
    }
}

the above code is used to seed the database when the command
dotnet run seeddata is ran in terminal of vs

this will run the seed.cs which will add the datat to the database


============================================================================================================================================================
        public ICollection<Review> GetReviewsByReviewer(int reviewerId)
        {
            //return _context.Reviews.Where(r=>r.Reviewer.Id==reviewerId).ToList();
            return _context.Reviewers.Where(r=>r.Id==reviewerId).SelectMany(r=>r.Reviews).ToList();
        }

                 public ICollection<Pokemon> GetPokemonByOwner(int ownerId)
        {
            return context.PokemonOwners.Where(p=>p.OwnerId==ownerId).Select(p=>p.Pokemon).ToList();
            //return context.PokemonOwners.Where(p=>p.Owner.Id==ownerId).Select(p=>p.Pokemon).ToList();
        }

    on the above examples in first we use SelectMany and in Second we use Select but if we change them we get a error even though they look the same

    bcz    r.Reviews type is ICollection<Review> and p.Pokemon type is Pokemon in the model hence Select makes a ICollection whereas SelectMany does not make collection ig.

    =========================================================================================================================================================

    This error happens because the JSON serializer detected a possible object cycle. An object cycle occurs when an object refers to itself, either directly or indirectly, causing an infinite loop during serialization.
This might be happening because your Reviewer object likely has a Reviews property, and each Review in turn has a Reviewer property. This creates a cycle: Reviewer -> Reviews -> Reviewer -> Reviews -> ... and so on.
To fix this issue, you can set the ReferenceHandler property to Preserve on JsonSerializerOptions when serializing your object. This tells the serializer to preserve object references, which prevents it from getting stuck in an infinite loop when it encounters a cycle.

this can be fixed by adding 
builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

    on above the 
    builder.Services.AddScoped<IPokemonRepository, PokemonRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<IOwnerRepository, OwnerRepository>();
builder.Services.AddScoped<IReviewerRepository, ReviewerRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();