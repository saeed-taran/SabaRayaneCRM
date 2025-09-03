using Taran.Shared.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taran.Shared.Core.Entity
{
    public class BaseEntity<PrimaryKey>
    {
        public BaseEntity(int creatorUserId)
        {
            if (creatorUserId <= 0)
                throw new DomainArgumentNullOrEmptyException(nameof(CreatorUserId));

            CreationDate = DateTime.UtcNow;
            ModificationDate = CreationDate;
            CreatorUserId = creatorUserId;
        }

        protected void Update(int editorUserId)
        {
            if (editorUserId <= 0)
                throw new DomainArgumentNullOrEmptyException(nameof(EditorUserId));

            EditorUserId = editorUserId;
            ModificationDate = DateTime.UtcNow;
        }

        public PrimaryKey Id { get; protected set; }

        public int CreatorUserId { get; private set; }
        public DateTime CreationDate { get; private set; }

        public int? EditorUserId { get; private set; }
        public DateTime? ModificationDate { get; private set; }
    }
}
