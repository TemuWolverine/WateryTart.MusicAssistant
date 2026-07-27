using WateryTart.MusicAssistant.Models;

namespace WateryTart.MusicAssistant.Responses;

public class GenresResponse : ResponseBase<List<Genre>>
{ 

}

public class GenreResponse : ResponseBase<Genre>
{
}

public class GenreOverviewResponse : ResponseBase<List<GenreOverview>>
{
}