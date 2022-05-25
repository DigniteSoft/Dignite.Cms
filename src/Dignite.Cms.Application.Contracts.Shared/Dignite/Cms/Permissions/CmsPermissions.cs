using Volo.Abp.Reflection;

namespace Dignite.Cms.Permissions
{
    public class CmsPermissions
    {
        public const string GroupName = "Cms";

        public static class Page
        {
            public const string Default = GroupName + ".Page";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
            public const string SuperAuthorization = Default + ".SuperAuthorization";   //超级用户授权，可以在不授权的情况下访问任意版块
        }


        public static class Section
        {
            public const string Default = GroupName + ".Section";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
        }

        public static class Entry
        {
            public const string Default = GroupName + ".Entry";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
            public const string Audit = Default + ".Audit";     //拥有审核他人条目的权限
        }


        public static class User
        {
            public const string Default = GroupName + ".User";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
        }

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(CmsPermissions));
        }
    }
}