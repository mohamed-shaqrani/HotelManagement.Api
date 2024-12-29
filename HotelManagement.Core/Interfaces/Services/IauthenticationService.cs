using HotelManagement.App.Core.ViewModels;
using HotelManagement.App.Core.ViewModels.Authentication;
using HotelManagement.App.Core.ViewModels.Response;
using HotelManagement.App.Core.ViewModels.Users;

namespace HotelManagement.App.Core.Interfaces.Services;
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

