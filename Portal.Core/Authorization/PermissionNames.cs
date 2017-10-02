namespace Portal.Core.Authorization
{
    public static class PermissionNames
    {
        public const string PagesTenants = "Pages.Tenants";
        public const string PagesUsers = "Pages.Users";
        public const string PagesRoles = "Pages.Roles";

        // delete, approve and edit contents
        public const string ContentChange = "Content.Change";

        // add contents
        public const string ContentAdd = "Content.Add";
    }
}