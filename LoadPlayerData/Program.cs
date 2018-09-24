using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Dynamic;

using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace LoadPlayerData
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Run Program\n");
            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();

            //指定したディレクトリ下の.jsonファイルを取得する
            string directoryPath = "C:\\Users\\mimutai\\Documents\\Visual Studio Code\\LoadPlayerData\\playerdata\\";
            string[] files = Directory.GetFiles(directoryPath);

            List<PlayerData> playerData = new List<PlayerData>();

            foreach (string file in files)
            {
                playerData.Add(JsonSerialize(file));
            }

            foreach(PlayerData data in playerData){
                Console.WriteLine(data.profile.name);
            }
            sw.Stop();
            Console.WriteLine("\n経過時間 " + sw.Elapsed.Milliseconds + " ms");
        }

        //Jsonの情報をシリアライズするメソッド
        public static PlayerData JsonSerialize(string path)
        {

            StreamReader sr = new StreamReader(path);
            string stringJson = sr.ReadToEnd();

            var serializer = new DataContractJsonSerializer(typeof(PlayerData));
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(stringJson)))
            {
                PlayerData playerData = (PlayerData)serializer.ReadObject(ms);
                return playerData;
            }

        }
    }

    [DataContract]
    public class PlayerData
    {
        [DataMember(Name = "profile")]
        public Profile profile = new Profile();

        //settings
        [DataMember(Name = "songs")]
        public List<SongPlayData> SongPlayData = new List<SongPlayData>();
    }

    public class Profile
    {
        public string name;
    }

    [DataContract(Name = "songs")]
    public class SongPlayData
    {
        [DataMember(Name = "title")]
        public string title;
        [DataMember(Name = "easy")]
        public SongPlayData_Difficulty easy = new SongPlayData_Difficulty();
        [DataMember(Name = "hard")]
        public SongPlayData_Difficulty hard = new SongPlayData_Difficulty();
    }

    public class SongPlayData_Difficulty
    {
        public int score;
        public int perfect;
        public int good;
        public int bad;
        public int miss;
        public string modified;
    }
}
