using HotelManagement.Core.ViewModels;
using HotelManagement.Core.ViewModels.Authentication;
using HotelManagement.Core.ViewModels.Response;
using HotelManagement.Core.ViewModels.Users;

namespace HotelManagement.Core.Interfaces.Services;
public interface IAuthenticationService
{
    ResponseViewModel<IEnumerable<UserViewModel>> GetAllUsers();
    ResponseViewModel<UserDetailsViewModel> GetUserDetailsById(int id);
    Task<ResponseViewModel<bool>> DeleteUserByIdAsync(int id);
    //TODO: Implement method GetAllFavoriteRecipesByUserId
    //IEnumerable<RecipeViewModel> GetAllFavoriteRecipesByUserId(int id);

    Task<ResponseViewModel<AuthModel>> RegisterAsync(UserCreateViewModel viewModel);
    Task<ResponseViewModel<AuthModel>> LoginAsync(LoginViewModel loginViewModel);
    Task<ResponseViewModel<AuthModel>> ResetPassword(ResetPasswordViewModel viewModel);
    Task<ResponseViewModel<int>> ForgetPassword(string email);
    Task<ResponseViewModel<AuthModel>> VerifyResetCode(VerifyCodeViewModel model);
}

