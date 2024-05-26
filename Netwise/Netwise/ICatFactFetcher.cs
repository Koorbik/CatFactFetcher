namespace Netwise
{
    interface ICatFactFetcher
    {
        Task<CatFact?> GetCatFactAsync(CatFact? catFact);
    }
}
