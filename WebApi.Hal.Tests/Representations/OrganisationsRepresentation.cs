using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApi.Hal.Tests.Representations
{
    class OrganisationsRepresentation : Representation
    {
        public ICollection<OrganisationWithPeopleRepresentation> Organisations { get; set; }

        protected override void CreateHypermedia()
        {
            this.Rel = "organisations";
            this.Href = "/organisations";
        }
    }
}
