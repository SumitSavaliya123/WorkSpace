using Common.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Enums
{
    public class EntityStatus
    {
        public enum UserStatusTypes
        {
            ACTIVE = EntityStatusConstants.ACTIVE,
            INACTIVE = EntityStatusConstants.INACTIVE,
            DELETED = EntityStatusConstants.DELETED
        }
    }
}
