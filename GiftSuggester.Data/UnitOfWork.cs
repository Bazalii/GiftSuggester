using GiftSuggester.Core.CommonClasses;

namespace GiftSuggester.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly GiftSuggesterContext _context;
    
    public UnitOfWork(GiftSuggesterContext context)
    {
        _context = context;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}