using Newtonsoft.Json;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Xml.Serialization;

namespace Nagad
{
    public static class RsaOperation
    {
        private static RSACryptoServiceProvider csp;
        private static RSAParameters _nGpublicKey;
        private static RSAParameters _nGprivateKey;
        private static RSAParameters _mSpublicKey;
        private static RSAParameters _mSprivateKey;
        private static bool Initialize = false;
        static RsaOperation()
        {
            csp = new RSACryptoServiceProvider();
            _mSpublicKey = csp.ExportParameters(false);
            _mSprivateKey = csp.ExportParameters(true);
        }
        public static void Init(string publicKey, string privateKey)
        {
            // csp = new RSACryptoServiceProvider();
            if (Initialize == false)
            {
                _nGpublicKey = new RSAParameters()
                {
                    Modulus = System.Convert.FromBase64String(publicKey),
                    Exponent = HexStringToByteArray("010001")
                };

                _nGprivateKey = new RSAParameters()
                {
                    Modulus = System.Convert.FromBase64String(privateKey),
                    Exponent = HexStringToByteArray("010001")
                };

                //  csp.ImportParameters(rsp);
                //  csp.ImportParameters(priv);
                Initialize = true;
            }
        }

        public static string GetOwnkey(SSLKeyType type) // Workty properlty
        {
            var sw = new StringWriter();
            var sr = new XmlSerializer(typeof(RSAParameters));
            switch (type)
            {
                case SSLKeyType.publicKey:
                    sr.Serialize(sw, _mSpublicKey);
                    return sw.ToString();
                case SSLKeyType.privateKey:

                    sr.Serialize(sw, _mSprivateKey);
                    return sw.ToString();
                default:
                    sr.Serialize(sw, _mSpublicKey);
                    return sw.ToString();
            }


        }


        public static string SignData(byte[] T)
        {
            csp = new RSACryptoServiceProvider();
            csp.ImportParameters(_mSprivateKey);
            //var data = ObjectToByteArray(T);
            var sign = csp.SignData(T, SHA256.Create());
            return Convert.ToBase64String(sign);
        } //Working properly
        public static bool VerifyData(string originalMessage, string signedMessage)
        {
            bool success = false;
            using (var rsa = new RSACryptoServiceProvider())
            {
                byte[] bytesToVerify = Convert.FromBase64String(originalMessage);
                byte[] signedBytes = Convert.FromBase64String(signedMessage);
                try
                {
                    rsa.ImportParameters(_nGpublicKey);
                    //  SHA512Managed Hash = new SHA512Managed();
                    //  byte[] hashedData = Hash.ComputeHash(signedBytes);
                    //CryptoConfig.MapNameToOID("SHA1withRSA)"),
                    success = rsa.VerifyData(bytesToVerify, RSAEncryptionPadding.OaepSHA1, signedBytes);
                }
                catch (CryptographicException e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
            return success;
        }
        public static string Encrypt(byte[] T)
        {

            csp = new RSACryptoServiceProvider();
            csp.ImportParameters(_nGpublicKey);
            //var p = csp.KeySize;
            //var data = ObjectToByteArray(T);
            var ect = csp.Encrypt(T, RSAEncryptionPadding.Pkcs1);
            return Convert.ToBase64String(ect);
        }
        public static object Decrypt(string ectext)
        {
            csp = new RSACryptoServiceProvider();
            var ta = Convert.FromBase64String(ectext);
            csp.ImportParameters(_nGprivateKey);
            var obj = csp.Decrypt(ta, RSAEncryptionPadding.Pkcs1);
            return ByteArrayToObject(obj);
        }







        // Object Converter
        private static byte[] ObjectToByteArray(Object obj)
        {
            if (obj == null)
                return null;
            var bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }






        // Byte Converter
        private static Object ByteArrayToObject(byte[] arrBytes)
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            Object obj = (Object)binForm.Deserialize(memStream);
            return obj;
        }

        public static byte[] HexStringToByteArray(string hexString)
        {
            //  return System.Convert.FromBase64String(hexString);
            MemoryStream stream = new MemoryStream(hexString.Length / 2);

            for (int i = 0; i < hexString.Length; i += 2)
            {
                stream.WriteByte(byte.Parse(hexString.Substring(i, 2), System.Globalization.NumberStyles.AllowHexSpecifier));
            }
            return stream.ToArray();
        }

        public static string GenerateRandomString(int length = 40)
        {
            string StringChars = "0123456789abcdef";
            Random rand = new Random();
            var charList = StringChars.ToCharArray();
            string hexString = "";

            for (int i = 0; i < length; i++)
            {
                int randIndex = rand.Next(0, charList.Length);
                hexString += charList[randIndex];
            }

            return hexString;
        }
    }
}

