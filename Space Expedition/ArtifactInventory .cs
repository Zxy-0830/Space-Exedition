using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Expedition
{
    internal class ArtifactInventory
    {
        public string EncodedName {  get; set; }
        public string Planet {  get; set; }
        public string DiscoveryDate { get; set; }
        public string StorageLocation {  get; set; }
        public string Description {  get; set; }
        public string DecodedName {  get; set; }

        public ArtifactInventory(string encodedName,string planet,string discoveryDate,string storageLocation,string description,string decodedName)
        {
            this.EncodedName = encodedName;
            this.Planet = planet;
            this.DiscoveryDate = discoveryDate;
            this.StorageLocation = storageLocation;
            this.Description = description;
            this.DecodedName = decodedName;
        }

        public override string ToString()
        {
            return EncodedName + " | " + Planet + " | " + DiscoveryDate + " | " + StorageLocation + " | " + Description;
        }        
    }
}
