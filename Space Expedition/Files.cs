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
            
            try
            {
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
                        string[] array = line.Split(',');
                        if (array.Length < 5) continue;

                        string encodedName = array[0].Trim().Trim('"');
                        string planet = array[1].Trim();
                        string discoveryDate = array[2].Trim();
                        string storageLocation = array[3].Trim();
                        string description = array[4].Trim();
                        for (int j = 5; j < array.Length; j++)
                        {
                            description += "," + array[j];
                        }
                        description = description.Trim();
                        string decodedName = Decoder.DecodeName(encodedName).ToUpper();
                        inventory[count++] = new ArtifactInventory(encodedName, planet, discoveryDate, storageLocation, description, decodedName);

                    }
                }
                InventorySort(inventory, count);
            }
            catch (IOException)
            {
                Console.WriteLine("Can't load the file.");
            } catch(Exception)
            {
                Console.WriteLine("Data has some error, caused can't load. ");
            }
        }

        public static void Displaying(ArtifactInventory[] inventory, int count)
        {
            for (int i = 0; i < count; i++)
            {
                ArtifactInventory artifact = inventory[i];
                Console.WriteLine($"{artifact.DecodedName} | {artifact.Planet} | {artifact.DiscoveryDate} | {artifact.StorageLocation} | {artifact.Description} ");
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

        public static void Adding(ref ArtifactInventory[] inventory, ref int count)
        {
            Console.Write("Please enter EncodedName here: ");
            string encodedName = Console.ReadLine();
            Console.Write("Please enter which planet you find: ");
            string planet = Console.ReadLine();
            Console.Write("Please enter the discovery date here: ");
            string discoveryDate = Console.ReadLine();
            Console.Write("Please enter the storage location here: ");
            string storageLocation = Console.ReadLine();
            Console.Write("Please enter the description here: ");
            string description = Console.ReadLine();
            string decodedName = Decoder.DecodeName(encodedName);
            decodedName = decodedName.ToUpper();

            int found = SearchingByDecodedName(inventory, count, decodedName);
            if (found != -1)
            {
                Console.WriteLine("This inventory has already in here. ");
                return;
            }

            if (count >= inventory.Length)
            {
                ArtifactInventory[] moreInventory = new ArtifactInventory[inventory.Length * 2];

                for (int i = 0; i < inventory.Length; i++)
                {
                    moreInventory[i] = inventory[i];
                }

                inventory = moreInventory;
            }

            ArtifactInventory newArtifact = new ArtifactInventory(encodedName, planet, discoveryDate, storageLocation, description, decodedName);
            int index = count;
            while (index > 0)
            {
                int compare = inventory[index - 1].DecodedName.CompareTo(newArtifact.DecodedName);
                if (compare > 0)
                {
                    inventory[index] = inventory[index - 1];
                    index--;
                }
                else
                {
                    break;
                }
            }
            inventory[index] = newArtifact;
            count++;
        }

        public static void Save(string path, ArtifactInventory[] inventory, int count)
        {
            try
            {
                using (StreamWriter saveFiles = new StreamWriter("expedition_summary.txt"))
                {
                    for (int i = 0; i < count; i++)
                    {
                        saveFiles.WriteLine(inventory[i].EncodedName + "|" + inventory[i].Planet + "|" + inventory[i].DiscoveryDate + "|" + inventory[i].StorageLocation + "|" + inventory[i].Description);
                    }
                }
            }
            catch(IOException)
            {
                Console.WriteLine("There has some error to save the summary.");
            }
        }
    }
}
