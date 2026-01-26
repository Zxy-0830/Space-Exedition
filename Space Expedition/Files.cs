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

        public static void Displaying(ArtifactInventory[] inventory, int count)
        {
            for (int i = 0; i < count; i++)
            {
                ArtifactInventory artifact = inventory[i];
                Console.WriteLine($"{artifact.EncodedName} | {artifact.Planet} | {artifact.DiscoveryDate} | {artifact.StorageLocation} | {artifact.Description} ");
            }
        }

        public static void InventorySort(ArtifactInventory[] inventory, int count)
        {
            for (int i = 1; i < count; i++)
            {
                ArtifactInventory key = inventory[i];
                string keyName = key.DecodedName;

                int j = i - 1;

                while (j >= 0 && inventory[j].DecodedName.CompareTo(keyName) > 0)
                {
                    inventory[j + 1] = inventory[j];
                    j--;
                }

                inventory[j + 1] = key;
            }
        }

        public static int SearchingByDecodedName(ArtifactInventory[] inventory, int count, string targetDecodedName)
        {
            int lo = 0;
            int hi = count - 1;
            targetDecodedName = targetDecodedName.ToUpper();

            while (lo <= hi)
            {
                int mid = lo + (hi - lo) / 2;
                int result = inventory[mid].DecodedName.CompareTo(targetDecodedName);
                if (result == 0)
                {
                    return mid;
                }
                else if (result > 0)
                {
                    hi = mid - 1;
                }
                else
                {
                    lo = mid + 1;
                }
            }
            return -1;
        }
    }
}
