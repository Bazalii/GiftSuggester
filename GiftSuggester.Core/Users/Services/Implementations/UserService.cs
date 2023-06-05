using FluentValidation;
using GiftSuggester.Core.CommonClasses;
using GiftSuggester.Core.Exceptions;
using GiftSuggester.Core.Users.Models;
using GiftSuggester.Core.Users.Repositories;
using GiftSuggester.Core.Users.Validators;

namespace GiftSuggester.Core.Users.Services.Implementations;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly UserValidator _userValidator;
    private readonly UserPasswordValidator _userPasswordValidator;

    public UserService(
        IUnitOfWork unitOfWork,
        IUserRepository userRepository,
        UserValidator userValidator,
        UserPasswordValidator userPasswordValidator)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _userValidator = userValidator;
        _userPasswordValidator = userPasswordValidator;
    }

    public async Task AddAsync(UserCreationModel creationModel, CancellationToken cancellationToken)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = creationModel.Name,
            Login = creationModel.Login,
            Email = creationModel.Email,
            Password = creationModel.Password,
            DateOfBirth = creationModel.DateOfBirth,
            GroupIds = new List<Guid>()
        };

        await _userValidator.ValidateAndThrowAsync(user, cancellationToken);

        await _userRepository.AddAsync(user, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public Task<User> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _userRepository.GetByIdAsync(id, cancellationToken);
    }

    public Task<User> GetByLoginAsync(string login, CancellationToken cancellationToken)
    {
        return _userRepository.GetByLoginAsync(login, cancellationToken);
    }

    public Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return _userRepository.GetByEmailAsync(email, cancellationToken);
    }

    public async Task UpdateAsync(User user, CancellationToken cancellationToken)
    {
        await _userValidator.ValidateAndThrowAsync(user, cancellationToken);

        await _userRepository.UpdateAsync(user, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdatePasswordAsync(Guid id, string oldPassword, string newPassword,
                                          CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(id, cancellationToken);

        if (user.Password != oldPassword)
        {
            throw new InvalidCredentialsException("Invalid old password!");
        }

        user.Password = newPassword;

        await _userPasswordValidator.ValidateAndThrowAsync(user, cancellationToken);

        await _userRepository.UpdatePasswordAsync(id, newPassword, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await _userRepository.RemoveByIdAsync(id, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}