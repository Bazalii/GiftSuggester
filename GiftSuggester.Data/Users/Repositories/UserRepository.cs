using GiftSuggester.Core.Exceptions;
using GiftSuggester.Core.Users.Models;
using GiftSuggester.Core.Users.Repositories;
using GiftSuggester.Data.Users.Mappers;
using Microsoft.EntityFrameworkCore;

namespace GiftSuggester.Data.Users.Repositories;

public class UserRepository : IUserRepository
{
    private readonly GiftSuggesterContext _context;
    private readonly UserDbModelsMapper _mapper;

    public UserRepository(GiftSuggesterContext context, UserDbModelsMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task AddAsync(User user, CancellationToken cancellationToken)
    {
        return _context.Users
            .AddAsync(_mapper.MapUserToDbModel(user), cancellationToken)
            .AsTask();
    }

    public async Task<User> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var dbModel =
            await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id, cancellationToken) ??
            throw new EntityNotFoundException($"User with id: {id} is not found!");

        return _mapper.MapDbModelToUser(dbModel);
    }

    public async Task<User> GetByLoginAsync(string login, CancellationToken cancellationToken)
    {
        var dbModel =
            await _context.Users.FirstOrDefaultAsync(model => model.Login == login, cancellationToken) ??
            throw new EntityNotFoundException($"User with login: {login} is not found!");

        return _mapper.MapDbModelToUser(dbModel);
    }

    public async Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        var dbModel =
            await _context.Users.FirstOrDefaultAsync(model => model.Email == email, cancellationToken) ??
            throw new EntityNotFoundException($"User with email: {email} is not found!");

        return _mapper.MapDbModelToUser(dbModel);
    }

    public async Task<bool> ExistsWithIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var dbModel = await _context.Users.FirstOrDefaultAsync(model => model.Id == id, cancellationToken);

        return dbModel is not null;
    }

    public async Task<bool> ExistsWithLoginAsync(string login, CancellationToken cancellationToken)
    {
        var dbModel = await _context.Users.FirstOrDefaultAsync(model => model.Login == login, cancellationToken);

        return dbModel is not null;
    }

    public async Task<bool> ExistsWithEmailAsync(string email, CancellationToken cancellationToken)
    {
        var dbModel = await _context.Users.FirstOrDefaultAsync(model => model.Email == email, cancellationToken);

        return dbModel is not null;
    }

    public async Task UpdateAsync(User user, CancellationToken cancellationToken)
    {
        var dbModel =
            await _context.Users.FirstOrDefaultAsync(model => model.Id == user.Id, cancellationToken) ??
            throw new EntityNotFoundException($"User with id: {user.Id} is not found!");

        dbModel.Name = user.Name;
        dbModel.Login = user.Login;
        dbModel.Email = user.Email;
        dbModel.DateOfBirth = user.DateOfBirth;
    }

    public async Task UpdatePasswordAsync(Guid id, string password, CancellationToken cancellationToken)
    {
        var dbModel =
            await _context.Users.FirstOrDefaultAsync(model => model.Id == id, cancellationToken) ??
            throw new EntityNotFoundException($"User with id: {id} is not found!");

        dbModel.Password = password;
    }

    public async Task RemoveByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var dbModel =
            await _context.Users.FirstOrDefaultAsync(model => model.Id == id, cancellationToken) ??
            throw new EntityNotFoundException($"User with id: {id} is not found!");

        _context.Users.Remove(dbModel);
    }
}