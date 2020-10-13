using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;
using System.Text;
using System.Security.Cryptography;

namespace LevelManagement.Data
{
    public class JsonSaver
    {
        private static readonly string _filename = "saveData1.sav";

        public static string GetSaveFilename()
        {
            return Application.persistentDataPath + "/" + _filename;
        }

        public void Save(SaveData data)
        {
            data.hashValue = String.Empty;

            //Transforma para json
            string json = JsonUtility.ToJson(data);

            //Criptografa os dados do arquivo
            data.hashValue = GetSHA256(json);

            //Transforma em json novamente - agora com a hash preenchida
            json = JsonUtility.ToJson(data);

            //Pega o caminho em que o arquivo será salvo, independente de plataforma
            string saveFilename = GetSaveFilename();

            //Cria um arquivo vazio no caminho especificado
            FileStream fileStream = new FileStream(saveFilename, FileMode.Create);

            //Escreve os dados no arquivo 
            using (StreamWriter writer = new StreamWriter(fileStream))
            {
                writer.Write(json);
            }
        }

        public bool Load(SaveData data)
        {
            string loadFilename = GetSaveFilename();
            if (File.Exists(loadFilename))
            {
                using (StreamReader reader = new StreamReader(loadFilename))
                {
                    //Recupera o arquivo json
                    string json = reader.ReadToEnd();

                    if (CheckData(json))
                    {
                        //Carrega o arquivo json no objeto data
                        JsonUtility.FromJsonOverwrite(json, data);
                    }
                    else
                    {
                        Debug.Log("JSONSAVER load: invalid hash. Aborting file read...");
                    }

                    
                }
                return true;
            }
            return false;
        }

        public void Delete()
        {
            File.Delete(GetSaveFilename());
        }

        private bool CheckData(string json)
        {
            SaveData tempSaveData = new SaveData();
            JsonUtility.FromJsonOverwrite(json, tempSaveData);

            //Pega a hash que está salva no arquivo
            string oldHash = tempSaveData.hashValue;

            //Limpa o valor salvo na hash
            tempSaveData.hashValue = String.Empty;

            //Converte novamente para json
            string tempJson = JsonUtility.ToJson(tempSaveData);

            //Gera uma nova hash
            string newHash = GetSHA256(tempJson);

            // Se as hash forem diferentes, significa que o arquivo .sav foi modificado
            return (oldHash == newHash);
        }

        public string GetHexStringFromHash(byte[] hash)
        {
            string hexString = String.Empty;

            foreach(byte b in hash)
            {
                hexString += b.ToString("x2");
            }

            return hexString;
        }

        private string GetSHA256(string text)
        {
            byte[] textToBytes = Encoding.UTF8.GetBytes(text);
            SHA256Managed mySHA256 = new SHA256Managed();

            byte[] hashValue = mySHA256.ComputeHash(textToBytes);

            return GetHexStringFromHash(hashValue);
        }
    } 
}
