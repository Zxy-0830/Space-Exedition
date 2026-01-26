using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Expedition
{
    internal class ArtifactInventory
    {
        private string encodedName;
        private string planet;
        private string discoveryDate;
        private string storageLocation;
        private string description;
        private string decodedName;

        public string EncodedName { get { return encodedName; } }
        public string Planet { get { return planet; } }
        public string DiscoveryDate { get { return discoveryDate; } }
        public string StorageLocation { get { return storageLocation; } }
        public string Description { get { return description; } }
        public string DecodedName { get { return decodedName; } }

        public ArtifactInventory(string encodedName,string planet,string discoveryDate,string storageLocation,string description,string decodedName)
        {
            this.encodedName = encodedName;
            this.planet = planet;
            this.discoveryDate = discoveryDate;
            this.storageLocation = storageLocation;
            this.description = description;
            this.decodedName = decodedName;
        }

        public override string ToString()
        {
            return encodedName + " | " + planet + " | " + discoveryDate + " | " + storageLocation + " | " + description;
        }        
    }
}
