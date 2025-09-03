
namespace Taran.Shared.UI.Services
{
    public interface IToastService
    {
        Task Show(string message, ToastKind toastKind);
    }

    public enum ToastKind
    {
        danger,
        warning,
        success,
        primary
    }
}