using System;
using System.Collections.Generic;
using System.Text;

namespace Shukhlyada.BusinessLogic.Exstensions
{
    public static class PermissionsExtension
    {
        public static bool FindPermission(this string permissions, string searchedPermission)
        {
            var permissionArray = permissions.Split(';');
            foreach(var permission in permissionArray)
            {
                if (permission.Equals(searchedPermission))
                    return true;
            }
            return false;
        }
    }
}
