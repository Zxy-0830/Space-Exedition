using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Space_Expedition
{
    internal class Files
    {
        public static void Load(string path, out ArtifactInventory[] inventory, out int count)
        {
            inventory = new ArtifactInventory[20];
            count = 0;
            using (StreamReader sr = new StreamReader("galactic_vault.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (count >= inventory.Length)
                    {
                        ArtifactInventory[] newInventory = new ArtifactInventory[inventory.Length * 2];

                        for (int i = 0; i < inventory.Length; i++)
                        {
                            newInventory[i] = inventory[i];
                        }

                        inventory = newInventory;
                    }
                    string[] array = line.Split('|');
                    if (array.Length < 5) continue;
                    string encodedName = array[0].Trim();
                    string planet = array[1].Trim();
                    string discoveryDate = array[2].Trim();
                    string storageLocation = array[3].Trim();
                    string description = array[4].Trim();
                    string decodedName = Decoder.DecodeName(encodedName);
                    ArtifactInventory artifact = new ArtifactInventory(encodedName, planet, discoveryDate, storageLocation, description, decodedName);
                    inventory[count++] = artifact;
                }
            }
        }
    }
}
