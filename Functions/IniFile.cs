using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace AutoBorderless
{
    // Credits to Danny Beckett at stackoverflow.
    // https://stackoverflow.com/questions/217902/reading-writing-an-ini-file

    internal class IniFile
    {
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern int GetPrivateProfileSection(string Section, byte[] RetVal, int Size, string FilePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern int GetPrivateProfileSectionNames(byte[] RetVal, int Size, string FilePath);

        string Path;
        string EXE = Assembly.GetExecutingAssembly().GetName().Name;

        public IniFile(string IniPath = null)
        {
            this.Path = new FileInfo(IniPath ?? this.EXE + ".ini").FullName;
        }

        public string Read(string Key, string Section = null)
        {
            var RetVal = new StringBuilder(255);
            GetPrivateProfileString(Section, Key, "", RetVal, 255, this.Path);
            string Value = RetVal.ToString();

            if (Value.Contains(";") | Value.Contains("#"))
            {
                string[] SplitValue = Value.Split(';','#');
                Value = SplitValue[0].Trim();
            }
            return Value;
        }

        public void Write(string Key, string Value, string Section = null)
        {
            WritePrivateProfileString(Section, Key, Value, this.Path);
        }

        public void DeleteKey(string Key, string Section = null)
        {
            Write(Key, null, Section);
        }

        public void ConditionalWriteDelete(string Key, string Value, string Section = null, bool DoWrite = true)
        {
            if (DoWrite)
                Write(Key, Value, Section);
            else
                DeleteKey(Key, Section);
        }

        public void DeleteSection(string Section = null)
        {
            Write(null, null, Section);
        }

        public bool KeyExists(string Key, string Section = null)
        {
            return Read(Key, Section).Length > 0;
        }

        public List<string> GetSections()
        {
            byte[] ByteArray = new byte[32767];
            GetPrivateProfileSectionNames(ByteArray, 32767, this.Path);

            string[] Chars = Encoding.ASCII.GetString(ByteArray).Split('\0');
            List<string> Sections = new List<string>();
            string sectionName = "";

            for (int i = 0; i < Chars.Length; i++)
            {
                if (Chars[i] != "" & i != Chars.Length - 1)
                {
                    sectionName += Chars[i];
                }
                else if (sectionName != "")
                {
                    Sections.Add(sectionName);
                    sectionName = "";
                }
            }
            return Sections;
        }

        public List<string> GetSectionKeys(string Section)
        {
            byte[] ByteArray = new byte[32767];
            GetPrivateProfileSection(Section, ByteArray, 32767, this.Path);

            string[] Chars = Encoding.ASCII.GetString(ByteArray).Split('\0');
            List<string> Keys = new List<string>();
            string keyName = "";

            for (int i = 0; i < Chars.Length; i++)
            {
                if (Chars[i] != "" & i != Chars.Length - 1)
                {
                    keyName += Chars[i];
                }
                else if (keyName != "")
                {
                    Keys.Add(keyName.Split('=')[0]);
                    keyName = "";
                }
            }
            return Keys;
        }
    }
}
