public class Team
{
    public Team(string name)
    {
        Name = name;
    }

    public int Id { get; set; }
    public string Name { get; set;}

    
}

public static class TeamRouter
{
    public static void MapTeam(this IEndpointRouteBuilder app)
    {
        var teams = new List<Team>();

        var index = 1;
        var team = new Team("Barcelona");
        team.Id = index;

        teams.Add(team);


        // Get All
        app.MapGet("/api/teams", (string? str) => {
            var teamsOut = teams.Where(x => str == null || (str != null && x.Name.ToLower().Contains(str.ToLower())))
                                .ToList();
            return teamsOut;
        });

        // Get
        app.MapGet("/api/teams/{id}", (int id) =>
        {
            var team = teams.FirstOrDefault(x => x.Id == id);
            return team;
        });

        // Create
        app.MapPost("/api/teams", (Team teamInput) =>
        {
            var exist = teams.FirstOrDefault(x => x.Name.ToLower() == teamInput.Name.ToLower());

            if (exist != null)
            {
                return false;
            }

            index = index + 1;
            teamInput.Id = index;
            teams.Add(teamInput);
            return true;
        });

        // Edit
        app.MapPut("/api/teams", (Team teamInput) =>
        {
            var team = teams.FirstOrDefault(x => x.Id == teamInput.Id);

            if (team == null)
            {
                return false;
            }

            teams.Remove(team);
            teams.Add(teamInput);

            return true;
        });

        // Delete
        app.MapDelete("/api/teams/{id}", (int id) =>
        {
            var team = teams.FirstOrDefault(x => x.Id == id);

            if (team == null)
            {
                return false;
            }

            teams.Remove(team);
            return true;
        });
    }

}