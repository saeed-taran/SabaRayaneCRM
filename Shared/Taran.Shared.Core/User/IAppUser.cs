namespace Taran.Shared.Core.User
{
    public interface IAppUser
    {
        string UserName { get; }
        string FirstName { get; }
        string LastName { get; }
        int UserID { get; }
        public IReadOnlyCollection<int> RoleIds { get; }
        public string FullName { get; }
        public long LoginDate { get; }
    }
}
